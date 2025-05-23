using UnityEngine;
using UnityEngine.EventSystems;
using Quest_Studio;
using UnityEngine.UI;

public class CardDropArea : DropArea
{
    // Valuable
    #region 
    // Is Inventory
    #region 
    [SerializeField] private bool isInventory;
    private bool GetIsInventory() { return isInventory; }
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
        if (isInventory) { return; }
        card.SetInCardSlot(true);
        CardSlot.GetInstance().InsertCard(card);
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
