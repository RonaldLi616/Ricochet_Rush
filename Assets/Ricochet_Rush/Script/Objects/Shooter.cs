using UnityEngine;
using Quest_Studio;

public class Shooter : MonoBehaviour
{
    // Instance
    #region 
    private static Shooter instance;
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
    public static Shooter GetInstance(){ return instance; }
    #endregion

    // Reference Object
    #region
    // Ball Spawn Point Transform
    #region 
    [SerializeField] private Transform ballSpawnPointTransform;
    public Transform GetBallSpawnPointTransform()
    {
        if(ballSpawnPointTransform == null)
        {
            Debug.Log("Missing Ball Spawn Point Transform Reference!");
        }
        return ballSpawnPointTransform;
    }
    #endregion

    [SerializeField] private GameObject ball;
    #endregion

    // Valuables
    #region 
    [SerializeField][Range(6f, 10f)] private float shootPower = 6f;
    private const float MAX_SHOOTPOWER = 10f;
    public void SetShootPower(float percentage)
    {
        shootPower = MAX_SHOOTPOWER * percentage;
    }

    [SerializeField] private float rotationOffset = 90f;
    [SerializeField] private float minDeviate = -0.25f;
    [SerializeField] private float maxDeviate = 0.25f;
    #endregion

    // Component
    #region 
    private void SetComponent()
    {
        SetShooterTransform();
    } 

    // Shooter Transform
    #region 
    private Transform shooterTransform;
    private void SetShooterTransform()
    {
        shooterTransform = this.transform.parent.transform;
    }
    private Transform GetShooterTransform(){ return shooterTransform; }
    #endregion

    #endregion

    // Method
    #region 
    // Update Shooter Rotation
    #region
    private void UpdateShooterRotation()
    {
        //Vector3 mousePosition = Input.mousePosition;
        //Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 pivotVector = new Vector2(GetShooterTransform().position.x, GetShooterTransform().transform.position.y);
        Vector2 aimPointVector = new Vector2(AimPointHandler.GetInstance().GetAimPointTransform().position.x, AimPointHandler.GetInstance().GetAimPointTransform().position.y);
        Vector3 direction = aimPointVector - pivotVector;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + rotationOffset;
        //Debug.Log(angle);
        //Debug.DrawLine(pivotVector, new Vector2(GetShooterTransform().position.x, GetShooterTransform().position.y + 10f), Color.yellow);
        //Debug.DrawLine(pivotVector, aimPointVector, Color.blue);
        GetShooterTransform().eulerAngles = Vector3.forward * angle;
    } 
    #endregion

    // Shoot Ball
    #region 
    public void ShootBall()
    {
        if (!TrayBox.GetInstance().RemoveBallInTray())
        {
            Debug.Log("Tray Box remove Ball fail!");
            return;
        }
        
        GameObject newBall = Instantiate(ball);
        newBall.transform.SetPositionAndRotation(ballSpawnPointTransform.position, Quaternion.Euler(0f, 0f, 0f));
        Rigidbody2D rb = newBall.GetComponent<Rigidbody2D>();
        Vector3 direction = (AimPointHandler.GetInstance().GetAimPointTransform().position - ballSpawnPointTransform.position).normalized;
        Vector2 newDirection = direction * Deviation.DeviateNumber(shootPower, minDeviate, maxDeviate);
        rb.AddForce(newDirection, ForceMode2D.Impulse);
    }
    #endregion

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
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateShooterRotation();
    }
}
