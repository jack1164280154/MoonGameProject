using UnityEngine;
using System;

[CreateAssetMenu(menuName = "SurvivalGames/Input/Input Context Group", fileName = "(InputContextGroup) ")]
public class InputContextGroup : InputContextBase
{
    [SerializeField]
    private InputContext[] m_Subcontexts = Array.Empty<InputContext>();


    internal override void EnableContext()
    {
        foreach (var subContext in m_Subcontexts)
            subContext.EnableContext();

        base.EnableContext();
    }

    internal override void DisableContext(InputContextBase newContext)
    {
        foreach (var subContext in m_Subcontexts)
        {
            if (!newContext.ContainsSubContext(subContext))
                subContext.DisableContext(newContext);
        }

        base.DisableContext(newContext);
    }

    internal override InputContextBase[] GetSubContexts() => m_Subcontexts;
}
