using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Buildable : Placeable
{
    public Bounds Bounds { get => m_colliders[0].bounds; }
    public BuildableDefinition Definition { get => m_Definition; set => m_Definition = value; }
    /*[SerializeField]
    private DataIdReference<BuildableDefinition> m_Definition;*/
    private BuildableDefinition m_Definition;
    public MaterialEffect MaterialEffect => GetComponent<MaterialEffect>();
    public abstract void OnCreated(bool playEffects = true);
}
