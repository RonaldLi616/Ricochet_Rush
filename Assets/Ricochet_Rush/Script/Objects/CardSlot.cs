using UnityEngine;
using Quest_Studio;

public class CardSlot : MonoBehaviour
{
    // Instance
    #region 
    public static CardSlot instance = null;
    private void SetInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public static CardSlot GetInstance() { return instance; }
    #endregion

    // Method
    #region
    
    #endregion

    private void Awake()
    {
        // Instance
        #region 
        SetInstance();
        #endregion

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
