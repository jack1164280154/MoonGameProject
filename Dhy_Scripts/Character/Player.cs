using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : Character
{
    public float health;
    public float oxygen;
    public float water;
    public float hungry;
    public float maxHealth = 100f;
    public float maxOxygen = 100f;
    public float maxWater = 100f;
    public float maxHungry = 100f;
    private bool isHungryWarning = false;
    private bool isHealthWarning = false;
    private bool isWaterWarning = false;
    private bool isOxygenWarning = false;
    //public AnimationCurve hungerCurve;
    private ICharacterUIHandler m_characterUIHandler;
    public float decreaseValue;
    public float oxygenRate;
    public float waterRate;
    public float healthRate;
    private float timeToInit;
    public static Player LocalPlayer
    {
        get => s_LocalPlayer;
        private set
        {
            if (s_LocalPlayer == value)
                return;

            s_LocalPlayer = value;
            LocalPlayerChanged?.Invoke(s_LocalPlayer);
        }
    }
    /// <summary>
    /// This message will be sent after the first initialized action.
    /// </summary>
    public event UnityAction AfterInitialized;

    /// <summary>
    ///  Player: Current Player
    /// </summary>
    public static event PlayerChangedDelegate LocalPlayerChanged;
    public delegate void PlayerChangedDelegate(Player player);

    private static Player s_LocalPlayer;

    protected override void Awake()
    {
        if (LocalPlayer != null)
            Destroy(this);
        else
        {
            LocalPlayer = this;
            base.Awake();
        }
    }
    protected override void Start()
    {
        base.Start();
        health = maxHealth;
        oxygen = maxOxygen;
        water = maxWater;
        hungry = maxHungry;
        //5·ÖÖÓ´Ó100-0
        decreaseValue = 1/15f;
        timeToInit = 5f;
        waterRate = 0.7f;
        oxygenRate = 5f;
        healthRate = 3f;
        GetModule(out m_characterUIHandler);
        AfterInitialized?.Invoke();
    }
    private void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (Time.time > timeToInit)
        {
            DecreaseHungry();
            DecreaseOxygen();
            DecreaseWater();
        }
        if (water <= 10f)
        {
            if (!isWaterWarning)
            {
                m_characterUIHandler.ChangeToWarningUI(BarType.WaterBar);
                isWaterWarning = true;
            }
        }
        else
        {
            if (isWaterWarning)
            {
                m_characterUIHandler.ChangeToNormalUI(BarType.WaterBar);
                isWaterWarning = false;
            }
        }
        if (oxygen <= 50f)
        {
            if (!isOxygenWarning)
            {
                m_characterUIHandler.ChangeToWarningUI(BarType.OxygenBar);
                isOxygenWarning = true;
            }
        }
        else
        {
            if (isOxygenWarning)
            {
                m_characterUIHandler.ChangeToNormalUI(BarType.OxygenBar);
                isOxygenWarning = false;
            }
            
        }
        if (health <= 45f)
        {
            if (!isHealthWarning)
            {
                m_characterUIHandler.ChangeToWarningUI(BarType.HealthBar);
                isHealthWarning = true;
            }
        }
        else
        {
            if (isHealthWarning)
            {
                m_characterUIHandler.ChangeToNormalUI(BarType.HealthBar);
                isHealthWarning = false;
            }

        }
        if (hungry <= 20f)
        {
            if (!isHungryWarning)
            {
                m_characterUIHandler.ChangeToWarningUI(BarType.HungryBar);
                isHungryWarning = true;
            }
            if (hungry <= 5f)
            {
                DecreaseHealth();
            }
        }
        else
        {
            if (isHungryWarning)
            {
                m_characterUIHandler.ChangeToNormalUI(BarType.HungryBar);
                isHungryWarning = false;
            }
            
        }
    }
    private void OnDestroy()
    {
        if (LocalPlayer == this)
            LocalPlayer = null;
    }
    private void DecreaseHungry()
    {
        m_characterUIHandler.DecreaseValue(decreaseValue, BarType.HungryBar);
    }
    private void DecreaseOxygen()
    {
        m_characterUIHandler.DecreaseValue(decreaseValue * oxygenRate, BarType.OxygenBar);

    }
    private void DecreaseWater()
    {
        m_characterUIHandler.DecreaseValue(decreaseValue * waterRate, BarType.WaterBar);

    }
    private void DecreaseHealth()
    {
        m_characterUIHandler.DecreaseValue(decreaseValue * healthRate, BarType.HealthBar);
    }
}
