using UnityEngine;

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
    #endregion

    // Method
    #region
    // Deviate
    #region 
    private float DeviatePower(float power)
    {
        float deviation = power * (1 + Random.Range(-0.25f, 0.25f));
        return deviation;
    }
    private Vector3 DeviatePosition()
    {
        float deviationX = Random.Range(minTransform.position.x, maxTransform.position.x);
        float deviationY = Random.Range(minTransform.position.y, maxTransform.position.y);
        Vector3 position = new Vector3(deviationX, deviationY, 0f);
        return position;
    }
    #endregion

    // Add Ball In Tray
    #region
    public void AddBallInTray(int num)
    {
        if((minTransform == null) || (maxTransform == null))
        {
            Debug.Log("Missing Dropper Transform Reference!");
            return;
        }
        if(ball == null)
        {
            Debug.Log("Missing Ball GameObject Reference!");
            return;
        }
        for(int i = 0; i < num; i++)
        {
            GameObject trayBall = Instantiate(ball);
            trayBall.transform.SetPositionAndRotation(DeviatePosition(), Quaternion.Euler(0f, 0f, 0f));
        }
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
