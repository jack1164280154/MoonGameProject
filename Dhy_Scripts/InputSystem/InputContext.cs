using UnityEngine;
using System;

[CreateAssetMenu(menuName = "SurvivalGames/Input/Input Context", fileName = "(InputContext) ")]
public sealed class InputContext : InputContextBase
{
    internal override InputContextBase[] GetSubContexts() => Array.Empty<InputContextBase>();
}
