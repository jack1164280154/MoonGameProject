using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyComponent : MonoBehaviour
{
    public GameObject sourceObject; // Դ��������Ҫ��������Ķ���
    public GameObject targetObject; // Ŀ���������Ҫ��������Ƶ��Ķ���

    void Start()
    {
        if (sourceObject != null && targetObject != null)
        {
            // ��ȡԴ�����ϵ��������
            Component[] componentsToCopy = sourceObject.GetComponents<Component>();

            // ������������������Ǹ��Ƶ�Ŀ�����
            foreach (Component component in componentsToCopy)
            {
                System.Type componentType = component.GetType();
                Component newComponent = targetObject.GetComponent(componentType);

                if (newComponent == null)
                {
                    // ���Ŀ�������û����ͬ���͵�����������һ���µ����
                    newComponent = targetObject.AddComponent(componentType);
                }

                // ��Դ�����ϵ�������Ը��Ƶ�Ŀ������ϵ������
                System.Reflection.FieldInfo[] fields = componentType.GetFields();
                foreach (System.Reflection.FieldInfo field in fields)
                {
                    field.SetValue(newComponent, field.GetValue(component));
                }

                // ������Ҳ���Ը������ԡ��������¼��ȣ����������
            }

            Debug.Log("����������");
        }
        else
        {
            Debug.LogError("�����Դ�����Ŀ�����");
        }
    }
}