using UnityEngine;
using Quest_Studio;
using Unity.VisualScripting;
using UnityEngine.UIElements;

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

    // Card Reference
    #region 
    [SerializeField] private Card card;
    private void SetCard(Card card) { this.card = card; }
    private Card GetCard() { return card; }
    
    // Move Card Position
    private void OnUpdateCardPosition(Vector3 newPosition)
    {
        GetCard().transform.localPosition = newPosition;
    }
    
    // Destroy Card
    private void DestroyCard()
    {
        if (GetCard() == null)
        {
            Debug.Log("Missing Card reference!");
            return;
        }
        CardInfo cardInfo = card.GetCardInfo();
        int ballNumber = cardInfo.GetCardValue().GetBallNumber();
        Dropper.GetInstance().AddBallInTray(ballNumber);

        Destroy(card.gameObject);
        SetCard(null);
    }
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

        SetCard(card);

        // Tween Rotation
        #region 
        TweenCardTransform();
        #endregion
    }

    #endregion

    #endregion

    // iTween
    #region
    // Tween Card Transform
    #region
    private void TweenCardTransform()
    {
        if (GetCard() == null)
        {
            Debug.Log("Missing Card reference!");
            return;
        }

        iTween.ValueTo(card.gameObject, iTween.Hash(
            "from", GetCard().GetLatestPosition(),
            "to", new Vector3(0, 0, 0),
            "time", 0.5f,
            "easetype", iTween.EaseType.linear,
            "onupdate", "OnUpdateCardPosition",
            "onupdatetarget", this.gameObject
        ));

        iTween.RotateTo(card.gameObject, iTween.Hash(
            "z", 90f,
            "time", 1f,
            "easetype", iTween.EaseType.easeInOutSine,
            "oncomplete", "TweenCardPosition",
            "oncompletetarget", this.gameObject
        ));
    }
    #endregion
    
    // Tween Card Position
    #region 
    private void TweenCardPosition()
    {
        if (GetCard() == null)
        {
            Debug.Log("Missing Card reference!");
            return;
        }

        float posX = GetCard().transform.localPosition.x + 500f;
        float posY = GetCard().transform.localPosition.y;
        float posZ = GetCard().transform.localPosition.z;

        iTween.ValueTo(card.gameObject, iTween.Hash(
            "from", GetCard().transform.localPosition,
            "to", new Vector3(posX, posY, posZ),
            "time", 1f,
            "easetype", iTween.EaseType.easeInSine,
            "onupdate", "OnUpdateCardPosition",
            "onupdatetarget", this.gameObject,
            "oncomplete", "DestroyCard",
            "oncompletetarget", this.gameObject
        ));
    }
    #endregion

    #endregion

    // Method
    #region
    

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
