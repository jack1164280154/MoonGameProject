using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AimUI : MonoBehaviour
{
    public Image[] aimLines;
    public string redLinePath;
    private Sprite whiteLine;
    private Sprite redLine;
    public float shakeAmount = 0.1f;
    public float shakeTime = 0.5f;
    private Coroutine shakeCoroutine;
    private Vector3[] prePosition = new Vector3[4];
    void Start()
    {
        //初始化aimLines
        aimLines = new Image[4];
        for (int i = 0; i < transform.childCount; i++)
        {
            aimLines[i] = transform.GetChild(i).GetComponent<Image>();
        }
        whiteLine = aimLines[0].sprite;
        redLine = Resources.Load<Sprite>(redLinePath);
    }
    //切换为红色
    void SwitchToRed()
    {
        for (int i = 0; i < aimLines.Length; i++)
        {
            aimLines[i].sprite = redLine;
        }
    }
    //切换回白色
    void SwitchToWhile()
    {
        for (int i = 0; i < aimLines.Length; i++)
        {
            aimLines[i].sprite = whiteLine;
        }
    }
    
    public void EnableShakeEffect()
    {
        if (shakeCoroutine != null)
        {
            StopCoroutine(shakeCoroutine);
        }
        //记录摇晃前的位置
        for (int i = 0; i < aimLines.Length; i++)
        {
            prePosition[i] = aimLines[i].transform.localPosition;
        }
        SwitchToRed();
        shakeCoroutine = StartCoroutine(ShakeEffect());
    }
    IEnumerator ShakeEffect()
    {
        float timer = 0f;
        Debug.Log("ShakeEffect");
        while (timer < shakeTime)
        {
            float offsetX = Mathf.PerlinNoise(Time.time, 0) * shakeAmount;
            float offsetY = Mathf.PerlinNoise(0, Time.time) * shakeAmount;
            /*for (int i = 0; i < aimLines.Length; i++) { 
            }*/
            aimLines[0].transform.localPosition += new Vector3(0f, offsetY, 0f);
            aimLines[1].transform.localPosition -= new Vector3(0f, offsetY, 0f);
            aimLines[2].transform.localPosition -= new Vector3(offsetX, 0f, 0f);
            aimLines[3].transform.localPosition += new Vector3(offsetX, 0f, 0f);
            timer += Time.deltaTime;
            yield return null;
        }
        //回到摇晃前的位置
        for (int i = 0; i < aimLines.Length; i++)
        {
            aimLines[i].transform.localPosition = prePosition[i];
        }
        SwitchToWhile();
    }
}
