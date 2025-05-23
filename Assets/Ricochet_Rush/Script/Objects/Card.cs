using UnityEngine;
using UnityEngine.EventSystems;
using Quest_Studio;

public class Card : DraggableObject
{
    // Valuable
    #region
    // Card Info
    #region
    [SerializeField] private CardInfo cardInfo = null;
    public CardInfo GetCardInfo() { return cardInfo; }
    #endregion

    // Latest Position
    #region 
    [SerializeField] private Vector3 latestPosition;
    private void SetLatestPosition()
    {
        latestPosition = this.transform.localPosition;
    }
    public Vector3 GetLatestPosition() { return latestPosition; }
    #endregion

    // In Card Slot
    #region 
    private bool InCardSlot = false;
    public void SetInCardSlot(bool isIn) { InCardSlot = isIn; }
    public bool GetInCardSlot() { return InCardSlot; }
    #endregion

    #endregion

    // Component
    #region 
    public override void SetComponent()
    {
        base.SetComponent();
    }

    #endregion

    // Method
    #region 
    private void UpdateCardPosition()
    {
        this.transform.position = GameManager.GetInstance().GetMousePosition();
    }

    #endregion

    // Drag Handler
    #region 
    public override void OnBeginDrag(PointerEventData eventData)
    {
        if (InCardSlot) { return; }
        SetParentTransform(this.transform.parent);
        this.transform.SetParent(this.transform.parent.parent);
        this.transform.SetAsLastSibling();
        GetImage().raycastTarget = false;
    }

    public override void OnDrag(PointerEventData eventData)
    {
        if (InCardSlot) { return; }
        UpdateCardPosition();
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        this.transform.SetParent(GetParentTransform());
        GetImage().raycastTarget = true;

        SetLatestPosition();
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
