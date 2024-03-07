using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacterUIHandler : ICharacterModule
{
    CharacterUIManager CharacterUIManager { get; }
    void CreateOrOpenView();
    void CloseView();
    void IncreaseValue(float value, BarType barType);
    void DecreaseValue(float value, BarType barType);
    void ChangeToWarningUI(BarType barType);
    void ChangeToNormalUI(BarType barType);
    void ShowAimUI();
    void CloseAimUI();
    void EnableAimShake();
}
