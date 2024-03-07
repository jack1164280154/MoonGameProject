using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using MxM;
using MxMGameplay;
[AddComponentMenu("InputSystem/InventoryInput")]
public class InventoryInput : CharacterInputBehaviour
{
    [SerializeField]
    private InputActionReference m_InventoryInput;

    [SerializeField]
    private MxMEventDefinition m_openDefinition = null;

    private IInventoryHandler m_BuildInventory;

    protected override void OnBehaviourEnabled(ICharacter character) => character.GetModule(out m_BuildInventory);
    protected override void OnInputEnabled()
    {
        m_InventoryInput.RegisterStarted(OnOpenBackPack);
    }
    protected override void OnInputDisabled()
    {
        m_InventoryInput.UnregisterStarted(OnOpenBackPack);
    }
    private void OnOpenBackPack(InputAction.CallbackContext ctx)
    {
        if (!m_BuildInventory.InspectionActive)
        {
            m_BuildInventory.TryStartInspection();
        }
        else
        {
            m_BuildInventory.TryStopInspection(null);
        }
    }
}
