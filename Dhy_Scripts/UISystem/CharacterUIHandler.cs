using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BarType
{
    HealthBar,
    OxygenBar,
    WaterBar,
    HungryBar
}
public class CharacterUIHandler : PlayerUIBehaviour, ICharacterUIHandler
{
    private GameObject characterUIView;
    public CharacterUIManager CharacterUIManager { get => characterUIManager; }
    private CharacterUIManager characterUIManager;
    new void Start()
    {
        base.Start();
        CreateOrOpenView();

    }
    void Update()
    {

    }
    public void ChangeToWarningUI(BarType barType)
    {
        characterUIManager.ChangeToWarningUI(barType);
    }
    public void ChangeToNormalUI(BarType barType)
    {
        characterUIManager.ChangeToNormalUI(barType);
    }
    public void CreateOrOpenView()
    {
        if (characterUIView == null)
        {
            GameObject uiview = Resources.Load<GameObject>("UIViews/CharacterUI");
            characterUIView = Instantiate(uiview, transform);
            characterUIManager = characterUIView.GetComponent<CharacterUIManager>();
        }
        else
        {
            characterUIView.SetActive(true);
        }
    }
    public void ShowAimUI()
    {
        characterUIManager.ShowAimUI();
    }
    public void CloseAimUI()
    {
        characterUIManager.CloseAimUI();
    }
    public void EnableAimShake()
    {
        characterUIManager.AimUIShake();
    }
    public void CloseView()
    {
        characterUIView.SetActive(false);
    }
    public void IncreaseValue(float value, BarType barType)
    {
        switch (barType)
        {
            case BarType.HealthBar:
                IncreaseHp(value);
                break;
            case BarType.OxygenBar:
                IncreaseOxygen(value);
                break;
            case BarType.WaterBar:
                IncreaseWater(value);
                break;
            case BarType.HungryBar:
                IncreaseHungry(value);
                break;
            default:
                break;
        }
    }
    public void DecreaseValue(float value, BarType barType)
    {
        switch (barType)
        {
            case BarType.HealthBar:
                DecreaseHp(value);
                break;
            case BarType.OxygenBar:
                DecreaseOxygen(value);
                break;
            case BarType.WaterBar:
                DecreaseWater(value);
                break;
            case BarType.HungryBar:
                DecreaseHungry(value);
                break;
            default:
                break;
        }
    }
    void IncreaseHp(float value)
    {
        if (Player.health + value >= Player.maxHealth)
        {
            Player.health = Player.maxHealth;
        }
        else
        {
            Player.health += value;
        }
        characterUIManager.healthBar.IncreaseHealth(value);
    }
    void DecreaseHp(float value)
    {
        if (Player.health <= value)
        {
            Player.health = 0;
            
        }
        else
        {
            Player.health -= value;
        }
        characterUIManager.healthBar.DecreaseHealth(value);
    }
    void IncreaseOxygen(float value)
    {
        if (Player.oxygen + value >= Player.maxOxygen)
        {
            Player.oxygen = Player.maxOxygen;
        }
        else
        {
            Player.oxygen += value;
        }
        characterUIManager.oxygenBar.IncreaseHealth(value);
    }
    void DecreaseOxygen(float value)
    {
        if (Player.oxygen <= value)
        {
            Player.oxygen = 0;

        }
        else
        {
            Player.oxygen -= value;
        }
        characterUIManager.oxygenBar.DecreaseHealth(value);
    }
    void IncreaseWater(float value)
    {
        if (Player.water + value >= Player.maxWater)
        {
            Player.water = Player.maxWater;
        }
        else
        {
            Player.water += value;
        }
        characterUIManager.waterBar.IncreaseHealth(value);
    }
    void DecreaseWater(float value)
    {
        if (Player.water <= value)
        {
            Player.water = 0;

        }
        else
        {
            Player.water -= value;
        }
        characterUIManager.waterBar.DecreaseHealth(value);
    }

    void IncreaseHungry(float value)
    {
        if (Player.hungry + value >= Player.maxHungry)
        {
            Player.hungry = Player.maxHungry;
        }
        else
        {
            Player.hungry += value;
        }
        characterUIManager.hungerBar.IncreaseHealth(value);
    }
    void DecreaseHungry(float value)
    {
        if (Player.hungry <= value)
        {
            Player.hungry = 0;

        }
        else
        {
            Player.hungry -= value;
        }
        characterUIManager.hungerBar.DecreaseHealth(value);
    }
    void UpdateCharacterUIView()
    {

    }


}
