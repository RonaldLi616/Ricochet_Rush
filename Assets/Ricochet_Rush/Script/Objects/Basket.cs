using UnityEngine;

public class Basket : MonoBehaviour
{
    // Valuable
    #region 
    [SerializeField] private int ballNumber = 0;
    #endregion

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log("Destroy");
        Destroy(collision.gameObject);
        Dropper.GetInstance().AddBallInTray(ballNumber);
    }
}
