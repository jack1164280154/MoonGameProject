using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeView : MonoBehaviour
{
    public GameObject firstPersonCamera;
    public GameObject thridPersonCamera;
    private bool isFirstView;
    void Start()
    {
        //firstPersonCamera = GameObject.Find("FirstPersonLookCamera");
        //thridPersonCamera = GameObject.Find("ThridPersonLookCamera");
        isFirstView = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (isFirstView)
            {
                firstPersonCamera.SetActive(false);
                thridPersonCamera.SetActive(true);
                isFirstView = false;
            }
            else
            {
                firstPersonCamera.SetActive(true);
                thridPersonCamera.SetActive(false);
                isFirstView = true;
            }
        }
    }
}
