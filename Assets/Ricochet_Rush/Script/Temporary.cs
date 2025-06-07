using UnityEngine;
using UnityEngine.EventSystems;
using Quest_Studio;
using System.Collections;

public class Temporary : MonoBehaviour
{
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
