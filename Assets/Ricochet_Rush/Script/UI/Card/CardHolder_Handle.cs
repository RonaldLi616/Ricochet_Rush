using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Quest_Studio;

[RequireComponent(typeof(RawImage))]
[ExecuteAlways]
public class CardHolder_Handle : DraggableObject
{
    // Reference Point
    #region
    [Header("Reference Point")]
    [Tooltip("Min position for vertical movement of card holder's reference point")]
    [SerializeField] private RectTransform bottomRT;

    [Tooltip("Min position for vertical movement of card holder's reference point")]
    [SerializeField] private RectTransform topRT;
    private void SetReferencePoint()
    {
        if (cardHolderHandleRT == null)
        {
            Debug.Log("Missing Card Holder RectTransform Reference!");
            return;
        }

        topRT.anchorMin = cardHolderHandleRT.anchorMin;
        topRT.anchorMax = cardHolderHandleRT.anchorMax;
        bottomRT.anchorMin = cardHolderHandleRT.anchorMin;
        bottomRT.anchorMax = cardHolderHandleRT.anchorMax;
    }
    #endregion

    // Component
    #region 
    public override void SetComponent()
    {
        base.SetComponent();

        SetCardHolderHandleRT();
        SetCardHolderHandleRI();
        SetHandleTransparent(true);
        SetReferencePoint();
    }

    // Card Holder Rect Transform
    #region 
    private RectTransform cardHolderHandleRT;
    private void SetCardHolderHandleRT() { cardHolderHandleRT = this.transform.parent.GetComponent<RectTransform>(); }
    #endregion

    // Card Holder Raw Image
    #region 
    [Header("Handle Image")]
    [SerializeField] private Texture2D handle_Normal;
    [SerializeField] private Texture2D handle_Transparent;
    private RawImage cardHolderHandleRI;
    private void SetCardHolderHandleRI() { cardHolderHandleRI = this.transform.GetComponent<RawImage>(); }
    private void SetHandleTransparent(bool isTransparent){ cardHolderHandleRI.texture = isTransparent ? handle_Transparent : handle_Normal; }
    #endregion

    #endregion

    // Method
    #region 
    private void UpdateCardHolderPosition()
    {
        Canvas canvas = GameManager.GetInstance().GetMainCanvas();
        Vector2 movePosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, canvas.worldCamera, out movePosition);
        Vector2 followPosition = canvas.transform.TransformPoint(movePosition);

        Vector2 min = canvas.transform.InverseTransformPoint(bottomRT.position);
        Vector2 max = canvas.transform.InverseTransformPoint(topRT.position);

        Vector2 inRangePosition = canvas.transform.InverseTransformPoint(followPosition);
        if (inRangePosition.y >= min.y && inRangePosition.y < max.y)
        {
            this.transform.parent.transform.position = new Vector3(this.transform.position.x, followPosition.y, 0f);
        }
    }
    #endregion

    // Drag Handler
    #region 
    public override void OnBeginDrag(PointerEventData eventData)
    {
        // Raycast Target
        #region 
        GetImage().raycastTarget = false;
        #endregion

        SetHandleTransparent(false);
    }

    public override void OnDrag(PointerEventData eventData)
    {
        UpdateCardHolderPosition();
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        // Raycast Target
        #region 
        GetImage().raycastTarget = true;
        #endregion

        SetHandleTransparent(true);
    }

    #endregion

    public override void Awake()
    {
        base.Awake();

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }
}
