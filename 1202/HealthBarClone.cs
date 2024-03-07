using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarClone : CharacterBehaviour, IHealthBarClone
{
    private ICharacterUIHandler m_characterUIHandler;
    public Transform barParent;
    public float intervalTime = 5f;
    private float timer;
    private GameObject[] barList = new GameObject[4];
    private HealthBar bar;
    void Start()
    {
        GetModule(out m_characterUIHandler);
        barList[0] = Instantiate(m_characterUIHandler.CharacterUIManager.healthBar.gameObject, barParent);
        barList[1] = Instantiate(m_characterUIHandler.CharacterUIManager.oxygenBar.gameObject, barParent);
        barList[2] = Instantiate(m_characterUIHandler.CharacterUIManager.waterBar.gameObject, barParent);
        barList[3] = Instantiate(m_characterUIHandler.CharacterUIManager.hungerBar.gameObject, barParent);
        timer = 0f;
    }


    void Update()
    {
        if (Time.time >= timer)
        {
            RefreshBars();
            timer = Time.time + intervalTime;
        }
        
    }
    public void RefreshBars()
    {
        //Debug.Log("Refresh One Time");
        //Debug.Log(barList[0].GetComponent<HealthBar>().hpImg.fillAmount);

        for (int i=0; i<barList.Length; i++)
        {
            CopyBarToClone(i);
        }
    }
    void CopyBarToClone(int index)
    {
        switch (index)
        {
            case 0:
                bar = m_characterUIHandler.CharacterUIManager.healthBar;
                break;
            case 1:
                bar = m_characterUIHandler.CharacterUIManager.oxygenBar;
                break;
            case 2:
                bar = m_characterUIHandler.CharacterUIManager.waterBar;
                break;
            case 3:
                bar = m_characterUIHandler.CharacterUIManager.hungerBar;
                break;
        }
        //¿½±´ÑªÌõÊýÖµ
        barList[index].GetComponent<HealthBar>().hpImg.fillAmount = bar.currentHp / bar.maxHp;
        barList[index].GetComponent<HealthBar>().hpEffectImg.fillAmount = barList[index].GetComponent<HealthBar>().hpImg.fillAmount;
        //¿½±´ÑªÌõ±³¾°
        barList[index].GetComponent<HealthBar>().hpImg.sprite = bar.hpImg.sprite;
        barList[index].GetComponent<HealthBar>().bg.sprite = bar.bg.sprite;
        //¿½±´icon
        barList[index].GetComponent<HealthBar>().icon.sprite = bar.icon.sprite;
    }
}
