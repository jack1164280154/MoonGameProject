using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FPSBuildingInput : CharacterInputBehaviour
{
    [SerializeField]
    private InputActionReference m_PlacePreviewInput;
    [SerializeField]
    private InputActionReference m_BuildingRotateInput;
    [SerializeField]
    private CharacterBuildController m_BuildingController;
    //protected override void OnBehaviourEnabled(ICharacter character) => character.GetModule(out m_BuildInventory);
    /*private void OnEnable()
    {
        m_PlacePreviewInput.action.Enable();
        m_PlacePreviewInput.action.started += OnPlaceInput;
    }*/
    protected override void OnInputEnabled()
    {
        //m_BuildingCycleInput.RegisterStarted(OnCycleInput);
        m_BuildingRotateInput.RegisterPerformed(OnRotateInput);
        m_PlacePreviewInput.RegisterStarted(OnPlaceInput);
    }

    protected override void OnInputDisabled()
    {
        //m_BuildingCycleInput.UnregisterStarted(OnCycleInput);
        m_BuildingRotateInput.UnregisterPerfomed(OnRotateInput);
        m_PlacePreviewInput.UnregisterStarted(OnPlaceInput);
    }
    private void OnPlaceInput(InputAction.CallbackContext ctx) => m_BuildingController.PlaceBuildable();

    private void OnRotateInput(InputAction.CallbackContext ctx) => m_BuildingController.RotationOffset += (ctx.ReadValue<float>() / 120f);
}
