using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectionManager : MonoBehaviour, ISelectionManager
{
    public GameObject interactableObj_UI;
    Text interactableObj_text;
    public Canvas canvas;
    float canvasW;
    float canvasH;

    void Start()
    {
        interactableObj_UI.SetActive(false);
        interactableObj_text = interactableObj_UI.GetComponentInChildren<Text>();

        //找到canvas
        /*Transform parentTransform = transform.parent;
        if (parentTransform != null)
        {
            Transform grandparentTransform = parentTransform.parent;
            if (grandparentTransform != null)
            {
                canvas = grandparentTransform.gameObject.GetComponent<Canvas>();
            }
        }*/
    }


    void Update()
    {
        RaycastToInteract();
    }
    public void ColliderToInteract()
    {

    }
    public void ShowInteractInfo(InteractableObject obj)
    {
        interactableObj_text.text = obj.GetItemName();
        //UI移到3D物体位置
        Vector3 pos = Camera.main.WorldToViewportPoint(obj.gameObject.transform.position + obj.tagOffset);//物体的世界坐标再加个高度，转换为viewport坐标
        canvasW = canvas.GetComponent<RectTransform>().rect.width;
        canvasH = canvas.GetComponent<RectTransform>().rect.height;
        interactableObj_UI.GetComponent<RectTransform>().localPosition = new Vector3((pos.x - 0.5f) * canvasW, (pos.y - 0.5f) * canvasH, 0);
        //激活UI显示
        interactableObj_UI.SetActive(true);
    }
    public void CloseInteractInfo()
    {
        interactableObj_UI.SetActive(false);
    }
    public void RaycastToInteract()
    {
        ////鼠标放到物体上，可交互物体显示UI - 也可以改为接近trigger后就显示
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0); // 屏幕中心点

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 5f))
        {
            //print("hit:" + hit.transform.name);

            if (hit.transform.GetComponent<InteractableObject>())
            {
                //获得名字
                InteractableObject selectedInte = hit.transform.GetComponent<InteractableObject>();
                interactableObj_text.text = selectedInte.GetItemName();

                //debug用
                print(interactableObj_text);



                //UI移到3D物体位置
                Vector3 pos = Camera.main.WorldToViewportPoint(selectedInte.gameObject.transform.position + selectedInte.tagOffset);//物体的世界坐标再加个高度，转换为viewport坐标
                canvasW = canvas.GetComponent<RectTransform>().rect.width;
                canvasH = canvas.GetComponent<RectTransform>().rect.height;
                interactableObj_UI.GetComponent<RectTransform>().localPosition = new Vector3((pos.x - 0.5f) * canvasW, (pos.y - 0.5f) * canvasH, 0);


                //激活UI显示
                interactableObj_UI.SetActive(true);

                if (Input.GetKeyDown(KeyCode.F))
                {
                    if (hit.transform.GetComponent<InteractableObject>().data != null)
                    {
                        TestCallAddItem.Instance.AddItem(hit.transform.GetComponent<InteractableObject>().data, 1);
                        Destroy(hit.transform.gameObject);
                    }
                }
            }
            else
            {
                interactableObj_UI.SetActive(false);
            }

        }
        else
        {
            interactableObj_UI.SetActive(false);
        }
    }
}