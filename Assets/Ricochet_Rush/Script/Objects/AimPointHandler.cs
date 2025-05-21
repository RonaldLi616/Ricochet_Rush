using UnityEngine;

public class AimPointHandler : MonoBehaviour
{
    // Instance
    #region 
    private static AimPointHandler instance;
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
    public static AimPointHandler GetInstance(){ return instance; }
    #endregion

    // Valuables
    #region 

    #endregion

    // Component
    #region 
    private void SetComponent()
    {
        SetAimPointTransform();
    }

    // Mouse
    #region 
    public Vector3 GetMousePosition(){ return GameManager.GetInstance().GetMousePosition(); }
    public Vector3 GetMouseWorldPosition()
    {
        return GameManager.GetInstance().GetMouseWorldPosition();
    }
    #endregion

    // Aim Point Transform
    #region 
    [SerializeField]private Transform aimPointTransform;
    private void SetAimPointTransform(){ aimPointTransform = this.transform.GetChild(0).transform; }
    public Transform GetAimPointTransform(){ return aimPointTransform; }
    #endregion

    #endregion

    // Method
    #region
    // Update Aim Point Position
    #region
    [SerializeField][Min(0f)]private float maxDistance = 10f;

    private void UpdateAimPointPosition()
    {
        Transform ballSpawnPointTransform = Shooter.GetInstance().GetBallSpawnPointTransform();
        Vector3 startPosition = new Vector3(ballSpawnPointTransform.position.x, ballSpawnPointTransform.position.y, 0f);
        Vector3 heading = GetMouseWorldPosition() - startPosition;
        Vector3 direction = heading.normalized;
        float distance = heading.magnitude;
        Vector3 newPosition;
        if(distance >= maxDistance)
        {
            newPosition = startPosition + (direction * maxDistance);
            UpdateShootPower(maxDistance);
        }else{
            newPosition = GetMouseWorldPosition();
            UpdateShootPower(distance);
        }
        GetAimPointTransform().SetPositionAndRotation(newPosition, Quaternion.Euler(0f, 0f, 0f));
    }
    #endregion

    // Update Shoot Power by Distance
    #region
    private void UpdateShootPower(float distance)
    {
        float percentage = distance / maxDistance;
        Shooter.GetInstance().SetShootPower(percentage);
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
        UpdateAimPointPosition();
    }
}
