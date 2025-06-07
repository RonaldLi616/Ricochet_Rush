using UnityEngine;

public class BlankCard : MonoBehaviour
{
    // Instance
    #region 
    public static BlankCard instance = null;
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
    public static BlankCard GetInstance() { return instance; }
    #endregion

    // Method
    #region 
    public void SetToLastSibling()
    {
        this.transform.SetAsLastSibling();
    }
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
        SetToLastSibling();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
