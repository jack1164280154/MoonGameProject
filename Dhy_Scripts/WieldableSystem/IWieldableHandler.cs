using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWieldableHandler : ICharacterModule
{
    GameObject Hammer { get; }
    void OnMouseButtonDown();
    void ChangeEquipmentByIndex(int index);
}
