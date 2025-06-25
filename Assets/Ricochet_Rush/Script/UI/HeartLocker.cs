using UnityEngine;
using UnityEngine.UI;

public class HeartLocker : MonoBehaviour
{
    // Reference Sprite
    #region 
    [Header("Heart Sprite")]
    [SerializeField] private Texture2D heartSprite;
    [SerializeField] private Texture2D brokenHeartSprite;
    private Texture2D GetHeartSprite()
    {
        if (heartSprite == null || brokenHeartSprite == null){ Debug.Log("Missing Heart Sprite Reference!"); }
        return (isBroken ? brokenHeartSprite : heartSprite);
    }
    #endregion

    // Raw Image
    #region 
    [Header("Raw Image")]
    [SerializeField] private RawImage heartLockerRI;
    public void SetHeartLockerImage()
    {
        heartLockerRI.texture = GetHeartSprite();
    }
    #endregion

    // Valuable
    #region 
    [Header("Valuable")]
    [SerializeField] private bool isBroken = false;
    public void SetIsBroken(bool isBroken) { this.isBroken = isBroken; }
    #endregion

    private void Awake()
    {
        SetHeartLockerImage();
    }
}
