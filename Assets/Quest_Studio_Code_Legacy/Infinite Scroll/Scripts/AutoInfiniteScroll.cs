using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Quest_Studio;

namespace Quest_Studio
{
    public class AutoInfiniteScroll : MonoBehaviour, IBeginDragHandler, IEndDragHandler
    {
        // References
        private ScrollRect scrollRect;
        private CustomInfiniteScroll customInfiniteScroll;

        // Components
        private RectTransform scrollContent;

        // Variables
        private bool isDragging = false;
        [SerializeField]
        private bool toggleAutoScroll = false;

        public float autoScrollingSpeed;
        public void SetAutoScrollingSpeed()
        {
            this.autoScrollingSpeed = Random.Range(-1f, 1f);
        }
        public void SetAutoScrollingSpeed(float speed)
        {
            this.autoScrollingSpeed = speed;
        }
        public void ZeroingScrollSpeed()
        {
            this.autoScrollingSpeed = 0f;
        }

        private void Awake()
        {
            scrollRect = this.GetComponent<ScrollRect>();
            customInfiniteScroll = this.GetComponent<CustomInfiniteScroll>();
            if (scrollRect != null && customInfiniteScroll != null)
            {
                scrollContent = customInfiniteScroll.GetScrollContent();
                SetAutoScrollingSpeed();
            }
        }

        private void Start()
        {

        }

        private void Update()
        {
            if (scrollRect != null && customInfiniteScroll != null)
            {
                HandleAutoScroll();
            }
        }

        private void HandleAutoScroll()
        {
            if (toggleAutoScroll)
            {
                float top = scrollContent.offsetMax.y;
                float bottom = scrollContent.offsetMin.y;
                float left = scrollContent.offsetMin.x;
                float right = scrollContent.offsetMax.x;

                if (scrollRect.horizontal)
                {
                    // Move Left or Right
                    scrollContent.position = new Vector2(scrollContent.position.x + autoScrollingSpeed, scrollContent.position.y);
                }
                else
                {
                    // Move Up or Down
                    scrollContent.position = new Vector2(scrollContent.position.x, scrollContent.position.y + autoScrollingSpeed);
                }

                CheckScrollObject();
            }
        }

        private void CheckScrollObject()
        {
            RectTransform startRT = scrollContent.GetChild(0).GetComponent<RectTransform>();
            int endIndex = scrollContent.childCount - 1;
            RectTransform endRT = scrollContent.GetChild(endIndex).GetComponent<RectTransform>();

            if (scrollRect.horizontal)
            {
                // Move Left or Right
                if (autoScrollingSpeed > 0)
                {
                    // Move Right
                    if (startRT.position.x > customInfiniteScroll.GetOffsetMinX())
                    {
                        endRT.SetSiblingIndex(0);
                        float newX = startRT.anchoredPosition.x - (startRT.sizeDelta.x / 2) - (customInfiniteScroll.GetItemSpacing() / 2) - (endRT.sizeDelta.x / 2);
                        endRT.anchoredPosition = new Vector2(newX, 0f);
                    }
                }
                else
                {
                    // Move Left
                    if (endRT.position.x < customInfiniteScroll.GetOffsetMaxX())
                    {
                        startRT.SetSiblingIndex(endIndex);
                        float newX = endRT.anchoredPosition.x + (endRT.sizeDelta.x / 2) + (customInfiniteScroll.GetItemSpacing() / 2) + (startRT.sizeDelta.x / 2);
                        startRT.anchoredPosition = new Vector2(newX, 0f);
                    }
                }
            }
            else
            {
                // Move Up or Down
                if (autoScrollingSpeed > 0)
                {
                    // Move Up
                    if (endRT.position.y > customInfiniteScroll.GetOffsetMinY())
                    {
                        startRT.SetSiblingIndex(endIndex);
                        float newY = endRT.anchoredPosition.y - (endRT.sizeDelta.y / 2) - (customInfiniteScroll.GetItemSpacing() / 2) - (startRT.sizeDelta.y / 2);
                        startRT.anchoredPosition = new Vector2(0f, newY);
                    }
                }
                else
                {
                    // Move Down
                    if (startRT.position.y < customInfiniteScroll.GetOffsetMaxY())
                    {
                        endRT.SetSiblingIndex(0);
                        float newY = startRT.anchoredPosition.y + (startRT.sizeDelta.y / 2) + (customInfiniteScroll.GetItemSpacing() / 2) + (endRT.sizeDelta.y / 2);
                        endRT.anchoredPosition = new Vector2(0f, newY);
                    }
                }
            }
        }

        // Drag Handler
        public void OnBeginDrag(PointerEventData eventData)
        {
            isDragging = true;
            toggleAutoScroll = false;
        }

        public void OnViewScroll()
        {
            if (isDragging)
            {
                toggleAutoScroll = false;
            }
            else
            {
                toggleAutoScroll = true;
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            isDragging = false;
            toggleAutoScroll = true;
        }
    }
}