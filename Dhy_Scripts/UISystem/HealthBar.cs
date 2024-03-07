using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image hpImg;
    public Image hpEffectImg;
    public Image icon;
    public Image bg;
    public float maxHp = 100f;
    public float currentHp;
    public float buffTime = 0.8f;

    private Coroutine updateCoroutine;
    private void Start()
    {
        currentHp = maxHp;
        icon = transform.GetChild(0).GetComponent<Image>();
        bg = transform.GetChild(1).GetChild(0).GetComponent<Image>();
        hpEffectImg = transform.GetChild(1).GetChild(1).GetComponent<Image>();
        hpImg = transform.GetChild(1).GetChild(2).GetComponent<Image>();
        UpdataHealthBar();
    }

    public void SetHealth(float health)
    {
        currentHp = Mathf.Clamp(health, 0f, maxHp);
        if (this.gameObject.activeInHierarchy)
        {
            UpdataHealthBar();
        }
        
        if (currentHp <= 0)
        {
            //Die();
        }
    }
    public void IncreaseHealth(float amount)
    {
        SetHealth(currentHp + amount);
    }
    public void DecreaseHealth(float amount)
    {
        SetHealth(currentHp - amount);
    }
    private void UpdataHealthBar()
    {
        hpImg.fillAmount = currentHp / maxHp;

        if (updateCoroutine != null)
        {
            StopCoroutine(updateCoroutine);
        }

        updateCoroutine = StartCoroutine(UpdateHpEffet());
    }
    private IEnumerator UpdateHpEffet()
    {
        float effectLength = hpEffectImg.fillAmount - hpImg.fillAmount;
        float elapsedTime = 0f;
        while(elapsedTime<buffTime && effectLength != 0)
        {
            elapsedTime += Time.deltaTime;
            hpEffectImg.fillAmount = Mathf.Lerp(hpImg.fillAmount + effectLength, hpImg.fillAmount, elapsedTime / buffTime);
            yield return null;
        }

        hpEffectImg.fillAmount = hpImg.fillAmount;
    }
}
