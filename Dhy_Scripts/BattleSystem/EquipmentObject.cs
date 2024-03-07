using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentObject : InteractableObject
{
    BoxCollider colliderToShowInfo;
    BoxCollider colliderPhysical;
    void Start()
    {
        BoxCollider[] colliders = GetComponents<BoxCollider>();
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].isTrigger)
            {
                colliderToShowInfo = colliders[i];
            }
            else
            {
                colliderPhysical = colliders[i];
            }
        }
    }
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log(other.name);
            //ÏÔÊ¾uiÐÅÏ¢
        }
    }
}
