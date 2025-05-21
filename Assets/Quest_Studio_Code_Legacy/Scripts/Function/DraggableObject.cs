using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Quest_Studio
{
    public class DraggableObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        // Component
        #region 
        public virtual void SetComponent()
        {
            SetImage();
        }

        // Image
        #region 
        [SerializeField] private Image image;
        private void SetImage()
        {
            image = this.GetComponent<Image>();
        }
        public Image GetImage()
        {
            if (image == null) { Debug.Log("Missing Image Reference!"); }
            return image;
        }
        #endregion

        #endregion

        // Drag Handler
        #region 
        public virtual void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log("Begin drag");
            GetImage().raycastTarget = false;
        }

        public virtual void OnDrag(PointerEventData eventData)
        {
            Debug.Log("Dragging");
            this.transform.position = Input.mousePosition;
        }

        public virtual void OnEndDrag(PointerEventData eventData)
        {
            Debug.Log("End drag");
            GetImage().raycastTarget = true;
        }

        #endregion

        public virtual void Awake()
        {
            // Set Component
            #region 
            SetComponent();
            #endregion    
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        public virtual void Start()
        {

        }

        // Update is called once per frame
        public virtual void Update()
        {

        }
    }
}