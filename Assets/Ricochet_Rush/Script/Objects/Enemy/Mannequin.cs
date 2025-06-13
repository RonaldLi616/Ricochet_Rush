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
        AddHealth(maxHealth);
        UpdateHealthBar();
    }
    #endregion

    #endregion

    // Animation
    #region
    [SerializeField] private GameObject torsoPointGO;
    [SerializeField] private Transform refTransform; 
    // On Hit Animation
    #region 
    private void OnHitAnimation(Vector2 magnitude)
    {
        isCooldown = true;

        // Hash Table
        #region 
        Hashtable onHitMannequinHash = new Hashtable();
        onHitMannequinHash.Add("name", "Mannequin_" + torsoPointGO.gameObject.GetInstanceID() + "_OnHit");
        onHitMannequinHash.Add("x", magnitude.x);
        onHitMannequinHash.Add("y", magnitude.y);
        onHitMannequinHash.Add("space", Space.Self);
        onHitMannequinHash.Add("time", 1f);
        onHitMannequinHash.Add("oncomplete", "OnCompleteAnimation_Mannequin");
        onHitMannequinHash.Add("oncompletetarget", this.gameObject);

        #endregion

        iTween.PunchPosition(torsoPointGO, onHitMannequinHash);

    }

    private void OnCompleteAnimation_Mannequin()
    {
        isCooldown = false;
        CheckHealth();
    }
    #endregion

    #endregion

    public void OnHitMannequin(Vector2 magnitude)
    {
        if (isCooldown) { return; }
        OnHitAnimation(magnitude);
        RemoveHealth(1);

    }

    public override void Awake()
    {
        base.Awake();

    }
    
}
