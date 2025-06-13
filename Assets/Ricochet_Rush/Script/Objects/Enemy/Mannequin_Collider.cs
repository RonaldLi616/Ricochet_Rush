using UnityEngine;

public class Mannequin_Collider : ColliderHandler
{
    // Reference Script
    #region 
    [Header("Reference Script")]
    [SerializeField] private Mannequin mannequin;
    private Mannequin GetMannequin()
    {
        if (mannequin == null) { Debug.Log("Missing Mannequin Script!"); }
        return mannequin;
    }
    #endregion

    [SerializeField][Range(0f, 1f)] private float weight = 1f;

    // Collision Handler
    #region 
    public override void OnCollisionEnter2D(Collision2D collision)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();
        if (ball == null) { return; }

        // Count Magnitude
        #region 
        Vector2 contactPoint = collision.GetContact(0).point;
        Vector2 direction = (contactPoint - (Vector2)ball.transform.position).normalized;
        var velocity = ball.GetComponent<Rigidbody2D>().GetPointVelocity(transform.TransformPoint(ball.transform.position));
        Vector2 magnitude = direction * velocity * weight;
        #endregion

        GetMannequin().OnHitMannequin(magnitude);
    }
    #endregion
}
