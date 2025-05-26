using UnityEngine;
using Quest_Studio;

namespace Quest_Studio
{
    public class InfiniteScrollDatePicker_CenterTrigger : MonoBehaviour
    {
        // Reference Components
        #region
        [Header("Reference Components")]
        // - Custom Infinite Scroll -
        #region
        [SerializeField]
        private CustomInfiniteScroll customInfiniteScroll;
        private CustomInfiniteScroll GetCustomInfiniteScroll()
        {
            if (customInfiniteScroll == null)
            {
                Debug.Log("Missing custom infinite scroll reference!");
                return null;
            }
            return customInfiniteScroll;
        }
        private Vector2 GetScrollContentPosition() { return GetCustomInfiniteScroll().GetScrollContent().position; }
        #endregion

        #endregion

        // Variables
        #region
        [Header("Variables")]
        // - Value -
        #region
        [SerializeField]
        private int value = 0;
        public int GetValue() { return value; }
        private void SetValue(int value) { this.value = value; }
        #endregion

        // - Position -
        #region
        [SerializeField]
        private Vector2 position;
        public Vector2 GetPosition() { return position; }
        private void SetPostion(Vector2 position) { this.position = position; }
        #endregion

        #endregion

        private void OnTriggerEnter2D(Collider2D collision)
        {
            InfiniteScrollDatePicker_DateObject dateObject = collision.GetComponent<InfiniteScrollDatePicker_DateObject>();
            dateObject.SetTextColor(true);
            SetPostion(GetScrollContentPosition());
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            InfiniteScrollDatePicker_DateObject dateObject = collision.GetComponent<InfiniteScrollDatePicker_DateObject>();
            dateObject.SetTextColor(true);
            SetValue(dateObject.GetDateValue());
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            InfiniteScrollDatePicker_DateObject dateObject = collision.GetComponent<InfiniteScrollDatePicker_DateObject>();
            dateObject.SetTextColor(false);
        }
    }
}