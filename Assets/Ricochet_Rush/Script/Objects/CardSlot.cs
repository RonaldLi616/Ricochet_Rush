using UnityEngine;
using Quest_Studio;

public class CardSlot : MonoBehaviour
{
    // Instance
    #region 
    public static CardSlot instance = null;
    private void SetInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public static CardSlot GetInstance() { return instance; }
    #endregion

    // Drop Handler
    #region 
    /*public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log("Dropped on target");
        GameObject dropped = eventData.pointerDrag;
        Card card = dropped.GetComponent<Card>();
        if (card == null) { return; }

        CardInfo cardInfo = card.GetCardInfo();
        int ballNumber = cardInfo.GetCardValue().GetBallNumber();
        Dropper.GetInstance().AddBallInTray(ballNumber);
    }*/
    #endregion

    // Animation
    #region 
    // Card Animation
    #region

    public void InsertCard(Card card)
    {
        if (card == null)
        {
            Debug.Log("Missing Card reference!");
            return;
        }
        card.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        float posX = card.transform.position.x + 500f;
        float posY = card.transform.position.y;
        float posZ = card.transform.position.z;
        FunctionTimer.Create(() =>
        {
            card.transform.position = new Vector3(posX, posY, posZ);
        }, 1f, "InsertCardTimer");
        
    }
    #endregion

    #endregion

    private void Awake()
    {
        // Instance
        #region 
        SetInstance();
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
