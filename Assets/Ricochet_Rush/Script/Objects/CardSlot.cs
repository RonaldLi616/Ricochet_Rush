using UnityEngine;
using UnityEngine.EventSystems;

public class CardSlot : MonoBehaviour, IDropHandler
{
    // Drop Handler
    #region 
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Dropped on target");
        GameObject dropped = eventData.pointerDrag;
        Card card = dropped.GetComponent<Card>();
        if (card == null) { return; }

        CardInfo cardInfo = card.GetCardInfo();
        int ballNumber = cardInfo.GetCardValue().GetBallNumber();
        Dropper.GetInstance().AddBallInTray(ballNumber);
    }
    #endregion

    // Collision
    #region 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        Card card = collision.gameObject.GetComponent<Card>();
        if (card == null)
        {
            return;
        }
        CardInfo cardInfo = card.GetCardInfo();
        int ballNumber = cardInfo.GetCardValue().GetBallNumber();
        Dropper.GetInstance().AddBallInTray(ballNumber);
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
