using UnityEngine;

public class Basket : MonoBehaviour
{
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
        GameManager.instance.AllBallRemain(5);
    }
}
