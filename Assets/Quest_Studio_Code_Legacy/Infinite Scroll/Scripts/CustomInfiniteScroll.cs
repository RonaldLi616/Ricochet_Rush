using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Quest_Studio;

namespace Quest_Studio
{
    public class CustomInfiniteScroll : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IScrollHandler
    {
        // References
        [SerializeField]
        private ScrollRect scrollRect;

        // Components
        [SerializeField]
        private RectTransform scrollViewRT;
        [SerializeField]
        private RectTransform scrollContentRT;
        public RectTransform GetScrollContent()
        {
            return scrollContentRT;
        }

        // - Point Locator -
        #region
        [SerializeField]
        private RectTransform pointLocator;
        private RectTransform top;
        private RectTransform bottom;
        private RectTransform left;
        private RectTransform right;
        #endregion

        // Variables
        // - ScrollRect Type -
        #region
        private enum ScrollRectType { Horizontal, Vertical, Both }
        private ScrollRectType scrollRectType;
        private void SetScrollRectType()
        {
            if (scrollRect.horizontal)
            {
                if (scrollRect.vertical)
                {
                    scrollRectType = ScrollRectType.Both;
                }
                else
                {
                    scrollRectType = ScrollRectType.Horizontal;
                }
            }
            else
            {
                if (scrollRect.horizontal)
                {
                    scrollRectType = ScrollRectType.Both;
                }
                else
                {
                    scrollRectType = ScrollRectType.Vertical;
                }
            }
        }
        #endregion

        // - Reference Left And Top -
        #region
        private float topPosY;
        private float bottomPosY;
        private float leftPosX;
        public float GetLeftPosX()
        {
            return leftPosX;
        }
        private float rightPosX;
        private void SetStartReference()
        {
            topPosY = (scrollViewRT.sizeDelta.y / 2);
            bottomPosY = -(scrollViewRT.sizeDelta.y / 2);
            leftPosX = /*-(scrollViewRT.sizeDelta.x / 2)*/0;
            rightPosX = (scrollViewRT.sizeDelta.x / 2);
        }
        #endregion

        // - Constant Border World Position -
        #region
        private float minX;
        private float minY;
        private float maxX;
        private float maxY;
        #endregion

        // - Threshold Border -
        #region
        [SerializeField]
        [Range(0f, 1f)] private float thresholdBorderValue;
        private float offsetMinX;
        public float GetOffsetMinX() { return offsetMinX; }
        private float offsetMaxX;
        public float GetOffsetMaxX() { return offsetMaxX; }
        private float offsetMinY;
        public float GetOffsetMinY() { return offsetMinY; }
        private float offsetMaxY;
        public float GetOffsetMaxY() { return offsetMaxY; }

        private void SetThresholdBorder()
        {
            float thresholdX = maxX * thresholdBorderValue;
            offsetMinX = minX - thresholdX;
            offsetMaxX = maxX + thresholdX;

            float thresholdY = maxY * thresholdBorderValue;
            offsetMinY = minY - thresholdY;
            offsetMaxY = maxY + thresholdY;
        }

        private bool IsReachedThreshold(ScrollObject scrollObject)
        {
            bool isReachedThreshold = false;

            if (scrollObject.GetPosX() > offsetMaxX || scrollObject.GetPosX() < offsetMinX || scrollObject.GetPosY() < offsetMinY || scrollObject.GetPosY() < offsetMinY)
            {
                isReachedThreshold = true;
            }

            return isReachedThreshold;
        }
        #endregion

        // - Item Spacing -
        #region
        [SerializeField]
        private float itemSpacing;
        public float GetItemSpacing()
        {
            return itemSpacing;
        }
        #endregion

        // - 2D Max Column & Roll Number -
        #region
        private int twoDimensionMaxColumn;
        public void SetMaxColumnNumber(int columnNumber)
        {
            this.twoDimensionMaxColumn = columnNumber;
        }
        private int twoDimensionMaxRow = 5;
        #endregion

        // - Drag Handler -
        #region
        public bool isOnDrag = false;
        public bool positiveDrag_Horizontal;
        public bool positiveDrag_Vertical;
        private Vector2 lastDragPosition;
        #endregion

        public ScrollObject[] scrollObjects;

        // Debug
        #region
        public bool isDebug = false;

        #endregion

        private void Awake()
        {
            SetScrollRectType();
            SetStartReference();
            if (CheckLocatorsReferenced())
            {
                SetDebug();
            }
        }

        private void Start()
        {
            if (scrollRectType == ScrollRectType.Both)
            {
                SetTwoDimensionGrid();
            }
        }

        private void Update()
        {
            if (CheckLocatorsReferenced())
            {
                SetDebug();
                FindBorderLocation();
            }
        }

        // Set & Get Border Locators
        #region
        private bool CheckLocatorsReferenced()
        {
            if (top != null && bottom != null && left != null && right != null)
            {
                return true;
            }
            else
            {
                SetLocators();
                return false;
            }
        }

        private void SetLocators()
        {
            top = pointLocator.transform.Find("Top").GetComponent<RectTransform>();
            bottom = pointLocator.transform.Find("Bottom").GetComponent<RectTransform>();
            left = pointLocator.transform.Find("Left").GetComponent<RectTransform>();
            right = pointLocator.transform.Find("Right").GetComponent<RectTransform>();
        }

        private void FindBorderLocation()
        {
            maxY = top.position.y;
            minY = bottom.position.y;
            minX = left.position.x;
            maxX = right.position.x;

            SetThresholdBorder();
        }
        #endregion

        public void SetOrdinaryPosition()
        {
            float posX = 0f;
            float posY = 0f;

            if (scrollContentRT.transform.childCount != 0)
            {
                for (int i = 0; i < scrollContentRT.transform.childCount; i++)
                {
                    RectTransform scrollObjectRT = scrollContentRT.transform.GetChild(i).GetComponent<RectTransform>();

                    switch (scrollRectType)
                    {
                        default:
                        case ScrollRectType.Horizontal:
                            if (i == 0) // First Object Center
                            {
                                posX = leftPosX + (scrollObjectRT.sizeDelta.x / 2);
                            }
                            else
                            {
                                int previousIndex = i - 1;
                                RectTransform previousRT = scrollContentRT.transform.GetChild(previousIndex).GetComponent<RectTransform>();
                                float previousPosX = previousRT.anchoredPosition.x;
                                float previousWidth = previousRT.sizeDelta.x;
                                float width = scrollObjectRT.sizeDelta.x;
                                posX = previousPosX + (previousWidth / 2) + itemSpacing + (width / 2);
                            }
                            break;

                        case ScrollRectType.Vertical:
                            if (i == 0) // First Object Center
                            {
                                posY = topPosY + (scrollObjectRT.sizeDelta.y / 2);
                            }
                            else
                            {
                                int previousIndex = i - 1;
                                RectTransform previousRT = scrollContentRT.transform.GetChild(previousIndex).GetComponent<RectTransform>();
                                float previousPosY = previousRT.anchoredPosition.y;
                                float previousHeight = previousRT.sizeDelta.y;
                                float height = scrollObjectRT.sizeDelta.y;
                                posY = previousPosY - (previousHeight / 2) - itemSpacing - (height / 2);
                            }
                            break;
                    }
                    scrollObjectRT.anchoredPosition = new Vector2(posX, posY);
                }
            }
        }

        public void SetTwoDimensionGrid()
        {
            scrollObjects = new ScrollObject[0];

            float posX = 0f;
            float posY = 0f;
            int rowCount = 0;
            int columnCount = 0;

            if (scrollContentRT.transform.childCount != 0)
            {
                for (int i = 0; i < scrollContentRT.transform.childCount; i++)
                {
                    RectTransform scrollObjectRT = scrollContentRT.transform.GetChild(i).GetComponent<RectTransform>();
                    if (i == 0) // First Object Center
                    {
                        posX = leftPosX - (scrollObjectRT.sizeDelta.x / 2);
                        posY = topPosY + (scrollObjectRT.sizeDelta.y / 2);
                    }
                    else
                    {
                        int previousIndex = i - 1;
                        RectTransform previousRT = scrollContentRT.transform.GetChild(previousIndex).GetComponent<RectTransform>();
                        float previousPosX = previousRT.anchoredPosition.x;
                        float previousPosY = previousRT.anchoredPosition.y;
                        float previousWidth = previousRT.sizeDelta.x;
                        float previousHeight = previousRT.sizeDelta.y;
                        float width = scrollObjectRT.sizeDelta.x;
                        float height = scrollObjectRT.sizeDelta.y;

                        if (i % twoDimensionMaxRow > 0)
                        {
                            // Fill items in Row
                            rowCount++;
                            posX = previousPosX + (previousWidth / 2) + itemSpacing + (width / 2);
                            posY = previousPosY;
                        }
                        else
                        {
                            // Switch to next Row
                            rowCount = 0;
                            columnCount++;
                            int firstRollIndex = i - twoDimensionMaxRow;
                            previousRT = scrollContentRT.transform.GetChild(firstRollIndex).GetComponent<RectTransform>();
                            previousPosX = previousRT.anchoredPosition.x;
                            previousPosY = previousRT.anchoredPosition.y;
                            previousWidth = previousRT.sizeDelta.x;
                            previousHeight = previousRT.sizeDelta.y;
                            posX = previousPosX;
                            posY = previousPosY - (previousHeight / 2) - itemSpacing - (height / 2);
                        }
                    }
                    scrollObjectRT.anchoredPosition = new Vector2(posX, posY);
                    ScrollObject scrollObject = new ScrollObject(scrollObjectRT, rowCount, columnCount);
                    ArrayExtension.AddElement(ref scrollObjects, ref scrollObject);
                }
            }
        }

        // Each Scroll Object
        [Serializable]
        public class ScrollObject
        {
            public RectTransform scrollObjectRT;
            public int rowIndex;
            public int columnIndex;

            public ScrollObject(RectTransform scrollObjectRT, int rowIndex, int columnIndex)
            {
                this.scrollObjectRT = scrollObjectRT;
                this.rowIndex = rowIndex;
                this.columnIndex = columnIndex;
            }

            public RectTransform GetRectTransform()
            {
                return scrollObjectRT;
            }
            public float GetPosX()
            {
                return scrollObjectRT.position.x;
            }
            public float GetAnchoredPosX()
            {
                return scrollObjectRT.anchoredPosition.x;
            }
            public float GetPosY()
            {
                return scrollObjectRT.position.y;
            }
            public float GetAnchoredPosY()
            {
                return scrollObjectRT.anchoredPosition.y;
            }
            public float GetWidth()
            {
                return scrollObjectRT.sizeDelta.x;
            }
            public float GetHeight()
            {
                return scrollObjectRT.sizeDelta.y;
            }
            public int GetRowIndex()
            {
                return rowIndex;
            }
            public int GetColumnIndex()
            {
                return columnIndex;
            }
            public void SetRowIndex(int rowIndex)
            {
                this.rowIndex = rowIndex;
            }
            public void SetColumnIndex(int columnIndex)
            {
                this.columnIndex = columnIndex;
            }

            public void SetPosition(Vector2 position)
            {
                scrollObjectRT.anchoredPosition = position;
            }
        }

        // Drag Handler
        #region
        /// <summary>
        /// Called when the user starts to drag the scroll view.
        /// </summary>
        /// <param name="eventData">The data related to the drag event.</param>
        public void OnBeginDrag(PointerEventData eventData)
        {
            lastDragPosition = eventData.position;
            isOnDrag = true;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            isOnDrag = false;
        }

        /// <summary>
        /// Called while the user is dragging the scroll view.
        /// </summary>
        /// <param name="eventData">The data related to the drag event.</param>
        public void OnDrag(PointerEventData eventData)
        {
            positiveDrag_Vertical = eventData.position.y > lastDragPosition.y;
            positiveDrag_Horizontal = eventData.position.x > lastDragPosition.x;

            lastDragPosition = eventData.position;
        }

        /// <summary>
        /// Called when the user starts to scroll with their mouse wheel in the scroll view.
        /// </summary>
        /// <param name="eventData">The data related to the scroll event.</param>
        public void OnScroll(PointerEventData eventData)
        {
            if (scrollRect.vertical)
            {
                positiveDrag_Vertical = eventData.scrollDelta.y > 0;
            }
            else
            {
                // Scrolling up on the mouse wheel is considered a negative scroll, but I defined
                // scrolling downwards (scrolls right in a horizontal view) as the positive direciton,
                // so I check if the if scrollDelta.y is less than zero to check for a positive drag.
                positiveDrag_Vertical = eventData.scrollDelta.y < 0;
            }
        }

        /// <summary>
        /// Called when the user is dragging/scrolling the scroll view.
        /// </summary>
        public virtual void OnViewScroll()
        {
            switch (scrollRectType == ScrollRectType.Both)
            {
                default:
                case false:
                    if (scrollRect.vertical)
                    {
                        HandleVerticalScroll();
                    }
                    else
                    {
                        HandleHorizontalScroll();
                    }
                    break;

                case true:
                    Handle2DScroll();
                    break;
            }
        }
        /// </summary>

        private void HandleHorizontalScroll()
        {
            RectTransform startRT = scrollContentRT.GetChild(0).GetComponent<RectTransform>();
            int endIndex = scrollContentRT.childCount - 1;
            RectTransform endRT = scrollContentRT.GetChild(endIndex).GetComponent<RectTransform>();

            if (positiveDrag_Horizontal)
            {
                // Move Right
                if (startRT.position.x > offsetMinX)
                {
                    endRT.SetSiblingIndex(0);
                    float newX = startRT.anchoredPosition.x - (startRT.sizeDelta.x / 2) - itemSpacing - (endRT.sizeDelta.x / 2);
                    endRT.anchoredPosition = new Vector2(newX, 0f);
                }
            }
            else
            {
                // Move Left
                if (endRT.position.x < offsetMaxX)
                {
                    startRT.SetSiblingIndex(endIndex);
                    float newX = endRT.anchoredPosition.x + (endRT.sizeDelta.x / 2) + itemSpacing + (startRT.sizeDelta.x / 2);
                    startRT.anchoredPosition = new Vector2(newX, 0f);
                }
            }
        }

        private void HandleVerticalScroll()
        {
            RectTransform startRT = scrollContentRT.GetChild(0).GetComponent<RectTransform>();
            int endIndex = scrollContentRT.childCount - 1;
            RectTransform endRT = scrollContentRT.GetChild(endIndex).GetComponent<RectTransform>();

            if (positiveDrag_Vertical)
            {
                // Move Up
                if (endRT.position.y > offsetMinY)
                {
                    startRT.SetSiblingIndex(endIndex);
                    float newY = endRT.anchoredPosition.y - (endRT.sizeDelta.y / 2) - itemSpacing - (startRT.sizeDelta.y / 2);
                    startRT.anchoredPosition = new Vector2(0f, newY);
                }
            }
            else
            {
                // Move Down
                if (startRT.position.y < offsetMaxY)
                {
                    endRT.SetSiblingIndex(0);
                    float newY = startRT.anchoredPosition.y + (startRT.sizeDelta.y / 2) + itemSpacing + (endRT.sizeDelta.y / 2);
                    endRT.anchoredPosition = new Vector2(0f, newY);
                }
            }
        }

        private ScrollObject FindScrollObject(int targetRow, int targetColumn)
        {
            ScrollObject targetScrollObject = null;
            for (int i = 0; i < scrollObjects.Length; i++)
            {
                int row = scrollObjects[i].GetRowIndex();
                int column = scrollObjects[i].GetColumnIndex();

                if (row == targetRow && column == targetColumn)
                {
                    targetScrollObject = scrollObjects[i];
                    break;
                }
            }
            return targetScrollObject;
        }

        private int DoHorizontalFirst()
        {
            int horizontalIsCloser;

            ScrollObject topLeftSO = FindScrollObject(0, 0);
            ScrollObject bottomRightSO = FindScrollObject(twoDimensionMaxRow - 1, twoDimensionMaxColumn - 1);
            float thresholdDistanceX;
            float thresholdDistanceY;

            if (positiveDrag_Horizontal)
            {
                thresholdDistanceX = offsetMinX - topLeftSO.GetPosX();
            }
            else
            {
                thresholdDistanceX = bottomRightSO.GetPosX() - offsetMaxX;
            }

            if (positiveDrag_Vertical)
            {
                thresholdDistanceY = topLeftSO.GetPosY() - offsetMaxY;
            }
            else
            {
                thresholdDistanceY = offsetMinY - bottomRightSO.GetPosY();
            }

            if (thresholdDistanceX < thresholdDistanceY)
            {
                // True
                horizontalIsCloser = 0;
            }
            else if (thresholdDistanceX > thresholdDistanceY)
            {
                // False
                horizontalIsCloser = 1;
            }
            else
            {
                // Both may equal
                horizontalIsCloser = 2;
            }

            return horizontalIsCloser;
        }

        private void Handle2DScroll()
        {
            int maxRow = twoDimensionMaxRow - 1;
            int maxColumn = twoDimensionMaxColumn - 1;

            ScrollObject topLeft = FindScrollObject(0, 0);
            ScrollObject topRight = FindScrollObject(maxRow, 0);
            ScrollObject bottomLeft = FindScrollObject(0, maxColumn);
            ScrollObject bottomRight = FindScrollObject(maxRow, maxColumn);

            if (positiveDrag_Horizontal)
            {
                // Move Top Right
                if (topLeft.GetPosX() > offsetMinX)
                {
                    for (int i = 0; i < maxColumn + 1; i++)
                    {
                        float endAnchoredPosY = FindScrollObject(maxRow, i).GetAnchoredPosY();
                        float endWidth = FindScrollObject(maxRow, i).GetWidth();
                        float startAnchoredPosX = FindScrollObject(0, i).GetAnchoredPosX();
                        float startWidth = FindScrollObject(0, i).GetWidth();
                        float newPosX = startAnchoredPosX - (startWidth / 2) - itemSpacing - (endWidth / 2);
                        float newPosY = endAnchoredPosY;

                        FindScrollObject(maxRow, i).GetRectTransform().anchoredPosition = new Vector2(newPosX, newPosY);
                        ReassignRow(i, true);
                    }
                }
            }
            else
            {
                // Move Top Left
                if (topRight.GetPosX() < offsetMaxX)
                {
                    for (int i = 0; i < maxColumn + 1; i++)
                    {
                        float endAnchoredPosY = FindScrollObject(0, i).GetAnchoredPosY();
                        float endWidth = FindScrollObject(0, i).GetWidth();
                        float startAnchoredPosX = FindScrollObject(maxRow, i).GetAnchoredPosX();
                        float startWidth = FindScrollObject(maxRow, i).GetWidth();
                        float newPosX = startAnchoredPosX + (startWidth / 2) + itemSpacing + (endWidth / 2);
                        float newPosY = endAnchoredPosY;

                        FindScrollObject(0, i).GetRectTransform().anchoredPosition = new Vector2(newPosX, newPosY);
                        ReassignRow(i, false);
                    }
                }
            }

            if (positiveDrag_Vertical)
            {
                if (bottomLeft.GetPosY() > offsetMinY)
                {
                    for (int i = 0; i < maxRow + 1; i++)
                    {
                        float endAnchoredPosX = FindScrollObject(i, 0).GetAnchoredPosX();
                        float endHeight = FindScrollObject(i, 0).GetHeight();
                        float startAnchoredPosY = FindScrollObject(i, maxColumn).GetAnchoredPosY();
                        float startHeight = FindScrollObject(i, maxColumn).GetWidth();
                        float newPosX = endAnchoredPosX;
                        float newPosY = startAnchoredPosY - (startHeight / 2) - itemSpacing - (endHeight / 2);

                        FindScrollObject(i, 0).GetRectTransform().anchoredPosition = new Vector2(newPosX, newPosY);
                        ReassignColumn(i, true);
                    }
                }
            }
            else
            {
                if (bottomRight.GetPosY() < offsetMaxY)
                {
                    for (int i = 0; i < maxRow + 1; i++)
                    {
                        float endAnchoredPosX = FindScrollObject(i, maxColumn).GetAnchoredPosX();
                        float endHeight = FindScrollObject(i, maxColumn).GetHeight();
                        float startAnchoredPosY = FindScrollObject(i, 0).GetAnchoredPosY();
                        float startHeight = FindScrollObject(i, 0).GetWidth();
                        float newPosX = endAnchoredPosX;
                        float newPosY = startAnchoredPosY + (startHeight / 2) + itemSpacing + (endHeight / 2);

                        FindScrollObject(i, maxColumn).GetRectTransform().anchoredPosition = new Vector2(newPosX, newPosY);
                        ReassignColumn(i, false);
                    }
                }
            }
        }

        private ScrollObject FindRow(int targetRowIndex, int columnIndex)
        {
            ScrollObject scrollObject = null;
            for (int i = 0; i < scrollObjects.Length; i++)
            {
                if (scrollObjects[i].GetColumnIndex() == columnIndex)
                {
                    if (scrollObjects[i].GetRowIndex() == targetRowIndex)
                    {
                        scrollObject = scrollObjects[i];
                        break;
                    }
                }
            }
            return scrollObject;
        }
        private ScrollObject FindColumn(int targetColumnIndex, int rowIndex)
        {
            ScrollObject scrollObject = null;
            for (int i = 0; i < scrollObjects.Length; i++)
            {
                if (scrollObjects[i].GetRowIndex() == rowIndex)
                {
                    if (scrollObjects[i].GetColumnIndex() == targetColumnIndex)
                    {
                        scrollObject = scrollObjects[i];
                        break;
                    }
                }
            }
            return scrollObject;
        }
        private void ReassignRow(int columnIndex, bool isMoveRight)
        {
            for (int i = 0; i < scrollObjects.Length; i++)
            {
                if (scrollObjects[i].GetColumnIndex() == columnIndex)
                {
                    if (isMoveRight)
                    {
                        if (scrollObjects[i].GetRowIndex() == twoDimensionMaxRow - 1)
                        {
                            scrollObjects[i].SetRowIndex(0);
                        }
                        else
                        {
                            int previousIndex = scrollObjects[i].GetRowIndex();
                            scrollObjects[i].SetRowIndex(previousIndex + 1);
                        }
                    }
                    else
                    {
                        if (scrollObjects[i].GetRowIndex() == 0)
                        {
                            scrollObjects[i].SetRowIndex(twoDimensionMaxRow - 1);
                        }
                        else
                        {
                            int previousIndex = scrollObjects[i].GetRowIndex();
                            scrollObjects[i].SetRowIndex(previousIndex - 1);
                        }
                    }
                }
            }
        }
        private void ReassignColumn(int rowIndex, bool isMoveDown)
        {
            for (int i = 0; i < scrollObjects.Length; i++)
            {
                if (scrollObjects[i].GetRowIndex() == rowIndex)
                {
                    if (isMoveDown)
                    {
                        if (scrollObjects[i].GetColumnIndex() == twoDimensionMaxColumn - 1)
                        {
                            scrollObjects[i].SetColumnIndex(0);
                        }
                        else
                        {
                            int previousIndex = scrollObjects[i].GetColumnIndex();
                            scrollObjects[i].SetColumnIndex(previousIndex + 1);
                        }
                    }
                    else
                    {
                        if (scrollObjects[i].GetColumnIndex() == 0)
                        {
                            scrollObjects[i].SetColumnIndex(twoDimensionMaxColumn - 1);
                        }
                        else
                        {
                            int previousIndex = scrollObjects[i].GetColumnIndex();
                            scrollObjects[i].SetColumnIndex(previousIndex - 1);
                        }
                    }
                }
            }
        }

        #endregion

        // Debug Usage
        #region
        private void SetDebug()
        {
            switch (isDebug)
            {
                default:
                case true:
                    top.GetComponent<Image>().enabled = true;
                    Text topT = top.transform.GetChild(0).GetComponent<Text>();
                    topT.enabled = true;
                    topT.text = "Pos Y: " + maxY;
                    bottom.GetComponent<Image>().enabled = true;
                    Text bottomT = bottom.transform.GetChild(0).GetComponent<Text>();
                    bottomT.enabled = true;
                    bottomT.text = "Pos Y: " + minY;
                    left.GetComponent<Image>().enabled = true;
                    Text leftT = left.transform.GetChild(0).GetComponent<Text>();
                    leftT.enabled = true;
                    leftT.text = "Pos X: " + minX;
                    right.GetComponent<Image>().enabled = true;
                    Text rightT = right.transform.GetChild(0).GetComponent<Text>();
                    rightT.enabled = true;
                    rightT.text = "Pos X: " + maxX;
                    break;

                case false:
                    top.GetComponent<Image>().enabled = false;
                    top.transform.GetChild(0).GetComponent<Text>().enabled = false;
                    bottom.GetComponent<Image>().enabled = false;
                    bottom.transform.GetChild(0).GetComponent<Text>().enabled = false;
                    left.GetComponent<Image>().enabled = false;
                    left.transform.GetChild(0).GetComponent<Text>().enabled = false;
                    right.GetComponent<Image>().enabled = false;
                    right.transform.GetChild(0).GetComponent<Text>().enabled = false;
                    break;
            }
        }
        #endregion
    }
}