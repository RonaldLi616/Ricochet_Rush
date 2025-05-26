using UnityEngine;
using UnityEngine.UI;
using Quest_Studio;

namespace Quest_Studio
{
    public class InfiniteScrollDatePicker : MonoBehaviour
    {
        // Reference Prefabs
        #region
        [Header("Reference Prefabs")]
        [SerializeField]
        private GameObject dateObjectPF;
        private GameObject GetDateObjectPF()
        {
            if (dateObjectPF == null)
            {
                Debug.Log("Missing date object prefab reference!");
                return null;
            }
            return dateObjectPF;
        }
        #endregion

        // Reference Component
        #region
        [Header("Reference Components")]
        // - Center -
        #region
        [SerializeField]
        private InfiniteScrollDatePicker_CenterTrigger centerTrigger;
        private InfiniteScrollDatePicker_CenterTrigger GetCenterTrigger()
        {
            if (centerTrigger == null)
            {
                Debug.Log("Missing center trigger reference!");
                return null;
            }
            return centerTrigger;
        }
        #endregion

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
        #endregion

        // -- Scroll Rect --
        #region
        private ScrollRect GetScrollRect() { return GetCustomInfiniteScroll().GetComponent<ScrollRect>(); }
        #endregion

        #endregion


        // Variables
        #region
        [Header("Variables")]
        // - Value String Array -
        #region
        [SerializeField]
        private string[] valueStringArray = new string[0];
        public string[] GetValueStringArray() { return valueStringArray; }
        public void SetValueStringArray(string[] valueStringArray)
        {
            this.valueStringArray = new string[0];
            this.valueStringArray = valueStringArray;
            InstaniateDateObjects();
        }
        #endregion

        // - Choosen Value -
        #region
        [SerializeField]
        private int value;
        public int GetValue()
        {
            SetValue(GetCenterTrigger().GetValue());
            return value;
        }
        private void SetValue(int value) { this.value = value; }
        #endregion

        // - Do Once -
        #region
        private bool doOnce = false;
        private bool GetDoOnce() { return doOnce; }
        private void SetDoOnce(bool doOnce) { this.doOnce = doOnce; }
        #endregion

        // -  Value Changing -
        #region
        private bool valueChanging = false;
        private bool GetValueChanging() { return valueChanging; }
        private void SetValueChanging(bool valueChanging) { this.valueChanging = valueChanging; }
        private void CheckValueChange()
        {
            if (GetScrollRect().velocity.y != 0)
            {
                SetValueChanging(true);
            }
            else
            {
                SetValueChanging(false);
            }
        }
        #endregion

        // - Enable Animation -
        #region
        private bool enableAnimation = false;
        private bool GetEnableAnimation() { return enableAnimation; }
        private void SetEnableAnimation(bool enableAnimation) { this.enableAnimation = enableAnimation; }
        #endregion

        #endregion

        // Method
        #region
        // - Instaniate Date Objects -
        #region
        private void InstaniateDateObjects()
        {
            for (int i = 0; i < GetValueStringArray().Length; i++)
            {
                GameObject dateObjectGO = Instantiate(GetDateObjectPF(), GetCustomInfiniteScroll().GetScrollContent());
                InfiniteScrollDatePicker_DateObject dateObject = dateObjectGO.GetComponent<InfiniteScrollDatePicker_DateObject>();
                dateObject.SetInfiniteScrollDatePicker(this);
                dateObject.SetDateValue(i);
            }
            GetCustomInfiniteScroll().SetOrdinaryPosition();
        }
        #endregion

        #endregion

        // iTween Animation
        #region
        // - Move Scroll Content Position -
        #region
        private bool isRunningAnimation = false;
        private bool GetIsRunningAnimation() { return isRunningAnimation; }
        private void SetIsRunningAnimation(bool isRunningAnimation) { this.isRunningAnimation = isRunningAnimation; }
        private void MoveScrollContentPosition()
        {
            if (GetDoOnce()) { return; }
            SetDoOnce(true);

            //iTween.MoveTo(GetCustomInfiniteScroll().GetScrollContent().gameObject, GetCenterTrigger().GetPosition(), 0.2f);
            iTween.ValueTo(this.gameObject, iTween.Hash
                (
                    "name", "iTweenMoveScrollContentPosition",
                    "from", GetCustomInfiniteScroll().GetScrollContent().position.y,
                    "to", GetCenterTrigger().GetPosition().y,
                    "time", 0.1f,
                    "easetype", iTween.EaseType.easeInOutSine,
                    "onstart", "OnStartMoveScrollRectPosition",
                    "onupdate", "OnUpdateMoveScrollRectPositionValue",
                    "oncomplete", "OnCompleteMoveScrollRectPosition"
                ));
        }
        private void OnStartMoveScrollRectPosition()
        {
            SetEnableAnimation(false);
            SetIsRunningAnimation(true);
        }
        private void OnUpdateMoveScrollRectPositionValue(float value)
        {
            GetCustomInfiniteScroll().GetScrollContent().position = new Vector2(GetCustomInfiniteScroll().GetScrollContent().position.x, value);
        }
        private void OnCompleteMoveScrollRectPosition()
        {
            SetDoOnce(false);
            SetIsRunningAnimation(false);
        }
        #endregion

        #endregion

        // Debug
        #region

        #endregion

        private void Awake()
        {

        }

        private void Update()
        {
            // Debug
            #region
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //SetValueStringArray(debugArray);
            }
            if (Input.GetKeyDown(KeyCode.G))
            {
                //Debug.Log(GetValue());
            }
            #endregion

            CheckValueChange();

            if (GetCustomInfiniteScroll().isOnDrag)
            {
                if (GetIsRunningAnimation())
                {
                    iTween.StopByName("iTweenMoveScrollContentPosition");
                    SetDoOnce(false);
                    SetIsRunningAnimation(false);
                }
                SetEnableAnimation(true);
            }
            if (!GetCustomInfiniteScroll().isOnDrag && !GetValueChanging() && GetEnableAnimation()) { MoveScrollContentPosition(); }
        }
    }
}