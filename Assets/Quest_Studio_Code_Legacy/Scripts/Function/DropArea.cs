using Quest_Studio;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Quest_Studio
{
    public class DropArea : MonoBehaviour, IDropHandler
    {
        // Drop Handler
        #region 
        public virtual void OnDrop(PointerEventData eventData)
        {
            Debug.Log("Dropped on target");
            GameObject dropped = eventData.pointerDrag;
            DraggableObject draggableObject = dropped.GetComponent<DraggableObject>();
            draggableObject.SetParentTransform(this.transform);
        }
        #endregion

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}