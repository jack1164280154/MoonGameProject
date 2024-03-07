using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SurvivalGames/Misc/Material Effect Info", fileName = "(MaterialEffect) ")]
public class MaterialEffectInfo : ScriptableObject
{
    public enum EffectType
    {
        StackWithBaseMaterials,
        ReplaceBaseMaterials,
    }

    public Material Material => m_Material;
    public EffectType EffectMode => m_EffectMode;

    [SerializeField]
    private EffectType m_EffectMode;

    [SerializeField]
    private Material m_Material;
}