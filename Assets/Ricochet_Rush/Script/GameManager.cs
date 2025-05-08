using UnityEngine;
using UnityEngine.InputSystem.iOS;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Valuables
    #region
    [SerializeField]private GameObject ball;
    [SerializeField]private GameObject spawnPoint;
    [SerializeField]private Text ballRemainText;
    [SerializeField]private PlayerRecord playerRecord;
    #endregion

    // Instance
    #region 
    public static GameManager instance = null;
    private void SetInstance()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    #endregion

    private void Awake()
    {
        SetInstance();
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
            GameObject newBall = Instantiate(ball);
            newBall.transform.SetPositionAndRotation(spawnPoint.transform.position, spawnPoint.transform.rotation);
            DeductBallRemain(1);
        }
    }

    // Update Ball Remain Text
    #region 
    public void AllBallRemain(int num)
    {
        playerRecord.ballRemains += num;
        UpdateBallRemains();
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
