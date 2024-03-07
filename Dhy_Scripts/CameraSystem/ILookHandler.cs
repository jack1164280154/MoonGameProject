using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface ILookHandler : ICharacterModule
{
    event UnityAction PostViewUpdate;
    void SetLookInput(LookHandlerInputDelegate input);
    void ChangeViewToFirstLook();
    void ChangeViewToThirdLook();
    void ChangeViewToShoulderLook();

}
public delegate Vector2 LookHandlerInputDelegate();