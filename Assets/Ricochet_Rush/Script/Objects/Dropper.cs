using UnityEngine;
using Quest_Studio;

public class Dropper : MonoBehaviour
{   
    // Instance
    #region 
    private static Dropper instance;
    private void SetInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
        else 
        {
            Destroy(this.gameObject);
        }
    }
    public static Dropper GetInstance(){ return instance;}
    #endregion

    // Reference Object
    #region 
    [SerializeField]private GameObject ball;
    #endregion

    // Valuables
    #region
    [SerializeField]private Transform minTransform;
    [SerializeField]private Transform maxTransform;

    [SerializeField][Min(0f)] private float spawnBallDelay = 0.1f;
    #endregion

    // Method
    #region
    // Add Ball In Tray
    #region
    public void AddBallInTray(int num)
    {
        if ((minTransform == null) || (maxTransform == null))
        {
            Debug.Log("Missing Dropper Transform Reference!");
            return;
        }
        if (ball == null)
        {
            Debug.Log("Missing Ball GameObject Reference!");
            return;
        }
        for (int i = 0; i < num; i++)
        {
            FunctionTimer.Create(() =>
            {
                InstantiateBall();
            }, spawnBallDelay, "AddBallFunctionTimer");
        }
    }
    #endregion

    // Instantiate Ball
    #region 
    private void InstantiateBall()
    {
        GameObject trayBall = Instantiate(ball);
        trayBall.transform.SetPositionAndRotation(Deviation.DeviateVector3(minTransform.position, maxTransform.position, 2), Quaternion.Euler(0f, 0f, 0f));
    }
    #endregion

    #endregion

    private void Awake()
    {
        // Instance
        #region
        SetInstance();
        #endregion
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
