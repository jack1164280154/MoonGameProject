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

        //�ҵ�canvas
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
        //UI�Ƶ�3D����λ��
        Vector3 pos = Camera.main.WorldToViewportPoint(obj.gameObject.transform.position + obj.tagOffset);//��������������ټӸ��߶ȣ�ת��Ϊviewport����
        canvasW = canvas.GetComponent<RectTransform>().rect.width;
        canvasH = canvas.GetComponent<RectTransform>().rect.height;
        interactableObj_UI.GetComponent<RectTransform>().localPosition = new Vector3((pos.x - 0.5f) * canvasW, (pos.y - 0.5f) * canvasH, 0);
        //����UI��ʾ
        interactableObj_UI.SetActive(true);
    }
    public void CloseInteractInfo()
    {
        interactableObj_UI.SetActive(false);
    }
    public void RaycastToInteract()
    {
        ////���ŵ������ϣ��ɽ���������ʾUI - Ҳ���Ը�Ϊ�ӽ�trigger�����ʾ
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0); // ��Ļ���ĵ�

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 5f))
        {
            //print("hit:" + hit.transform.name);

            if (hit.transform.GetComponent<InteractableObject>())
            {
                //�������
                InteractableObject selectedInte = hit.transform.GetComponent<InteractableObject>();
                interactableObj_text.text = selectedInte.GetItemName();

                //debug��
                print(interactableObj_text);



                //UI�Ƶ�3D����λ��
                Vector3 pos = Camera.main.WorldToViewportPoint(selectedInte.gameObject.transform.position + selectedInte.tagOffset);//��������������ټӸ��߶ȣ�ת��Ϊviewport����
                canvasW = canvas.GetComponent<RectTransform>().rect.width;
                canvasH = canvas.GetComponent<RectTransform>().rect.height;
                interactableObj_UI.GetComponent<RectTransform>().localPosition = new Vector3((pos.x - 0.5f) * canvasW, (pos.y - 0.5f) * canvasH, 0);


                //����UI��ʾ
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