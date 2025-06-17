using UnityEngine;
using System.Collections;
using Quest_Studio;

[RequireComponent(typeof(Rigidbody2D))]
public class DestroyObject : MonoBehaviour
{
    // Method
    #region 
    // Destroy Self
    #region 
    private void DestroySelf()
    {
        Destroy(this.gameObject);
    }
    #endregion

    #endregion

    // Animation
    #region 
    public void OnDestroyProgress(float delay)
    {
        // Set Rigidbody
        #region 
        Rigidbody2D rb = this.transform.GetComponent<Rigidbody2D>();
        rb.simulated = true;
        Vector2 min = new Vector2(-1f, -1f);
        Vector2 max = new Vector2(1f, 1f);
        Vector2 force = Deviation.DeviateVector3(min, max, 2);
        rb.AddForce(force);
        #endregion

        Vector3 finalScale = new Vector3(0f, 0f, 0f);
        // Hash Table
        #region 
        Hashtable onDestroyObjectHash = new Hashtable();
        onDestroyObjectHash.Add("name", "Plant_" + this.gameObject.GetInstanceID() + "_OnHit");
        onDestroyObjectHash.Add("scale", finalScale);
        onDestroyObjectHash.Add("easetype", iTween.EaseType.linear);
        onDestroyObjectHash.Add("delay", Deviation.DeviateNumber(delay, 0.25f));
        onDestroyObjectHash.Add("time", 1f);
        onDestroyObjectHash.Add("oncomplete", "DestroySelf");
        onDestroyObjectHash.Add("oncompletetarget", this.gameObject);

        #endregion

        iTween.ScaleTo(this.gameObject, onDestroyObjectHash);
    }
    #endregion
}
