using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdMovementInput : InputBehaviour
{
    [SerializeField]
    private InputActionReference m_MovementInput;
    protected override void OnInputEnabled()
    {
        m_MovementInput.RegisterStarted(OnMovement);
    }
    protected override void OnInputDisabled()
    {
        m_MovementInput.UnregisterStarted(OnMovement);
    }
    private void OnMovement(InputAction.CallbackContext ctx)
    {

    }
}
