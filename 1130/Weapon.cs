using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SurvivalGames.Util;

public class Weapon : Equipment
{
    List<GameObject> m_fireEffect = new List<GameObject>();
    public string effectPath;
    [SerializeField]
    Transform muzzle;
    int offset;
    public void OnFire()
    {
        DebugUtil.Log("OnFire");
        //使用对象池生成子弹
        GameObject fireEffect = PoolManager.Instance.GetObjFromPool(effectPath, this.transform);
        fireEffect.transform.localPosition = muzzle.localPosition;
        fireEffect.transform.localRotation = muzzle.localRotation;
        fireEffect.transform.localScale = muzzle.localScale;
        //Instantiate(fireEffect,transform);
        m_fireEffect.Add(fireEffect);
        Invoke("Recycle",1f);
    }
    void Recycle()
    {
        PoolManager.Instance.ReleaseObj(m_fireEffect[offset]);
        offset++;
    }
}
