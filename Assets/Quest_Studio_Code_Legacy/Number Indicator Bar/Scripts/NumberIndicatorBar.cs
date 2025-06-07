using UnityEngine;
using UnityEngine.UI;

namespace Quest_Studio
{
    [RequireComponent(typeof(Slider))]
    [ExecuteAlways]
    public class NumberIndicatorBar : MonoBehaviour
    {
        // Valuable
        #region 
        [Header("Valuable")]
        [SerializeField][Min(0f)] private float maximum;
        public void SetMaximum(float maximum) { this.maximum = maximum; }
        [SerializeField][Min(0f)] private float currentNumber;
        public void SetCurrentNumber(float currentNumber) { this.currentNumber = currentNumber; }
        #endregion

        // Component
        #region 
        private void SetComponent()
        {
            SetSlider();
        }

        [Header("Component")]

        // Slider
        #region 
        [SerializeField] private Slider slider;
        private void SetSlider(){ slider = this.GetComponent<Slider>(); }
        private void SetSliderValue()
        {
            slider.maxValue = maximum;
            slider.value = currentNumber;

            SetFillColor();
        }
        public virtual void SetSliderValue(float value)
        {
            slider.value = value;
            SetFillColor();        
        }
        #endregion

        // Gradient
        #region 
        [SerializeField] private Gradient gradient;
        #endregion

        // Fill Raw Image
        #region 
        [Tooltip("Change fill color with gradient.")]
        [SerializeField] private RawImage fillRI;
        private void SetFillColor()
        {
            if (fillRI == null || gradient == null) { return; }
            fillRI.color = gradient.Evaluate(slider.normalizedValue);
        }
        #endregion

        // Text
        #region
        [Tooltip("Current Value / Maximum Text")]
        [SerializeField] private Text indicatorText;
        public Text GetIndicatorText()
        {
            if (indicatorText == null)
            {
                Debug.Log("Missing Indicator Text Reference!");
                return null;
            }
            return indicatorText;
        }

        public void SetIndicatorText(string text)
        {
            if (indicatorText == null) { return; }
            indicatorText.text = text;
        }
        #endregion

        #endregion

        // Method
        #region 
        public virtual void SetNumber() { }
        #endregion

        public virtual void OnValidate()
        {
            // Set Component
            #region 
            SetComponent();
            #endregion

        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        public virtual void Start()
        {
            SetSliderValue();
        }

        // Update is called once per frame
        public virtual void Update()
        {

        }
    }
}