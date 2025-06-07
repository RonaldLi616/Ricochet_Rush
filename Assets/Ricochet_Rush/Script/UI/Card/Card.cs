using UnityEngine;
using System.Collections;
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
    private Vector3 latestPosition;
    private void SetLatestPosition()
    {
        latestPosition = this.transform.GetComponent<RectTransform>().anchoredPosition;
    }
    public Vector3 GetLatestPosition() { return latestPosition; }
    #endregion

    // Card Status
    #region 
    public enum CardStatus
    {
        Inventory,
        Dragging,
        InsertCard,
        NewCard
    }
    private CardStatus cardStatus = CardStatus.Inventory;
    public void SetCardStatus(CardStatus cardStatus) { this.cardStatus = cardStatus; }
    private CardStatus GetCardStatus() { return cardStatus; }
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
    // Update Card Position
    #region 
    private void UpdateCardPosition()
    {
        Canvas canvas = GameManager.GetInstance().GetMainCanvas();
        Vector2 movePosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, canvas.worldCamera, out movePosition);
        transform.position = canvas.transform.TransformPoint(movePosition);
    }
    #endregion

    // Card Set Parent
    #region 
    private void OnCompleteDropCard_SetParent()
    {
        SetCardStatus(CardStatus.Inventory);
        this.transform.SetParent(GetParentTransform());
        GameManager.GetInstance().ResizeScrollContent();
    }
    private void OnCompleteDropCard_SetParent_InsertCard(){ this.transform.SetParent(GetParentTransform()); }
    #endregion

    // Destroy Self
    #region 
    private void OnCompleteInsertCard_DestroySelf()
    {
        iTween.Stop(this.gameObject);
        if (GetCardStatus() == CardStatus.InsertCard)
        {
            int ballNumber = cardInfo.GetCardValue().GetBallNumber();
            Dropper.GetInstance().AddBallInTray(ballNumber);
        }
        Destroy(this.gameObject);
    }
    #endregion

    #endregion

    // Animation
    #region
    // Move Card Position
    private void OnUpdateCardPosition(Vector3 newPosition)
    {
        RectTransform rt = this.transform.GetComponent<RectTransform>();
        rt.anchoredPosition = newPosition;
    }

    // Drop Card
    #region
    private void DropCard()
    {
        float time = 1f;
        Canvas canvas = GameManager.GetInstance().GetMainCanvas();
        // Hash Table
        #region 
        Hashtable dropCardHash = new Hashtable();
        dropCardHash.Add("name", "Card_DropCard");
        dropCardHash.Add("from", latestPosition);
        dropCardHash.Add("time", time);
        dropCardHash.Add("delay", 0.1f);
        dropCardHash.Add("easetype", iTween.EaseType.easeInOutSine);
        dropCardHash.Add("onupdate", "OnUpdateCardPosition");
        dropCardHash.Add("onupdatetarget", this.gameObject);
        dropCardHash.Add("oncompletetarget", this.gameObject);
        #endregion

        switch (GetCardStatus())
        {
            case (CardStatus.InsertCard):
                // Hover Area
                dropCardHash.Add("to", canvas.transform.InverseTransformPoint(GetParentTransform().GetChild(0).position));
                dropCardHash.Add("onstart", "OnStartDropCard_RotateCard");
                dropCardHash.Add("onstarttarget", this.gameObject);
                dropCardHash.Add("oncomplete", "OnCompleteDropCard_SetParent_InsertCard");
                break;

            default:
                // Viewport
                //dropCardHash.Add("to", canvas.transform.InverseTransformPoint(GetParentTransform().parent.parent.position));
                dropCardHash.Add("to", canvas.transform.InverseTransformPoint(GetParentTransform().GetChild(GetParentTransform().childCount - 1).position));
                dropCardHash.Add("oncomplete", "OnCompleteDropCard_SetParent");
                break;
        }

        iTween.ValueTo(this.gameObject, dropCardHash);
    }
    #endregion

    // Rotate Card
    #region 
    private void OnStartDropCard_RotateCard()
    { 
        iTween.RotateTo(this.gameObject, iTween.Hash(
            "name", "Card_DropCard_RotateCard",
            "z", 90f,
            "time", 1f,
            "easetype", iTween.EaseType.easeInOutSine,
            "oncomplete", "OnCompleteRotateCard_InsertCard",
            "oncompletetarget", this.gameObject
        ));
    }
    #endregion

    // Insert Card
    #region 
    private void OnCompleteRotateCard_InsertCard()
    {
        Vector3 fromPosition = this.transform.GetComponent<RectTransform>().anchoredPosition;
        Vector3 toPosition = fromPosition;
        toPosition.x += 500f;
        iTween.ValueTo(this.gameObject, iTween.Hash(
            "from", fromPosition,
            "to", toPosition,
            "time", 1f,
            "easetype", iTween.EaseType.easeInOutSine,
            "onupdate", "OnUpdateCardPosition",
            "onupdatetarget", this.gameObject,
            "oncomplete", "OnCompleteInsertCard_DestroySelf",
            "oncompletetarget", this.gameObject
        ));
    }
    #endregion

    #endregion

    // Drag Handler
    #region 
    public override void OnBeginDrag(PointerEventData eventData)
    {
        if (cardStatus == CardStatus.InsertCard) { return; }
        SetCardStatus(CardStatus.Dragging);

        // Set GameObject Parent
        #region
        SetParentTransform(this.transform.parent);
        this.transform.SetParent(GameManager.GetInstance().GetMainCanvas().transform.GetChild(1));
        this.transform.SetAsLastSibling();
        #endregion

        // Set Anchor Point
        #region 
        RectTransform rt = this.transform.GetComponent<RectTransform>();
        rt.anchorMin = new Vector2(0.5f, 0.5f);
        rt.anchorMax = new Vector2(0.5f, 0.5f);
        #endregion

        // Raycast Target
        #region 
        GetImage().raycastTarget = false;
        #endregion

        GameManager.GetInstance().ResizeScrollContent();
    }

    public override void OnDrag(PointerEventData eventData)
    {
        UpdateCardPosition();
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        SetLatestPosition();

        // Raycast Target
        #region 
        GetImage().raycastTarget = true;
        #endregion

        // Move card from latest position to target position
        #region
        DropCard();
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
