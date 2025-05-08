using UnityEngine;

public class Bounce : MonoBehaviour
{
    // Valuables
    #region 
    [SerializeField]private float power = 0f;    
    #endregion

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Bounce!");
        StartBounceAnimation();
        BounceObject(collision);
    }

    // Bounce
    #region 
    private void BounceObject(Collision2D collision)
    {
        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
        Ball ball = collision.gameObject.GetComponent<Ball>();
        var speed = ball.GetLastVelocity().magnitude;
        Vector3 direction = Vector3.Reflect(ball.GetLastVelocity().normalized, collision.contacts[0].normal);
        rb.linearVelocity = direction * Mathf.Max(speed, power);
    }
    #endregion

    // Animation
    #region 
    private void StartBounceAnimation()
    {
        Animator animator = this.GetComponent<Animator>();
        if(animator!= null)
        {

        }
    }
    #endregion
}
