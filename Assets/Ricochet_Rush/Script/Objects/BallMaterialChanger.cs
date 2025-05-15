using UnityEngine;

public class BallMaterialChanger : MonoBehaviour
{
    // Valuable
    #region 
    [SerializeField][Min(0f)]private float bounciness;
    [SerializeField]private bool isReset;
    #endregion

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();
        if(ball == null)
        {
            return;
        }
        if(isReset)
        {
            ball.ResetBousiness();
        }else
        {
            ball.ChangeBounciness(bounciness);
        }
        
    }
}
