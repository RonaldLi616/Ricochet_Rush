using UnityEngine;

public class Ball : MonoBehaviour
{
    // Valuables
    #region
    
    private Vector3 lastVelocity;
    private int value = 0;

    
    #endregion

    // Component
    #region 
    private Rigidbody2D rb;

    // PhysicsMaterial2D
    #region 
    private PhysicsMaterial2D pm;
    private void SetPhysicsMaterial()
    {
        pm = rb.sharedMaterial;
        if(pm == null)
        {
            Debug.Log(this.gameObject.name + " : Missing PhysicsMaterial!");
            return;
        }else
        {
            SetFirstBounsiness();
        }
    }

    private float firstBounsiness;
    private void SetFirstBounsiness(){ firstBounsiness = pm.bounciness; }
    private float GetFirstBousiness(){ return firstBounsiness; }
    #endregion

    #endregion

    // Reference Object
    #region 
    public GameObject spawnPoint = null;
    #endregion

    public Vector3 GetLastVelocity(){ return lastVelocity; }

    // Method
    #region 
    // Check Status
    #region 
    public enum InTrayStatus
    {
        Await,
        Ready
    }
    [SerializeField] private InTrayStatus inTrayStatus = InTrayStatus.Await;
    public InTrayStatus GetBallInTrayStatus() { return inTrayStatus; }

    private void CheckBallInTrayStatus(Collider2D collision)
    {
        TrayBox trayBox = collision.gameObject.GetComponent<TrayBox>();
        if (trayBox == null)
        {
            inTrayStatus = InTrayStatus.Await;
            return;
        }
        inTrayStatus = InTrayStatus.Ready;
    }
    #endregion

    #endregion

    // Collision Handler
    #region
    private void OnCollisionStay2D(Collision2D collision)
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        CheckBallInTrayStatus(collision);
    }
    #endregion

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
        SetPhysicsMaterial();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        lastVelocity = rb.linearVelocity;
    }

    

    // Change Material
    #region 
    public void ResetBousiness()
    {
        ChangeBounciness(GetFirstBousiness());
    }
    public void ChangeBounciness(float bounciness)
    {
        pm.bounciness = bounciness;
    }
    #endregion

}
