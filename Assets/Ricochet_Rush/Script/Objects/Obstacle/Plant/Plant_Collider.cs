using System;
using UnityEngine;

public class Plant_Collider : ColliderHandler
{
    // Reference Script
    #region 
    [Header("Reference Script")]
    [SerializeField] private Plant plant;
    private Plant GetPlant()
    {
        if (plant == null) { Debug.Log("Missing Plant Script!"); }
        return plant;
    }
    #endregion

    // Type
    #region 
    private enum Type
    {
        BloomLeaf,
        LeafLeft,
        LeafRight,
        Bloom
    }

    [Header("Type")]
    [SerializeField] private Type type = Type.LeafLeft;
    #endregion

    // Weight
    #region 
    [Header("Weight")]
    [Tooltip("Effector animation weight")]
    [SerializeField][Range(0f, 1f)] private float weight = 1f;

    #endregion

    // Hit Once
    #region 
    private bool hitOnce = false;
    #endregion

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

        switch (type)
        {
            case Type.BloomLeaf:
                if (hitOnce) { return; }
                hitOnce = true;
                GetPlant().OnHitAnimation(magnitude, false);
                GetPlant().DestroyBloomLeaf(this.gameObject, magnitude);
                break;

            case Type.Bloom:
                GetPlant().OnHitAnimation(magnitude, true);
                break;

            case Type.LeafLeft:
                GetPlant().OnHitAnimation_Leaf(magnitude, true);
                break;

            case Type.LeafRight:
                GetPlant().OnHitAnimation_Leaf(magnitude, false);
                break;

            default:

                break;
        }

    }
    #endregion
    
}
