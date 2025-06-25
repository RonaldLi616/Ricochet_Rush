using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealthHandler : MonoBehaviour
{   
     // Instance
    #region 
    public static PlayerHealthHandler instance = null;
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
    public static PlayerHealthHandler GetInstance() { return instance; }
    #endregion

    // Heart Locaker
    #region 
    [Header("Reference Heart Locker")]
    [SerializeField] private GameObject heartLockerPF;
    private GameObject GetHeartLocker()
    {
        if (heartLockerPF == null) { Debug.Log("Missing Heart Locker Prefab!"); }
        return heartLockerPF;
    }
    #endregion

    // Valuable
    #region 
    [Header("Valuable")]
    // Player Max Health
    #region 
    [SerializeField][Min(0)] private int playerMaxHealth;
    private void SetPlayerMaxHealth(int maxHealth) { playerMaxHealth = maxHealth; }

    #endregion

    // Player Health
    #region 
    [SerializeField][Min(0)] private int playerHealth;
    private void SetPlayerHealth(int health) { playerHealth = health; }
    #endregion

    #endregion

    // Method
    #region 
    // Instantiate Heart Lockers
    #region 
    private void InstantiateHeartLocker(bool isBroken)
    {
        GameObject go = Instantiate(GetHeartLocker(), this.transform);
        HeartLocker heartLocker = go.GetComponent<HeartLocker>();
        if (heartLocker == null)
        {
            Debug.Log("Missing Heart Locker Script!");
            return;
        }

        // Set Heart Locker Image
        #region 
        heartLocker.SetIsBroken(isBroken);
        heartLocker.SetHeartLockerImage();
        #endregion

        if (isBroken)
        {
            go.transform.SetAsLastSibling();
        }
        else
        {
            go.transform.SetAsFirstSibling();
        }
    }
    #endregion

    // Setup
    #region 
    private void Setup()
    {
        // Clean up
        #region 
        if (this.transform.childCount != 0) {
            for (int i = 0; i < this.transform.childCount; i++)
            {
                Destroy(this.transform.GetChild(i).gameObject);
            }
        }
        
        #endregion

        for (int i = 0; i < playerMaxHealth; i++)
        {
            bool isBroken = false;
            if (i > playerHealth) { isBroken = true; }
            InstantiateHeartLocker(isBroken);
        }
    }
    #endregion

    // Add Health
    #region 
    public void AddHealth(int number)
    {
        if (playerHealth >= playerMaxHealth) { return; }
        int result = playerHealth + number;
        if (result >= playerMaxHealth) { result = playerMaxHealth; }
        SetPlayerHealth(result);
        UpdateHeartLockersImage();
    }
    #endregion

    // Remove Health
    #region 
    public void RemoveHealth(int number)
    {
        if (playerHealth <= 0) { return; }
        int result = playerHealth - number;
        if (result < 0) { result = 0; }
        SetPlayerHealth(result);
        UpdateHeartLockersImage();
    }
    #endregion

    // Update Heart Lockers Image
    #region 
    private void UpdateHeartLockersImage()
    {
        for (int i = 0; i < playerMaxHealth; i++)
        {
            GameObject go = this.transform.GetChild(i).gameObject;
            HeartLocker heartLocker = go.GetComponent<HeartLocker>();
            if (heartLocker == null)
            {
                Debug.Log("Missing Heart Locker Script!");
                return;
            }

            bool isBroken = false;
            if (i > playerHealth) { isBroken = true; }
            heartLocker.SetIsBroken(isBroken);
            heartLocker.SetHeartLockerImage();
        }
    }
    #endregion

    #endregion

    private void Awake()
    {
        // Instance
        #region 
        SetInstance();
        #endregion

        Setup();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) { AddHealth(1); }
        if (Input.GetKeyDown(KeyCode.D)) { RemoveHealth(1); }
    }
}
