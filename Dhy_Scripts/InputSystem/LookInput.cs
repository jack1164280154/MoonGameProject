using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LookInput : CharacterInputBehaviour
{
    [Header("Actions")]
    [SerializeField]
    private InputActionReference m_LookInput;

    private ILookHandler m_LookHandler;


    #region Initialization
    protected override void OnBehaviourEnabled(ICharacter character)
    {
        character.GetModule(out m_LookHandler);
    }

    protected override void OnInputEnabled()
    {
        m_LookInput.Enable();
        m_LookHandler.SetLookInput(GetInput);
    }

    protected override void OnInputDisabled()
    {
        m_LookInput.TryDisable();
        m_LookHandler.SetLookInput(null);
    }
    #endregion

    #region Input Handling
    private Vector2 GetInput()
    {
        Vector2 lookInput = m_LookInput.action.ReadValue<Vector2>() * 0.1f;
        //lookInput.ReverseVector();
        float xValue = lookInput.x;
        lookInput.x = lookInput.y;
        lookInput.y = xValue;
        return lookInput;
    }
    #endregion
}
