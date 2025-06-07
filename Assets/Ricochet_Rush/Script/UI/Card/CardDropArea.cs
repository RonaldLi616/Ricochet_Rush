using UnityEngine;
using UnityEngine.EventSystems;
using Quest_Studio;
using UnityEngine.UI;

public class CardDropArea : DropArea
{
    // Valuable
    #region 
    // Drop Area Type
    #region 
    private enum DropAreaType
    {
        CardSlot,
        Inventory
    }
    [SerializeField] private DropAreaType dropAreaType = DropAreaType.Inventory;
    private DropAreaType GetDropAreaType() { return dropAreaType; }
    #endregion

    #endregion

    // Component
    #region 
    private void SetComponent()
    {
        
    }

    #endregion

    // Drop Handler
    #region 
    public override void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        Card card = dropped.GetComponent<Card>();
        if (card == null) { return; }

        card.SetParentTransform(this.transform);
        switch (GetDropAreaType())
        {
            case DropAreaType.CardSlot:
                card.SetCardStatus(Card.CardStatus.InsertCard);
                break;

            case DropAreaType.Inventory:
                card.SetCardStatus(Card.CardStatus.Inventory);
                break;
        }
    }
    #endregion

    private void Awake()
    {
        // Set Component
        #region 
        SetComponent();
        #endregion

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
