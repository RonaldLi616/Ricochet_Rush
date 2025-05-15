using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Valuables
    #region
    [SerializeField]private GameObject ball;
    [SerializeField]private GameObject spawnPoint;
    [SerializeField]private GameObject aimPoint;
    [SerializeField]private GameObject shooterPivot;
    [SerializeField]private GameObject traySpawnPoint;
    [SerializeField]private Text ballRemainText;
    [SerializeField]private PlayerRecord playerRecord;
    [SerializeField][Range(6f,10f)]private float shootPower = 10f;
    #endregion

    // Instance
    #region 
    public static GameManager instance = null;
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
    public static GameManager GetInstance(){ return instance; }
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
        UpdateBallRemains();
    }

    // Update is called once per frame
    void Update()
    {
        OnClickSpaceBar();
    }

    private void OnClickSpaceBar()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Shooter.GetInstance().ShootBall();
        }
    }

    // Deviate
    #region 
    private float DeviatePower(float power)
    {
        float deviation = power * (1 + Random.Range(-0.25f, 0.25f));
        return deviation;
    }
    private Vector3 DeviatePosition(Vector3 position)
    {
        float deviationNum = 0.15f;
        float deviationX = position.x * (1 + Random.Range(-deviationNum, deviationNum));
        float deviationY = position.y * (1 + Random.Range(-deviationNum, deviationNum));
        Vector3 newPosition = new Vector3(deviationX, deviationY, 0);
        return newPosition;
    }
    #endregion

    // Add Ball in Tray
    #region
    private void AddBallInTray(int num)
    {
        if(traySpawnPoint == null)
        {
            Debug.Log("Missing Tray Spawn Point Reference!");
            return;
        }
        for(int i = 0; i < num; i++)
        {
            GameObject trayBall = Instantiate(ball);
            trayBall.transform.SetPositionAndRotation(DeviatePosition(traySpawnPoint.transform.position), Quaternion.Euler(0f, 0f, 0f));
        }
    }
    #endregion

    // Update Ball Remain Text
    #region 
    public void AllBallRemain(int num)
    {
        playerRecord.ballRemains += num;
        UpdateBallRemains();
        Dropper.GetInstance().AddBallInTray(num);
    }
    public void DeductBallRemain(int num)
    {
        playerRecord.ballRemains -= num;
        UpdateBallRemains();
    }
    private void UpdateBallRemains()
    {
        ballRemainText.text = "Ball Remains: " + playerRecord.ballRemains;
    }
    #endregion
}
