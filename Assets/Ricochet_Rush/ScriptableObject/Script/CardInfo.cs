using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "Scriptable Objects/Card")]
public class CardInfo : ScriptableObject
{
    // Valuable
    #region
    [Serializable]
    public class CardValue
    {
        // Type
        #region 
        public enum Type
        {
            Restore,
            Buff
        }

        [SerializeField] private Type cardType = Type.Restore;
        public Type GetCardType() { return cardType; }

        #endregion

        // Ball GameObject Prefab
        #region 
        private GameObject ballPF = null;
        public GameObject GetBallPF() { return ballPF; }
        #endregion

        // Ball Number
        #region
        [SerializeField][Min(0)] private int ballNumber = 0;
        public int GetBallNumber() { return ballNumber; }
        #endregion

        public CardValue(Type cardType, GameObject ballPF, int ballNumber)
        {
            this.cardType = cardType;
            this.ballPF = ballPF;
            this.ballNumber = ballNumber;
        }
    }

    [SerializeField] private CardValue cardValue = null;
    public CardValue GetCardValue() { return cardValue; }
    #endregion

    // Card Detail
    #region
    [Serializable]
    public class CardDetail
    {
        // Title
        #region 
        [SerializeField] private string title = "";
        public string GetTitle() { return title; }
        #endregion

        // Description
        #region 
        [SerializeField] private string description = "";
        public string GetDescription() { return description; }
        #endregion

        // Thumbnail
        #region 
        [SerializeField] private RenderTexture thumbnail = null;
        public RenderTexture GetThumbnail() { return thumbnail; }
        #endregion

        public CardDetail(string title, string description, RenderTexture thumbnail)
        {
            this.title = title;
            this.description = description;
            this.thumbnail = thumbnail;
        }
    }

    [SerializeField] private CardDetail cardDetail = null;
    public CardDetail GetCardDetail() { return cardDetail; }
    #endregion
}
