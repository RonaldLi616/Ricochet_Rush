using UnityEngine;
using UnityEngine.UI;

namespace Quest_Studio
{
    public class RadialProgressBar : MonoBehaviour
    {
        // Image
        #region 
        [Header("Image")]
        [SerializeField] private Image image;
        private Image GetImage()
        {
            if (image == null) { Debug.Log("Missing Image Reference!"); }
            return image;
        }
        public void SetFillAmount(float fillAmount){ GetImage().fillAmount = fillAmount; }
        public float GetFillAmount() { return GetImage().fillAmount; }
        #endregion

        // Text
        #region 
        [Header("Text")]
        [SerializeField] private Text showText;
        public void SetText(string text)
        {
            if (showText == null)
            {
                Debug.Log("Missing Text Reference!");
                return;
            }
            this.showText.text = text;
        }
        #endregion

        // Valuable
        #region 
        [Header("Valuable")]
        [SerializeField][Range(0f, 1f)] private float number = 0f;
        #endregion

        // Method
        #region 
        private void Temporary()
        {
            SetFillAmount(number);
            SetText(number.ToString());
        }
        #endregion

        private void Update()
        {
            //Temporary();
        }
    }
}
