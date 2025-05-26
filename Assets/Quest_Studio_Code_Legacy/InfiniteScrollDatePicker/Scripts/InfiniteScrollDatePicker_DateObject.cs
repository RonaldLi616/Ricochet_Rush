using UnityEngine;
using UnityEngine.UI;
using Quest_Studio;

namespace Quest_Studio
{
    public class InfiniteScrollDatePicker_DateObject : MonoBehaviour
    {
        // Reference Components
        #region
        [Header("Reference Components")]
        // - Infinite Scroll Date Picker -
        #region
        private InfiniteScrollDatePicker infiniteScrollDatePicker;
        private InfiniteScrollDatePicker GetInfiniteScrollDatePicker()
        {
            if (infiniteScrollDatePicker == null)
            {
                Debug.Log("Missing infinite scroll date picker reference!");
                return null;
            }
            return infiniteScrollDatePicker;
        }
        public void SetInfiniteScrollDatePicker(InfiniteScrollDatePicker infiniteScrollDatePicker) { this.infiniteScrollDatePicker = infiniteScrollDatePicker; }
        private string GetStringByValue() { return GetInfiniteScrollDatePicker().GetValueStringArray()[GetDateValue()]; }
        #endregion

        #endregion

        // Components
        #region
        [Header("Components")]
        // - Date Text -
        #region
        [SerializeField]
        private Text dateText;
        private Text GetDateText()
        {
            if (dateText == null)
            {
                Debug.Log("Missing date text reference!");
                return null;
            }
            return dateText;
        }
        private void SetText()
        {
            GetDateText().text = GetStringByValue();
        }
        public void SetTextColor(bool isSelected)
        {
            GetDateText().color = (isSelected == true ? selectedColor : deselectColor);
        }
        #endregion

        #endregion

        // Color
        #region
        [Header("Color")]
        [SerializeField]
        private Color32 selectedColor;
        [SerializeField]
        private Color32 deselectColor;
        #endregion

        // Variables
        #region
        // - Date Value -
        #region
        private int dateValue = 0;
        public int GetDateValue() { return dateValue; }
        public void SetDateValue(int dateValue)
        {
            this.dateValue = dateValue;
            SetText();
        }
        #endregion

        #endregion

        private void Awake()
        {
            SetTextColor(false);
        }
    }
}