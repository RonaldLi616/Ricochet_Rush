using UnityEngine;
using Quest_Studio;

public class TrayBox : MonoBehaviour
{
    // Instance
    #region 
    public static TrayBox instance = null;
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
    public static TrayBox GetInstance() { return instance; }
    #endregion

    // Component
    #region 
    private void SetComponent()
    {

    }

    #endregion

    // Tray Ball
    #region 
    [SerializeField] private Ball[] balls = null;
    public Ball[] GetInTrayBalls() { return balls; }

    public void AddBallInTray(Ball ball)
    {
        ArrayExtension.AddElement(ref balls, ref ball);
        UpdateBallRemain();
    }

    public bool RemoveBallInTray()
    {
        bool targetFound = false;
        Ball targetBall = null;
        if (this.transform.childCount <= 0) { return targetFound; }
        foreach (Ball ball in balls)
        {
            if (ball.GetBallInTrayStatus() == Ball.InTrayStatus.Ready)
            {
                targetBall = ball;
                targetFound = true;
                break;
            }
        }
        int ballIndex = ArrayExtension.FindElement(ref balls, ref targetBall);
        ArrayExtension.RemoveElement(ref balls, ballIndex);
        Destroy(targetBall.gameObject);
        FunctionTimer.Create(() => { UpdateBallRemain(); }, 0.1f, "DelayUpdateBallRemain");
        //UpdateBallRemain();
        return targetFound;
    }

    private void UpdateBallRemain()
    {
        int ballCount = this.transform.childCount;
        GameManager.GetInstance().SetBallRemain(ballCount);
    }
    #endregion

    private void Awake()
    {
        // Instance
        #region 
        SetInstance();
        #endregion

        // Set Component
        #region 
        SetComponent();
        #endregion

        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateBallRemain();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
