using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructManager : Singleton<ConstructManager>
{
    private ConstructManager()
    {

    }
    //调用ConstructManager的方法，创建一个Construct物体
    public void ActiveConstructionPlacement(string itemToConstruction)
    {
        GameObject item = GameObject.Instantiate(Resources.Load<GameObject>("BuildSystem/"+itemToConstruction));
    }
}
