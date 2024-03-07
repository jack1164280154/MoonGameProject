using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CharacterUIManager : MonoBehaviour
{
    public HealthBar healthBar;
    public HealthBar oxygenBar;
    public HealthBar waterBar;
    public HealthBar hungerBar;
    public string baseHealth;
    public string baseOxygen;
    public string baseWater;
    public string baseHunger;
    public string warningHealth;
    public string warningOxygen;
    public string warningWater;
    public string warningHunger;

    public string baseHpBg;
    public string baseFront;
    public string hpWarningBg;
    public string hpWarningFront;

    public AimUI aimUI;
    public void Start()
    {

    }
    public void ChangeToNormalUI(BarType barType)
    {
        switch (barType)
        {
            case BarType.HealthBar:
                healthBar.icon.sprite = Resources.Load<Sprite>(baseHealth);
                healthBar.hpImg.sprite = Resources.Load<Sprite>(baseFront);
                healthBar.bg.sprite = Resources.Load<Sprite>(baseHpBg);
                break;
            case BarType.OxygenBar:
                oxygenBar.icon.sprite = Resources.Load<Sprite>(baseOxygen);
                oxygenBar.hpImg.sprite = Resources.Load<Sprite>(baseFront);
                oxygenBar.bg.sprite = Resources.Load<Sprite>(baseHpBg);
                break;
            case BarType.WaterBar:
                waterBar.icon.sprite = Resources.Load<Sprite>(baseWater);
                waterBar.hpImg.sprite = Resources.Load<Sprite>(baseFront);
                waterBar.bg.sprite = Resources.Load<Sprite>(baseHpBg);
                break;
            case BarType.HungryBar:
                hungerBar.icon.sprite = Resources.Load<Sprite>(baseHunger);
                hungerBar.hpImg.sprite = Resources.Load<Sprite>(baseFront);
                hungerBar.bg.sprite = Resources.Load<Sprite>(baseHpBg);
                break;
            default:
                break;
        }
    }
    public void ChangeToWarningUI(BarType barType)
    {
        switch (barType)
        {
            case BarType.HealthBar:
                healthBar.icon.sprite = Resources.Load<Sprite>(warningHealth);
                healthBar.hpImg.sprite = Resources.Load<Sprite>(hpWarningFront);
                healthBar.bg.sprite = Resources.Load<Sprite>(hpWarningBg);
                break;
            case BarType.OxygenBar:
                oxygenBar.icon.sprite = Resources.Load<Sprite>(warningOxygen);
                oxygenBar.hpImg.sprite = Resources.Load<Sprite>(hpWarningFront);
                oxygenBar.bg.sprite = Resources.Load<Sprite>(hpWarningBg);
                break;
            case BarType.WaterBar:
                waterBar.icon.sprite = Resources.Load<Sprite>(warningWater);
                waterBar.hpImg.sprite = Resources.Load<Sprite>(hpWarningFront);
                waterBar.bg.sprite = Resources.Load<Sprite>(hpWarningBg);
                break;
            case BarType.HungryBar:
                hungerBar.icon.sprite = Resources.Load<Sprite>(warningHunger);
                hungerBar.hpImg.sprite = Resources.Load<Sprite>(hpWarningFront);
                hungerBar.bg.sprite = Resources.Load<Sprite>(hpWarningBg);
                break;
            default:
                break;
        }

    }
    public void ShowAimUI()
    {
        aimUI.gameObject.SetActive(true);
    }
    public void CloseAimUI()
    {
        aimUI.gameObject.SetActive(false);
    }
    public void AimUIShake()
    {
        aimUI.EnableShakeEffect();
    }
}
