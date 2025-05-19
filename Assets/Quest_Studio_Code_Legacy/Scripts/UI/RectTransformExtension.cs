using UnityEngine;
using UnityEngine.UI;

namespace Quest_Studio
{
    public class RectTransformExtension
    {
        // Enum
        #region
        public enum RectTransformType { Horizontal, Vertical }
        #endregion

        // Method
        #region
        // - Check Overflow -
        #region
        public static bool IsOverflowingHorizontal(RectTransform rectTransform)
        {
            return LayoutUtility.GetPreferredWidth(rectTransform) > GetCalculatedPermissibleWidth(rectTransform);
        }
        public static bool IsOverflowingVertical(RectTransform rectTransform)
        {
            return LayoutUtility.GetPreferredHeight(rectTransform) > GetCalculatedPermissibleHeight(rectTransform);
        }

        private static float GetCalculatedPermissibleWidth(RectTransform rectTransform)
        {
            if (cachedCalculatedPermissiblWidth != -1) return cachedCalculatedPermissiblWidth;

            cachedCalculatedPermissiblWidth = rectTransform.rect.width;
            return cachedCalculatedPermissiblWidth;
        }
        private static float GetCalculatedPermissibleHeight(RectTransform rectTransform)
        {
            if (cachedCalculatedPermissibleHeight != -1) return cachedCalculatedPermissibleHeight;

            cachedCalculatedPermissibleHeight = rectTransform.rect.height;
            return cachedCalculatedPermissibleHeight;
        }
        private static float cachedCalculatedPermissiblWidth = -1;
        private static float cachedCalculatedPermissibleHeight = -1;
        #endregion


        // - Resize Container -
        #region
        public static void ResizePreferredSize(RectTransform rectTransform, RectTransformType rectTransformType)
        {
            float originalWidth = rectTransform.sizeDelta.x;
            float originalHeight = rectTransform.sizeDelta.y;
            float preferredWidth = LayoutUtility.GetPreferredWidth(rectTransform);
            float preferredHeight = LayoutUtility.GetPreferredHeight(rectTransform);
            switch (rectTransformType)
            {
                case RectTransformType.Horizontal:
                    rectTransform.sizeDelta = new Vector2(preferredWidth + 50f, originalHeight);
                    break;

                case RectTransformType.Vertical:
                    rectTransform.sizeDelta = new Vector2(originalWidth, preferredHeight + 50f);
                    break;
            }
        }
        #endregion

        // - Check Preferred Size -
        #region
        public static bool IsOverPreferredSize(RectTransform rectTransform, RectTransformType rectTransformType)
        {
            bool isOver;
            float originalWidth = rectTransform.sizeDelta.x;
            float originalHeight = rectTransform.sizeDelta.y;
            float preferredWidth = LayoutUtility.GetPreferredWidth(rectTransform);
            float preferredHeight = LayoutUtility.GetPreferredHeight(rectTransform);
            switch (rectTransformType)
            {
                default:
                case RectTransformType.Horizontal:
                    if (originalWidth >= preferredWidth)
                    {
                        isOver = false;
                    }
                    else
                    {
                        isOver = true;
                    }
                    break;

                case RectTransformType.Vertical:
                    if (originalHeight >= preferredHeight)
                    {
                        isOver = false;
                    }
                    else
                    {
                        isOver = true;
                    }
                    break;
            }
            return isOver;
        }
        #endregion

        #endregion
    }
}