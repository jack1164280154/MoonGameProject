using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputBehaviour : MonoBehaviour
{
    [SerializeField]
    private InputContext m_Context;
    protected virtual void OnInputEnabled() { }
    protected virtual void OnInputDisabled() { }
    protected virtual void TickInput() { }

    protected virtual void OnEnable()
    {
        if (m_Context == null)
        {
            OnInputEnabled();
            return;
        }

        m_Context.ContextEnabled += OnInputEnabled;
        m_Context.ContextDisabled += OnInputDisabled;

        if (m_Context.IsEnabled)
            OnInputEnabled();
    }

    protected virtual void OnDisable()
    {
        if (m_Context == null)
        {
            OnInputDisabled();
            return;
        }

        m_Context.ContextEnabled -= OnInputEnabled;
        m_Context.ContextDisabled -= OnInputDisabled;

        if (m_Context.IsEnabled)
            OnInputDisabled();
    }

    protected bool IsContextEnabled() => m_Context.IsEnabled;

    private void Update()
    {
        if (m_Context == null || m_Context.IsEnabled)
            TickInput();
    }
}
