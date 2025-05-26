using UnityEngine;
using UnityEngine.EventSystems;
using Quest_Studio;
using UnityEngine.UI;

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
        latestPosition = this.transform.position;
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
        this.transform.position = GameManager.GetInstance().GetMousePosition();
    }
    #endregion

    // Destroy Self
    #region 
    private void DestroySelf()
    {
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
        this.transform.position = newPosition;
    }

    // Drop Card
    #region 
    private void DropCard(Vector3 targetPosition, float time)
    {
        iTween.ValueTo(this.gameObject, iTween.Hash(
            "from", latestPosition,
            "to", targetPosition,
            "time", time,
            "easetype", iTween.EaseType.easeInOutSine,
            "onupdate", "OnUpdateCardPosition",
            "onupdatetarget", this.gameObject,
            "oncomplete", "DestroyCard",
            "oncompletetarget", this.gameObject
        ));
    }
    #endregion

    // Rotate Card
    #region 
    public void RotateCard(float rotateAngle, float time)
    {
        
    }
    #endregion

    // Insert Card
    #region 
    public void InsertCard(float rotateAngle, float rotateTime, Vector3 targetPosition, float moveTime, float delayTime)
    {
        SetCardStatus(CardStatus.InsertCard);
        iTween.RotateTo(this.gameObject, iTween.Hash(
            "z", rotateAngle,
            "time", rotateTime,
            "easetype", iTween.EaseType.easeInOutSine
        ));

        iTween.ValueTo(this.gameObject, iTween.Hash(
            "from", this.transform.position,
            "to", targetPosition,
            "time", moveTime,
            "delay", delayTime,
            "easetype", iTween.EaseType.easeInOutSine,
            "onupdate", "OnUpdateCardPosition",
            "onupdatetarget", this.gameObject,
            "oncomplete", "DestroySelf",
            "oncompletetarget", this.gameObject
        ));
    }
    #endregion

    #endregion

    // Drag Handler
    #region 
    public override void OnBeginDrag(PointerEventData eventData)
    {
        if (cardStatus != CardStatus.Inventory) { return; }
        SetCardStatus(CardStatus.Dragging);

        // Set GameObject Parent
        #region 
        SetParentTransform(this.transform.parent);
        this.transform.SetParent(this.transform.parent.parent);
        this.transform.SetAsLastSibling();
        #endregion

        // Raycast Target
        #region 
        GetImage().raycastTarget = false;
        #endregion

    }

    public override void OnDrag(PointerEventData eventData)
    {
        UpdateCardPosition();
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        SetLatestPosition();

        // Move card from latest position to target position
        #region
        float time = 1f;
        DropCard(GetParentTransform().position, time);
        #endregion

        FunctionTimer.Create(() =>
        {
            this.transform.SetParent(GetParentTransform());
        }, time, "CardSetParentDelay");

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
        localTem = this.transform.localPosition;
        worldTem = this.transform.position;
    }

    [SerializeField] private Vector3 localTem;
    [SerializeField] private Vector3 worldTem;
}
