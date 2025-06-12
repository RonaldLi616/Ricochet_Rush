using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System;
using Quest_Studio;

public class Mannequin : Enemy
{
    // Valuable
    #region 
    public override void SetValuable()
    {
        base.SetValuable();
    }

    private bool isCooldown = false;
    #endregion

    // Component
    #region 
    public override void SetComponent()
    {

    }

    [SerializeField] private GameObject animateTarget;
    #endregion

    // Method
    #region
    // Update Health Bar
    #region 
    public override void OnUpdateHealthBar(float value)
    {
        GetIndicatorBar().SetSliderValue(value);
        GetIndicatorBar().SetIndicatorText(Math.Ceiling(value) + " / " + maxHealth);
    }
    #endregion

    // Add Health
    public override void AddHealth(int number)
    {
        int result = currentHealth;
        if (number <= 0) { return; }
        result += number;
        if (result >= maxHealth) { result = maxHealth; }

        // Hash Table
        #region 
        Hashtable updateSliderValueHash = new Hashtable();
        updateSliderValueHash.Add("name", this.gameObject.name + "_" + this.gameObject.GetInstanceID() + "_AddHealth_ValueTo");
        updateSliderValueHash.Add("from", currentHealth);
        updateSliderValueHash.Add("to", result);
        updateSliderValueHash.Add("time", 1f);
        updateSliderValueHash.Add("easetype", iTween.EaseType.easeOutSine);
        updateSliderValueHash.Add("onupdate", "OnUpdateHealthBar");
        updateSliderValueHash.Add("onupdatetarget", this.gameObject);
        updateSliderValueHash.Add("oncomplete", "OnCompleteUpdateHealth_Mannequin");
        updateSliderValueHash.Add("oncompletetarget", this.gameObject);

        #endregion

        iTween.ValueTo(this.gameObject, updateSliderValueHash);

        base.currentHealth = result;
    }

    // Remove Health
    public override void RemoveHealth(int number)
    {
        int result = base.currentHealth;
        if (number <= 0 || base.currentHealth == 0) { return; }
        result -= number;
        if (result <= 0) { result = 0; }

        // Hash Table
        #region 
        Hashtable updateSliderValueHash = new Hashtable();
        updateSliderValueHash.Add("name", "Mannequin_" + this.gameObject.GetInstanceID() + "_RemoveHealth_ValueTo");
        updateSliderValueHash.Add("from", currentHealth);
        updateSliderValueHash.Add("to", result);
        updateSliderValueHash.Add("time", 0.5f);
        updateSliderValueHash.Add("easetype", iTween.EaseType.easeOutSine);
        updateSliderValueHash.Add("onupdate", "OnUpdateHealthBar");
        updateSliderValueHash.Add("onupdatetarget", this.gameObject);
        updateSliderValueHash.Add("oncomplete", "OnCompleteUpdateHealth_Mannequin");
        updateSliderValueHash.Add("oncompletetarget", this.gameObject);

        #endregion

        iTween.ValueTo(this.gameObject, updateSliderValueHash);

        base.currentHealth = result;
    }

    // Hide Indicator Text
    #region 
    private void OnCompleteUpdateHealth_Mannequin()
    {
        if (currentHealth == 0)
        {
            GetIndicatorBar().GetIndicatorText().enabled = false;
        }
        else if (currentHealth == maxHealth)
        {
            GetIndicatorBar().GetIndicatorText().enabled = true;
        }

    }
    #endregion

    // Check Health
    #region 
    public override void CheckHealth()
    {
        if (base.currentHealth > 0) { return; }
        DestroySelf();
    }
    #endregion

    // Destroy Self
    #region
    public override void DestroySelf()
    {
        //Destroy(this.gameObject);
        AddHealth(maxHealth);
        UpdateHealthBar();
    }
    #endregion

    #endregion

    // Animation
    #region
    // On Hit Animation
    #region 
    private void OnHitAnimation()
    {
        isCooldown = true;

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

        }

    }

    private void OnCompleteAnimation_Mannequin()
    {
        isCooldown = false;
        CheckHealth();
    }
    #endregion

    // Destroy Animation
    #region 
    private void OnStartDestroyAnimation_Mannequin()
    {
        // Hash Table
        #region 
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
    }
    #endregion

    // Reset Animation
    #region 
    private void OnCompleteResetAnimation_Mannequin()
    {
        // Hash Table
        #region 
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
    }
    #endregion

    #endregion

    // Collision Handler
    #region 
    public override void OnCollisionEnter2D(Collision2D collision)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();
        if (ball == null) { return; }

        if (isCooldown) { return; }
        OnHitAnimation();
        RemoveHealth(1);
    }
    #endregion

    public void OnHitMannequin()
    {
        if (isCooldown) { return; }
        OnHitAnimation();
        RemoveHealth(1);

    }

    public override void Awake()
    {
        base.Awake();

    }
    
}
