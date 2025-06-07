using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Quest_Studio;
using Unity.Mathematics;
using System;

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
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public virtual void Start()
    {

    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }
}
