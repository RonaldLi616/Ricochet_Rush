using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Quest_Studio;
using Unity.Mathematics;
using System;
using Mono.Cecil.Cil;
using UnityEngine.Rendering;

public class Enemy : MonoBehaviour
{
    // Ennemy Info
    #region 
    [Header("Enemy Info")]
    [SerializeField] private EnemyInfo enemyInfo;
    public EnemyInfo GetEnemyInfo()
    {
        if (enemyInfo == null)
        {
            Debug.Log("Missing" + this.gameObject.name + "_" + this.gameObject.GetInstanceID() + "Enmey Info Reference!");
            return null;
        }
        return enemyInfo;
    }
    public virtual void SetupEnemy()
    {
        // Get Value from Enemy Info
        #region 
        this.maxHealth = GetEnemyInfo().GetEnemyValue().maxHealth;
        #endregion
    }

    #endregion

    // Valuable
    #region 
    public virtual void SetValuable()
    {
        currentHealth = maxHealth;
    }

    [Header("Valuable")]
    [SerializeField][Min(1)] public int maxHealth;
    [SerializeField][Min(0)] public int currentHealth;
    #endregion

    // Component
    #region 
    public virtual void SetComponent()
    {

    }

    [Header("Component")]
    [SerializeField] private NumberIndicatorBar indicatorBar;
    public NumberIndicatorBar GetIndicatorBar()
    {
        if (indicatorBar == null)
        {
            Debug.Log("Missing indicator bar!");
            return null;
        }
        return indicatorBar;
    }

    private void SetupIndicatorBar()
    {
        if (indicatorBar == null)
        {
            Debug.Log("Missing indicator bar!");
            return;
        }
        indicatorBar.SetMaximum(maxHealth);
        indicatorBar.SetCurrentNumber(currentHealth);
    }
    public void UpdateHealthBar()
    {
        if (indicatorBar == null)
        {
            Debug.Log("Missing indicator bar!");
            return;
        }

        indicatorBar.SetSliderValue(currentHealth);
        indicatorBar.SetIndicatorText(currentHealth + " / " + maxHealth);
    }
    public virtual void OnUpdateHealthBar(float value)
    {
        if (indicatorBar == null)
        {
            Debug.Log("Missing indicator bar!");
            return;
        }

        indicatorBar.SetSliderValue(value);
        indicatorBar.SetIndicatorText(Math.Ceiling(value) + " / " + maxHealth);
    }
    #endregion

    // Method
    #region 
    // Add Health
    public virtual void AddHealth(int number)
    {
        int result = currentHealth;
        if (number <= 0) { return; }
        result += number;

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

        #endregion

        iTween.ValueTo(this.gameObject, updateSliderValueHash);

        currentHealth = result;
        //UpdateHealthBar();
    }

    // Remove Health
    public virtual void RemoveHealth(int number)
    {
        int result = currentHealth;
        if (number <= 0 || currentHealth == 0) { return; }
        result -= number;
        if (result <= 0) { result = 0; }

        // Hash Table
        #region 
        Hashtable updateSliderValueHash = new Hashtable();
        updateSliderValueHash.Add("name", this.gameObject.name + "_" + this.gameObject.GetInstanceID() + "_RemoveHealth_ValueTo");
        updateSliderValueHash.Add("from", currentHealth);
        updateSliderValueHash.Add("to", result);
        updateSliderValueHash.Add("time", 1f);
        updateSliderValueHash.Add("easetype", iTween.EaseType.easeOutSine);
        updateSliderValueHash.Add("onupdate", "OnUpdateHealthBar");
        updateSliderValueHash.Add("onupdatetarget", this.gameObject);

        #endregion

        iTween.ValueTo(this.gameObject, updateSliderValueHash);

        currentHealth = result;
        //UpdateHealthBar();
        CheckHealth();
    }

    // Check Health
    #region 
    public virtual void CheckHealth()
    {
        if (currentHealth > 0) { return; }
        DestroySelf();
    }
    #endregion

    // Destroy Self
    #region
    public virtual void DestroySelf()
    {
        Destroy(this.gameObject);
    }
    #endregion

    #endregion

    // On Action Handler
    #region 
    [SerializeField] private RadialProgressBar radialProgressBar;
    [SerializeField][Min(0)] private int maxActionCounter = 3;
    [Min(0)] private int currentActionCounter;
    private bool isAction = false;
    private void SetupRadialProgressBar()
    {
        currentActionCounter = maxActionCounter;
        radialProgressBar.SetFillAmount(1);
        radialProgressBar.SetText(maxActionCounter.ToString());
    }
    public virtual void OnActionHandler(int value)
    {
        //iTween.StopByName(this.gameObject.name + "_" + this.gameObject.GetInstanceID() + "_FillAmount_ValueTo");
        
        float lastFillAmount = radialProgressBar.GetFillAmount();
        currentActionCounter--;
        float fillAmount = (float)currentActionCounter / (float)maxActionCounter;

        // Hash Table
        #region 
        Hashtable updateFillAmountHash = new Hashtable();
        updateFillAmountHash.Add("name", this.gameObject.name + "_" + this.gameObject.GetInstanceID() + "_FillAmount_ValueTo");
        updateFillAmountHash.Add("from", lastFillAmount);
        updateFillAmountHash.Add("to", fillAmount);
        updateFillAmountHash.Add("time", 0.25f);
        updateFillAmountHash.Add("easetype", iTween.EaseType.easeOutSine);
        updateFillAmountHash.Add("onupdate", "OnUpdateRadialProgressBar");
        updateFillAmountHash.Add("onupdatetarget", this.gameObject);
        updateFillAmountHash.Add("oncomplete", "OnCompleteUpdateRadialProgressBar");
        updateFillAmountHash.Add("oncompletetarget", this.gameObject);

        #endregion

        if (isAction) { return; }
        if (currentActionCounter <= 0){ isAction = true; }

        iTween.ValueTo(this.gameObject, updateFillAmountHash);
        
    }

    private void OnUpdateRadialProgressBar(float fillAmount)
    {
        radialProgressBar.SetFillAmount(fillAmount);
    }

    private void OnCompleteUpdateRadialProgressBar()
    {
        radialProgressBar.SetText(currentActionCounter.ToString());
        
        float lastFillAmount = radialProgressBar.GetFillAmount();
        // Hash Table
        #region 
        Hashtable resetFillAmountHash = new Hashtable();
        resetFillAmountHash.Add("name", this.gameObject.name + "_" + this.gameObject.GetInstanceID() + "_ResetFillAmount_ValueTo");
        resetFillAmountHash.Add("from", lastFillAmount);
        resetFillAmountHash.Add("to", 1f);
        resetFillAmountHash.Add("time", 1f);
        resetFillAmountHash.Add("delay", 1f);
        resetFillAmountHash.Add("easetype", iTween.EaseType.easeOutSine);
        resetFillAmountHash.Add("onupdate", "OnUpdateRadialProgressBar");
        resetFillAmountHash.Add("onupdatetarget", this.gameObject);
        resetFillAmountHash.Add("oncomplete", "OnCompleteResetRadialProgressBar");
        resetFillAmountHash.Add("oncompletetarget", this.gameObject);

        #endregion

        if (currentActionCounter > 0) { return; }
        PlayerHealthHandler.GetInstance().RemoveHealth(1);
        iTween.ValueTo(this.gameObject, resetFillAmountHash);
        
    }

    private void OnCompleteResetRadialProgressBar()
    { 
        currentActionCounter = maxActionCounter;
        radialProgressBar.SetText(currentActionCounter.ToString());
        isAction = false;
    }

    #endregion

    // Collision Handler
    #region 
    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();
        if (ball == null) { return; }

        RemoveHealth(1);
    }
    #endregion

    public virtual void Awake()
    {
        // Set Component
        #region 
        SetComponent();
        #endregion

        SetupEnemy();

        // Set Valuable
        #region 
        SetValuable();
        #endregion

        SetupIndicatorBar();
        UpdateHealthBar();

        SetupRadialProgressBar();
        
    }

    public virtual void Start()
    { 
        Void_Area.GetInstance().OnDestryCountChange += OnActionHandler;
    }

}
