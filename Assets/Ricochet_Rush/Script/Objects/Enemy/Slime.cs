using UnityEngine;
using System;
using System.Collections;

public class Slime : Enemy
{
    // Valuable
    #region 
    public override void SetValuable()
    {
        base.SetValuable();
    }

    private bool isCooldown = false;
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
        updateSliderValueHash.Add("oncomplete", "OnCompleteUpdateHealth_Slime");
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
        updateSliderValueHash.Add("oncomplete", "OnCompleteUpdateHealth_Slime");
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

    // On Action Handler
    #region 
    public override void OnActionHandler(int value)
    {
        base.OnActionHandler(value);
    }
    #endregion

    // Animation
    #region
    [SerializeField] private GameObject mainPointGO;
    // On Hit Animation
    #region 
    private void OnHitAnimation(Vector2 magnitude)
    {
        isCooldown = true;

        // Hash Table
        #region 
        Hashtable onHitMannequinHash = new Hashtable();
        onHitMannequinHash.Add("name", "Mannequin_" + mainPointGO.gameObject.GetInstanceID() + "_OnHit");
        onHitMannequinHash.Add("x", magnitude.x);
        onHitMannequinHash.Add("y", magnitude.y);
        onHitMannequinHash.Add("space", Space.Self);
        onHitMannequinHash.Add("time", 1f);
        onHitMannequinHash.Add("oncomplete", "OnCompleteAnimation_Slime");
        onHitMannequinHash.Add("oncompletetarget", this.gameObject);

        #endregion

        iTween.PunchPosition(mainPointGO, onHitMannequinHash);

    }

    private void OnCompleteAnimation_Slime()
    {
        isCooldown = false;
        CheckHealth();
    }
    #endregion

    #endregion

    public void OnHitSlime(Vector2 magnitude)
    {
        if (isCooldown) { return; }
        OnHitAnimation(magnitude);
        RemoveHealth(1);

    }

    public override void Awake()
    {
        base.Awake();

    }

    public override void Start()
    {
        base.Start();
    }
    
}
