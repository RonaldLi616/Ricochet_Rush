using UnityEngine;
using System.Collections;
using Quest_Studio;

public class Plant : MonoBehaviour
{
    // Reference Object
    #region 
    [Header("Reference Object")]
    [SerializeField] private ClayPot clayPot;
    public void SetClayPot(ClayPot clayPot) { this.clayPot = clayPot; }
    public ClayPot GetClayPot()
    {
        if (clayPot == null) { Debug.Log("Missing Clay Pot Reference!"); }
        return clayPot;
    }
    #endregion

    // Valuable
    #region 
    private bool isCooldown = false;
    #endregion

    // Method
    #region 
    public void DestroyBloomLeaf(GameObject destroyTarget, Vector2 magnitude)
    {
        Rigidbody2D rb = destroyTarget.AddComponent<Rigidbody2D>();
        rb.simulated = true;
        rb.AddForce(magnitude);

        Vector3 finalScale = new Vector3(0f, 0f, 0f);
        float delay = 3f;
        // Hash Table
        #region 
        Hashtable onDestroyObjectHash = new Hashtable();
        onDestroyObjectHash.Add("name", "Plant_BloomLeaf" + destroyTarget.GetInstanceID() + "_OnHit");
        onDestroyObjectHash.Add("scale", finalScale);
        onDestroyObjectHash.Add("easetype", iTween.EaseType.linear);
        onDestroyObjectHash.Add("delay", Deviation.DeviateNumber(delay, 0.25f));
        onDestroyObjectHash.Add("time", 1f);
        onDestroyObjectHash.Add("oncomplete", "DestroySelf");
        onDestroyObjectHash.Add("oncompleteparams", destroyTarget);
        onDestroyObjectHash.Add("oncompletetarget", this.gameObject);

        #endregion

        iTween.ScaleTo(destroyTarget, onDestroyObjectHash);
    }

    // Destroy Self
    #region
    private void DestroySelf(GameObject destroyTarget)
    {
        Destroy(destroyTarget);
    }
    #endregion

    // Destroy Whole Plant
    #region 
    [Header("Destroyables")]
    [SerializeField] private GameObject destroyables;
    private void DestroyWholePlant()
    {
        //this.gameObject.SetActive(false);
        this.gameObject.transform.localScale = Vector3.zero;
        GameObject go = Instantiate(destroyables, this.gameObject.transform.parent);
        go.transform.position = this.gameObject.transform.position;

        float delay = 3f;
        for (int i = 0; i < go.transform.childCount; i++)
        {
            go.transform.GetChild(i).GetComponent<DestroyObject>().OnDestroyProgress(delay);
        }

        FunctionTimer.Create(() =>
        {
            GetClayPot().SetSpawnerAvailable(true);
            iTween.Stop(this.gameObject);
            Destroy(go);
            Destroy(this.gameObject);
            
        }, delay + 1f, "DestroyPlantTimer");
    }

    #endregion

    #endregion

    // Animation
    #region
    [Header("Rig Control Point")]
    [SerializeField] private GameObject mainPointGO;
    [SerializeField] private GameObject rightPointGO;
    [SerializeField] private GameObject leftPointGO;

    // On Hit Animation
    #region 
    public void OnHitAnimation(Vector2 magnitude, bool isBloom)
    {
        isCooldown = true;

        // Hash Table
        #region 
        Hashtable onHitMannequinHash = new Hashtable();
        onHitMannequinHash.Add("name", "Plant_" + mainPointGO.gameObject.GetInstanceID() + "_OnHit");
        onHitMannequinHash.Add("x", magnitude.x);
        onHitMannequinHash.Add("y", magnitude.y);
        onHitMannequinHash.Add("space", Space.Self);
        onHitMannequinHash.Add("time", 1f);

        #endregion

        if (isBloom)
        {
            onHitMannequinHash.Add("onstart", "DestroyWholePlant");
            onHitMannequinHash.Add("onstarttarget", this.gameObject);
        }
        else
        {
            onHitMannequinHash.Add("oncomplete", "OnCompleteAnimation_Plant");
            onHitMannequinHash.Add("oncompletetarget", this.gameObject);
        }

        iTween.PunchPosition(mainPointGO, onHitMannequinHash);

    }
    #endregion
    
    public void OnHitAnimation_Leaf(Vector2 magnitude, bool isLeft)
    {
        isCooldown = true;

        // Hash Table
        #region 
        Hashtable onHitPlantHash = new Hashtable();
        
        onHitPlantHash.Add("x", magnitude.x);
        onHitPlantHash.Add("y", magnitude.y);
        onHitPlantHash.Add("space", Space.Self);
        onHitPlantHash.Add("time", 1f);
        onHitPlantHash.Add("oncomplete", "OnCompleteAnimation_Plant");
        onHitPlantHash.Add("oncompletetarget", this.gameObject);

        #endregion

        if (isLeft)
        {
            onHitPlantHash.Add("name", "Plant_LeafLeft" + leftPointGO.gameObject.GetInstanceID() + "_OnHit");
            iTween.PunchPosition(leftPointGO, onHitPlantHash);
        }
        else
        { 
            onHitPlantHash.Add("name", "Plant_LeafRight" + rightPointGO.gameObject.GetInstanceID() + "_OnHit");
            iTween.PunchPosition(rightPointGO, onHitPlantHash);
        }
        
    }

    private void OnCompleteAnimation_Plant()
    {
        isCooldown = false;
    }

    #endregion

}
