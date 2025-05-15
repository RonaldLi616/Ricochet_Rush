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

    #region
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Hit!");
        /*Transform transform = this.gameObject.GetComponent<Transform>();
        transform.position = spawnPoint.transform.position;*/
    }
    #endregion

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
