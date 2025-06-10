using UnityEngine;
using UnityEngine.UI;

namespace Quest_Studio
{
    public class LeaveGameWindow : MonoBehaviour
    {
        // Confirm Button
        #region
        [Header("Confirm Button")]
        [SerializeField] private Button confirmBtn;
        private Button GetConfirmBtn()
        { 
            if (confirmBtn == null) { Debug.Log("Missing Confirm Button Reference!"); }
            return confirmBtn;
        }

        public virtual void OnClickedConfirmBtn()
        {
            Application.Quit();
        }
        #endregion

        // Return Button
        #region
        [Header("Return Button")]
        [SerializeField] private Button returnBtn;
        private Button GetReturnBtn()
        { 
            if (returnBtn == null) { Debug.Log("Missing Return Button Reference!"); }
            return returnBtn;
        }

        public virtual void OnClickedReturnBtn()
        {
            this.gameObject.SetActive(false);
        }
        #endregion

        // Method
        #region 
        // Set Listener
        #region 
        private void SetListener()
        {
            // Confirm Button
            #region 
            GetConfirmBtn().onClick.AddListener(OnClickedConfirmBtn);
            #endregion

            // Return Button
            #region 
            GetReturnBtn().onClick.AddListener(OnClickedReturnBtn);
            #endregion
        }
        #endregion

        #endregion

        public virtual void Awake()
        {
            // Set Listener
            #region 
            SetListener();
            #endregion

        }

        public virtual void Start()
        {
            this.gameObject.SetActive(false);

        }

    }
}