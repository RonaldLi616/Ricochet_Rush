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
        GetImage().raycastTarget = false;
    }

    public override void OnDrag(PointerEventData eventData)
    {
        UpdateCardPosition();
    }

    public override void OnEndDrag(PointerEventData eventData)
    { 
        GetImage().raycastTarget = true;
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
