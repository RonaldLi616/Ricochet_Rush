using UnityEngine;
using UnityEngine.UI;
using Quest_Studio;

namespace Quest_Studio
{
    [RequireComponent(typeof(Button))]
    [ExecuteAlways]
    public class OptionButton : MonoBehaviour
    {
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

        // Option Button
        #region
        [Header("Option Button")]
        private Button optionBtn;
        private void SetOptionButton() { optionBtn = this.gameObject.GetComponent<Button>(); }
        private Button GetOptionBtn()
        {
            if (optionBtn == null) { Debug.Log("Missing Option Button Reference!"); }
            return optionBtn;
        }

        public virtual void OnClickedOptionBtn()
        {
            GetOptionManager().gameObject.SetActive(true);
        }
        #endregion

        // Method
        #region 
        // Set Listener
        #region 
        private void SetListener()
        {
            GetOptionBtn().onClick.AddListener(OnClickedOptionBtn);
        }
        #endregion

        #endregion

        public virtual void OnValidate()
        {
            // Set Component
            #region 
            SetOptionButton();
            #endregion

            SetListener();
        }

        /*public void Resume()
        {
            optionMenuUI.SetActive(false);
            Time.timeScale = 1f;
            audioManager.SetPitch("Theme", 1f);
            //this.GetComponent<Button>().interactable = true;
            optionButton.interactable = true;
        }

        public void Pause()
        {
            optionMenuUI.SetActive(true);
            Time.timeScale = 0f;
            audioManager.SetPitch("Theme", .98f);
            //this.GetComponent<Button>().interactable = false;
            optionButton.interactable = false;
        }*/
    }
}