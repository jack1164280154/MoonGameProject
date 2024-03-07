using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WieldableInput : CharacterInputBehaviour
{
    [SerializeField]
    private InputActionReference m_Numerickey1Input;
    [SerializeField]
    private InputActionReference m_Numerickey2Input;
    [SerializeField]
    private InputActionReference m_Numerickey3Input;
    [SerializeField]
    private InputActionReference m_Numerickey4Input;
    [SerializeField]
    private InputActionReference m_Numerickey5Input;
    [SerializeField]
    private InputActionReference m_Numerickey6Input;
    [SerializeField]
    private WieldableHandler wieldableHandler;

    //protected override void OnBehaviourEnabled(ICharacter character) => character.GetModule(out m_BuildInventory);
    /*private void OnEnable()
    {
        m_PlacePreviewInput.action.Enable();
        m_PlacePreviewInput.action.started += OnPlaceInput;
    }*/
    protected override void OnInputEnabled()
    {
        //m_BuildingCycleInput.RegisterStarted(OnCycleInput);
        m_Numerickey1Input.RegisterStarted(OnNumerickey1Input);
        m_Numerickey2Input.RegisterStarted(OnNumerickey2Input);
        m_Numerickey3Input.RegisterStarted(OnNumerickey3Input);
        m_Numerickey4Input.RegisterStarted(OnNumerickey4Input);
        m_Numerickey5Input.RegisterStarted(OnNumerickey5Input);
        m_Numerickey6Input.RegisterStarted(OnNumerickey6Input);
    }

    protected override void OnInputDisabled()
    {
        m_Numerickey1Input.UnregisterStarted(OnNumerickey1Input);
        m_Numerickey2Input.UnregisterStarted(OnNumerickey2Input);
        m_Numerickey3Input.UnregisterStarted(OnNumerickey3Input);
        m_Numerickey4Input.UnregisterStarted(OnNumerickey4Input);
        m_Numerickey5Input.UnregisterStarted(OnNumerickey5Input);
        m_Numerickey6Input.UnregisterStarted(OnNumerickey6Input);
    }
    private void OnNumerickey1Input(InputAction.CallbackContext ctx) => wieldableHandler.ChangeEquipmentByIndex(0);
    private void OnNumerickey2Input(InputAction.CallbackContext ctx) => wieldableHandler.ChangeEquipmentByIndex(1);
    private void OnNumerickey3Input(InputAction.CallbackContext ctx) => wieldableHandler.ChangeEquipmentByIndex(2);
    private void OnNumerickey4Input(InputAction.CallbackContext ctx) => wieldableHandler.ChangeEquipmentByIndex(3);
    private void OnNumerickey5Input(InputAction.CallbackContext ctx) => wieldableHandler.ChangeEquipmentByIndex(4);
    private void OnNumerickey6Input(InputAction.CallbackContext ctx) => wieldableHandler.ChangeEquipmentByIndex(5);
}
