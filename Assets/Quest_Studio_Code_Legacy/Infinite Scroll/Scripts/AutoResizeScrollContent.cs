using UnityEngine;
using UnityEngine.UI;

namespace Quest_Studio
{
    public class AutoResizeScrollContent : MonoBehaviour
    {
        // Components
        #region
        [Header("Components")]
        [SerializeField]
        private ScrollRect scrollRect;
        private RectTransform scrollContentRT;
        private VerticalLayoutGroup verticalLayoutGroup;
        private HorizontalLayoutGroup horizontalLayoutGroup;
        private GridLayoutGroup gridLayoutGroup;
        #endregion

        // Variables
        #region
        [Header("Variables")]
        // - Scroll Type -
        #region
        private ScrollType scrollType;
        private enum ScrollType { Vertical, Horizontal, TwoDimention }
        private void SetScrollType()
        {
            if (scrollRect == null)
            {
                Debug.Log("Missing scroll rect reference!");
                return;
            }
            if (scrollRect.horizontal == true && scrollRect.vertical == true)
            {
                scrollType = ScrollType.TwoDimention;
                return;
            }
            if (scrollRect.horizontal == true)
            {
                scrollType = ScrollType.Horizontal;
                return;
            }
            if (scrollRect.vertical == true)
            {
                scrollType = ScrollType.Vertical;
                return;
            }
        }
        #endregion

        // - Layout Type -
        #region
        private LayoutType layoutType = LayoutType.Null;
        private enum LayoutType { Null, Vertical, Horizontal, Grid }
        #endregion

        // - Basic -
        #region
        private int childCount;
        private float containerMinX;
        private float containerMinY;
        #endregion

        // Vertical & Horizontal
        #region
        private float spacing;
        #endregion

        // Grid
        #region
        private float cellSizeX;
        private float cellSizeY;
        private float spacingX;
        private float spacingY;
        private float paddingX;
        private float paddingY;

        [Header("Two Dimention Grid Only")]
        [SerializeField]
        [Min(1)] private int cellInRowNumber = 1;
        [SerializeField]
        [Min(1)] private int cellInColumnNumber = 1;

        private void SetGridVariables()
        {
            cellSizeX = gridLayoutGroup.cellSize.x;
            cellSizeY = gridLayoutGroup.cellSize.y;
            spacingX = gridLayoutGroup.spacing.x;
            spacingY = gridLayoutGroup.spacing.y;
            paddingX = gridLayoutGroup.padding.top + gridLayoutGroup.padding.bottom;
            paddingY = gridLayoutGroup.padding.left + gridLayoutGroup.padding.right;
        }
        #endregion

        #endregion

        // Method
        #region
        // - Check Componets -
        #region
        private void CheckComponents()
        {
            scrollContentRT = this.GetComponent<RectTransform>();
            containerMinX = scrollContentRT.sizeDelta.x;
            containerMinY = scrollContentRT.sizeDelta.y;

            if (this.GetComponent<VerticalLayoutGroup>() != null)
            {
                verticalLayoutGroup = this.GetComponent<VerticalLayoutGroup>();
                layoutType = LayoutType.Vertical;
                scrollRect.horizontal = false;
                SetScrollType();
                return;
            }
            if (this.GetComponent<HorizontalLayoutGroup>() != null)
            {
                horizontalLayoutGroup = this.GetComponent<HorizontalLayoutGroup>();
                layoutType = LayoutType.Horizontal;
                scrollRect.vertical = false;
                SetScrollType();
                return;
            }
            if (this.GetComponent<GridLayoutGroup>() != null)
            {
                gridLayoutGroup = this.GetComponent<GridLayoutGroup>();
                layoutType = LayoutType.Grid;
                return;
            }
        }
        #endregion

        // - Resize Scroll Content -
        #region
        public void ResizeScrollContent()
        {
            CheckComponents();

            childCount = scrollContentRT.childCount;
            if (childCount == 0)
            {
                return;
            }

            switch (layoutType)
            {
                case LayoutType.Horizontal:
                    // Horizontal
                    #region
                    spacing = horizontalLayoutGroup.spacing;
                    float containerMaxX = 0;
                    for (int i = 0; i < childCount; i++)
                    {
                        RectTransform childRT = scrollContentRT.GetChild(i).GetComponent<RectTransform>();
                        containerMaxX = containerMaxX + (childRT.sizeDelta.x + (spacing * 2));
                    }
                    if (containerMaxX > containerMinX)
                    {
                        scrollContentRT.sizeDelta = new Vector2(containerMaxX, containerMinY);
                    }
                    #endregion
                    break;

                case LayoutType.Vertical:
                    // Veritcal
                    #region
                    spacing = verticalLayoutGroup.spacing;
                    float containerMaxY = 0;
                    for (int i = 0; i < childCount; i++)
                    {
                        RectTransform childRT = scrollContentRT.GetChild(i).GetComponent<RectTransform>();
                        containerMaxY = containerMaxY + (childRT.sizeDelta.y + (spacing * 2));
                    }
                    if (containerMaxY > containerMinY)
                    {
                        scrollContentRT.sizeDelta = new Vector2(containerMinX, containerMaxY);
                    }
                    #endregion
                    break;

                case LayoutType.Grid:
                    // Grid
                    #region
                    SetGridVariables();
                    int cellInRow = 0;
                    int cellInColumn = 0;
                    float adjustX = 0;
                    float adjustY = 0;

                    switch (scrollType)
                    {
                        case ScrollType.Vertical:
                            cellInRow = Mathf.FloorToInt(containerMinX / (cellSizeX + spacingX));
                            cellInColumn = (int)Mathf.Ceil((float)childCount / (float)cellInRow);
                            adjustY = (cellSizeY + spacingY) * cellInColumn + paddingX;
                            if (adjustY > containerMinY)
                            {
                                scrollContentRT.sizeDelta = new Vector2(containerMinX, adjustY);
                            }
                            break;

                        case ScrollType.Horizontal:
                            cellInColumn = (int)Mathf.FloorToInt(containerMinY / (cellSizeY + spacingY));
                            cellInRow = (int)Mathf.Ceil((float)childCount / (float)cellInColumn);
                            adjustX = (cellSizeX + spacingX) * cellInRow + paddingY;
                            if (adjustX > containerMinX)
                            {
                                scrollContentRT.sizeDelta = new Vector2(adjustX, containerMinY);
                            }
                            break;

                        case ScrollType.TwoDimention:
                            cellInColumn = cellInColumnNumber;
                            cellInRow = cellInRowNumber;
                            switch (gridLayoutGroup.startAxis)
                            {
                                case GridLayoutGroup.Axis.Horizontal:
                                    int columnNumber = (int)(childCount / cellInRow);
                                    adjustX = (cellSizeX + (spacingX * 2)) * cellInRow;
                                    adjustY = (cellSizeY + (spacingY * 2)) * columnNumber;
                                    break;

                                case GridLayoutGroup.Axis.Vertical:
                                    int rowNumber = (int)(childCount / cellInColumn);
                                    adjustX = (cellSizeX + (spacingX * 2)) * rowNumber;
                                    adjustY = (cellSizeY + (spacingY * 2)) * cellInColumn;
                                    break;
                            }

                            if (adjustX > containerMinX && adjustY > containerMinY)
                            {
                                scrollContentRT.sizeDelta = new Vector2(adjustX, adjustY);
                                return;
                            }
                            if (adjustX > containerMinX)
                            {
                                scrollContentRT.sizeDelta = new Vector2(adjustX, containerMinY);
                                return;
                            }
                            if (adjustY > containerMinY)
                            {
                                scrollContentRT.sizeDelta = new Vector2(containerMinX, adjustY);
                                return;
                            }
                            break;
                    }
                    #endregion
                    break;
            }
        }
        #endregion

        #endregion

        private void Awake()
        {
            SetScrollType();
            CheckComponents();
            ResizeScrollContent();
        }

        private void Start()
        {

        }

        private void Update()
        {

        }
    }
}