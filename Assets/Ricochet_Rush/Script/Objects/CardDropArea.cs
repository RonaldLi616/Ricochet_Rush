using UnityEngine;
using UnityEngine.EventSystems;
using Quest_Studio;

public class CardDropArea : DropArea
{
    // Drop Handler
    #region 
    public override void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        Card card = dropped.GetComponent<Card>();
        if (card == null) { return; }

        card.SetParentTransform(this.transform);
        CardInfo cardInfo = card.GetCardInfo();
        int ballNumber = cardInfo.GetCardValue().GetBallNumber();
        Dropper.GetInstance().AddBallInTray(ballNumber);

        CardSlot.GetInstance().InsertCard(card);
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
