using UnityEngine;

public class Ball : MonoBehaviour
{
    // Valuables
    #region
    private Rigidbody2D rb;
    private Vector3 lastVelocity;
    private int value = 0;
    public GameObject spawnPoint = null;
    #endregion

    public Vector3 GetLastVelocity(){ return lastVelocity; }

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
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

}
