using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPostProcess : ICharacterModule
{
    void BeginGlobalBlur();
    void CloseGlobalBlur();
}
