using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Polymind Games/Building/Buildable Category", fileName = "(BuildableCategory) ")]
public sealed class BuildableCategoryDefinition : GroupDefinition<BuildableCategoryDefinition, BuildableDefinition>
{
    public override Sprite Icon => m_Icon;

    [SerializeField]
    private Sprite m_Icon;
}
