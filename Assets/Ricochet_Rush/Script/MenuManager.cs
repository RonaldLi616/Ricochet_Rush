using Quest_Studio;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    // Instance
    #region 
    public static MenuManager instance = null;
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
    public static MenuManager GetInstance() { return instance; }
    #endregion

    // Reference Option Menu
    #region 
    [Header("Option Menu")]
    [SerializeField] private OptionManager optionManager;
    private OptionManager GetOptionManager()
    {
        if (optionManager == null) { Debug.Log("Missing Option Manager Reference!"); }
        return optionManager;
    }

    #endregion

    // Reference Leave Game Window
    #region 
    [Header("Leave Game Window")]
    [SerializeField] private LeaveGameWindow leaveGameWindow;
    private LeaveGameWindow GetLeaveGameWindow()
    {
        if (leaveGameWindow == null) { Debug.Log("Missing Leave Game Window Reference!"); }
        return leaveGameWindow;
    }
    #endregion

    // Button
    #region
    private void AddListener()
    {
        GetStartButton().onClick.AddListener(OnClickedStartButton);
        GetOptionButton().onClick.AddListener(OnClickedOptionButton);
        GetExitButton().onClick.AddListener(OnClickedExitButton);
    }

    [Header("Menu Button")]
    // Start Button
    #region 
    [SerializeField] private Button startBtn;
    private Button GetStartButton()
    {
        if (startBtn == null){ Debug.Log("Missing Start Button Reference!"); }
        return startBtn;
    }
    private void OnClickedStartButton() { RicochetRush_SceneLoader.Load(RicochetRush_SceneLoader.Scene.TestingGround); }
    #endregion

    // Option Button
    #region 
    [SerializeField] private Button optionBtn;
    private Button GetOptionButton()
    {
        if (optionBtn == null){ Debug.Log("Missing Option Button Reference!"); }
        return optionBtn;
    }
    private void OnClickedOptionButton()
    {
        GetOptionManager().gameObject.SetActive(true);
    }
    #endregion

    // Exit Button
    #region 
    [SerializeField] private Button exitBtn;
    private Button GetExitButton()
    {
        if (exitBtn == null){ Debug.Log("Missing Exit Button Reference!"); }
        return exitBtn;
    }
    private void OnClickedExitButton() { GetLeaveGameWindow().gameObject.SetActive(true); }
    #endregion

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

        // Add Listener
        #region 
        AddListener();
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
