using UnityEngine;
using UnityEngine.EventSystems;
using Quest_Studio;
using System.Collections;

public class Temporary : MonoBehaviour
{   
    // On Hit Animation
    #region 
    private void OnHitAnimation()
    {
        /*isCooldown = true;

        // Hash Table
        #region 
        Hashtable onHitMannequinHash = new Hashtable();
        onHitMannequinHash.Add("space", Space.Self);
        onHitMannequinHash.Add("time", 1f);
        onHitMannequinHash.Add("easetype", iTween.EaseType.easeInOutBounce);

        #endregion

        if (currentHealth > 0)
        {
            onHitMannequinHash.Add("name", "Mannequin_" + animateTarget.GetInstanceID() + "_OnHit_PunchPosition");
            onHitMannequinHash.Add("z", 180f);
            onHitMannequinHash.Add("oncomplete", "OnCompleteAnimation_Mannequin");
            onHitMannequinHash.Add("oncompletetarget", this.gameObject);
            iTween.PunchRotation(animateTarget, onHitMannequinHash);
        }
        else
        {
            onHitMannequinHash.Add("name", "Mannequin_" + animateTarget.GetInstanceID() + "_OnHit_PunchScale");
            onHitMannequinHash.Add("x", 2f);
            onHitMannequinHash.Add("y", 2f);
            onHitMannequinHash.Add("onstart", "OnStartDestroyAnimation_Mannequin");
            onHitMannequinHash.Add("onstarttarget", this.gameObject);
            iTween.PunchScale(animateTarget, onHitMannequinHash);

        }*/

    }

    private void OnCompleteAnimation_Mannequin()
    {
        /*isCooldown = false;
        CheckHealth();*/
    }
    #endregion

    // Destroy Animation
    #region 
    private void OnStartDestroyAnimation_Mannequin()
    {
        // Hash Table
        /*#region 
        Hashtable onDestroyMannequinHash = new Hashtable();
        onDestroyMannequinHash.Add("name", "Mannequin_" + animateTarget.GetInstanceID() + "_OnStartDestroy_ScaleTo");
        onDestroyMannequinHash.Add("x", 0f);
        onDestroyMannequinHash.Add("y", 0f);
        onDestroyMannequinHash.Add("time", 1f);
        onDestroyMannequinHash.Add("easetype", iTween.EaseType.easeOutSine);
        onDestroyMannequinHash.Add("oncomplete", "OnCompleteResetAnimation_Mannequin");
        onDestroyMannequinHash.Add("oncompletetarget", this.gameObject);

        #endregion
        iTween.ScaleTo(this.gameObject, onDestroyMannequinHash);
        */
    }
    #endregion

    // Reset Animation
    #region 
    private void OnCompleteResetAnimation_Mannequin()
    {
        // Hash Table
        /*#region 
        Hashtable onDestroyMannequinHash = new Hashtable();
        onDestroyMannequinHash.Add("name", "Mannequin_" + animateTarget.GetInstanceID() + "_OnCompleteReset_ScaleTo");
        onDestroyMannequinHash.Add("x", 1f);
        onDestroyMannequinHash.Add("y", 1f);
        onDestroyMannequinHash.Add("time", 1f);
        onDestroyMannequinHash.Add("easetype", iTween.EaseType.easeOutSine);
        onDestroyMannequinHash.Add("oncomplete", "OnCompleteAnimation_Mannequin");
        onDestroyMannequinHash.Add("oncompletetarget", this.gameObject);

        #endregion
        iTween.ScaleTo(this.gameObject, onDestroyMannequinHash);
        */
    }
    #endregion


    // Fade Animation
    #region 
    private void FadeObject(GameObject target, float alpha, float time, float delay)
    {
        // Hash Table
        #region 
        Hashtable fadeHash = new Hashtable();
        fadeHash.Add("name", "Temporary_" + this.gameObject.GetInstanceID() + "_FadeObject_FadeTo");
        fadeHash.Add("alpha", alpha);
        fadeHash.Add("includechildren", true);
        fadeHash.Add("time", time);
        fadeHash.Add("delay", delay);
        fadeHash.Add("easetype", iTween.EaseType.linear);
        fadeHash.Add("oncompletetarget", target);
        #endregion

        iTween.FadeTo(target, fadeHash);
    }
    [SerializeField] bool isFade;
    private void OnCompleteFade_Mannequin()
    {
        if (isFade)
        {
            isFade = !isFade;
            FadeObject(this.gameObject, 0f, 1f, 0f);
        }
        else
        {
            isFade = !isFade;
            FadeObject(this.gameObject, 1f, 1f, 1f);
        }

    }
    #endregion

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Fade");
            OnCompleteFade_Mannequin();
        }
    }
}
