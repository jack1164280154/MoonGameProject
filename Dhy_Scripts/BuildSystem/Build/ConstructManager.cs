using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructManager : Singleton<ConstructManager>
{
    private ConstructManager()
    {

    }
    //����ConstructManager�ķ���������һ��Construct����
    public void ActiveConstructionPlacement(string itemToConstruction)
    {
        GameObject item = GameObject.Instantiate(Resources.Load<GameObject>("BuildSystem/"+itemToConstruction));
    }
}
