using UnityEngine;

public class Void_Area : MonoBehaviour
{
    // Instance
    #region 
    public static Void_Area instance = null;
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
    public static Void_Area GetInstance() { return instance; }
    #endregion

    // Valuable
    #region 
    [Header("Valuable")]
    [SerializeField][Min(0)] private int m_DestroyCount = 0;
    [Min(0)]
    public int destroyCount
    {
        get { return m_DestroyCount; }
        set
        {
            if (m_DestroyCount == value) { return; }
            m_DestroyCount = value;
            if (OnDestryCountChange != null) { OnDestryCountChange(m_DestroyCount); }
        }
    }
    #endregion

    public delegate void OnDestryCountChangeDelegate(int value);
    public event OnDestryCountChangeDelegate OnDestryCountChange;

    private void OnTriggerExit2D(Collider2D collision)
    {
        Ball ball = collision.GetComponent<Ball>();
        if (ball != null)
        {
            destroyCount++;
        }

        Destroy(collision.gameObject);
    }

    private void Awake()
    {
        // Instance
        #region 
        SetInstance();
        #endregion

    }
}
