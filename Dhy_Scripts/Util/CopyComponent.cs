using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyComponent : MonoBehaviour
{
    public GameObject sourceObject; // 源对象，你想要复制组件的对象
    public GameObject targetObject; // 目标对象，你想要将组件复制到的对象

    void Start()
    {
        if (sourceObject != null && targetObject != null)
        {
            // 获取源对象上的所有组件
            Component[] componentsToCopy = sourceObject.GetComponents<Component>();

            // 遍历所有组件并将它们复制到目标对象
            foreach (Component component in componentsToCopy)
            {
                System.Type componentType = component.GetType();
                Component newComponent = targetObject.GetComponent(componentType);

                if (newComponent == null)
                {
                    // 如果目标对象上没有相同类型的组件，就添加一个新的组件
                    newComponent = targetObject.AddComponent(componentType);
                }

                // 将源对象上的组件属性复制到目标对象上的新组件
                System.Reflection.FieldInfo[] fields = componentType.GetFields();
                foreach (System.Reflection.FieldInfo field in fields)
                {
                    field.SetValue(newComponent, field.GetValue(component));
                }

                // 这里你也可以复制属性、方法、事件等，视情况而定
            }

            Debug.Log("组件复制完成");
        }
        else
        {
            Debug.LogError("请分配源对象和目标对象");
        }
    }
}