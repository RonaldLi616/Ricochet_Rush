using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
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
    public static GameManager GetInstance() { return instance; }
    #endregion

    // Valuables
    #region
    [SerializeField] private GameObject ball;
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private GameObject aimPoint;
    [SerializeField] private GameObject shooterPivot;
    [SerializeField] private GameObject traySpawnPoint;
    [SerializeField] private Text ballRemainText;
    
    [SerializeField][Range(6f, 10f)] private float shootPower = 10f;
    #endregion

    // Player Record
    #region 
    [SerializeField] private PlayerRecord playerRecord;
    public PlayerRecord GetPlayerRecord()
    {
        if (playerRecord == null)
        {
            Debug.Log("Missing Player Record Reference!");
            return null;
        }
        return playerRecord;
    }
    public void SetBallRemain(int ballCount)
    {
        GetPlayerRecord().ballRemains = ballCount;
        UpdateBallRemains();
    }
    #endregion

    // Component
    #region 
    // Mouse
    #region 
    private Vector3 mousePosition;
    private void UpdateMousePosition(){ mousePosition = Input.mousePosition; }
    public Vector3 GetMousePosition(){ return mousePosition; }
    public Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(GetMousePosition());
        mouseWorldPosition = new Vector3(mouseWorldPosition.x, mouseWorldPosition.y, 0f);
        return mouseWorldPosition;
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

    private void OnClickSpaceBar()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shooter.GetInstance().ShootBall();
        }
    }

    // Update Ball Remain Text
    #region
    public void UpdateBallRemains()
    {
        ballRemainText.text = "Ball Remains: " + playerRecord.ballRemains;
    }
    #endregion

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMousePosition();
        OnClickSpaceBar();
    }

    // Temporary
    #region
    private void Temporary()
    {
        Vector3 x = new Vector3();
        Quest_Studio.Deviation.DeviateVector3(x, x, 2);
    }
    #endregion
}
