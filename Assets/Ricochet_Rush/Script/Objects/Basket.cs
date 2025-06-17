using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class Basket : MonoBehaviour
{
    // Valuable
    #region 
    [Header("Valuable")]
    [SerializeField] private int ballNumber = 0;
    #endregion

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();
        if (ball == null) { return; }
        
        Destroy(collision.gameObject);
        Dropper.GetInstance().AddBallInTray(ballNumber);
    }
}
