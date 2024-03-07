using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(menuName = "SurvivalGames/Building/Buildable Definition", fileName = "(Buildable) ")]
public class BuildableDefinition : GroupMemberDefinition<BuildableDefinition, BuildableCategoryDefinition>
{
    public override string Name
    {
        get => m_BuildableName;
        protected set => m_BuildableName = value;
    }

    public override Sprite Icon => m_Icon;
    public override string Description => m_Description;
    public Buildable Prefab => m_Prefab;


    [SerializeField]
    private string m_BuildableName;

    [SerializeField]
    [Tooltip("Item Icon.")]
    private Sprite m_Icon;

    [SerializeField]
    [Tooltip("Corresponding pickup for this item, so you can actually drop it, or pick it up from the ground.")]
    private Buildable m_Prefab;

    [SerializeField, Multiline]
    [Tooltip("Item description to display in the UI.")]
    private string m_Description;
    public static BuildableDefinition[] GetAllBuildablesOfType<T>() where T : Buildable
    {
        return Definitions.Where((BuildableDefinition def) => def.Prefab.GetType() == typeof(T)).ToArray();
    }
}
