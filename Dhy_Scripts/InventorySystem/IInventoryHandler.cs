using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventoryHandler : ICharacterModule
{
    bool InspectionActive { get; }
    bool TryStartInspection();
    bool TryStopInspection(BuildableDefinition buildableDef);
    //bool TryStopInspection();
}
