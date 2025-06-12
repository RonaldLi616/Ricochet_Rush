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

    // Collision Handler
    #region 
    public override void OnCollisionEnter2D(Collision2D collision)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();
        if (ball == null) { return; }

        GetMannequin().OnHitMannequin();
    }
    #endregion
}
