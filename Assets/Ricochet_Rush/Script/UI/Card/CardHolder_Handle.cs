using UnityEngine;
using UnityEngine.EventSystems;
using Quest_Studio;

[RequireComponent(typeof(RectTransform))]
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
        topRT.anchorMin = cardHolderGroupRT.anchorMin;
        topRT.anchorMax = cardHolderGroupRT.anchorMax;
        bottomRT.anchorMin = cardHolderGroupRT.anchorMin;
        bottomRT.anchorMax = cardHolderGroupRT.anchorMax;
    }
    #endregion

    // Component
    #region 
    public override void SetComponent()
    {
        base.SetComponent();

        SetCardHolderGroupRT();
        SetReferencePoint();
    }

    // Card Holder Rect Transform
    #region 
    private RectTransform cardHolderGroupRT;
    private void SetCardHolderGroupRT() { cardHolderGroupRT = this.GetComponent<RectTransform>(); }
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
            this.transform.position = new Vector3(this.transform.position.x, followPosition.y, 0f);
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
