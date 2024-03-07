using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISelectionManager : ICharacterModule
{
    void ShowInteractInfo(InteractableObject obj);
    void CloseInteractInfo();
}
