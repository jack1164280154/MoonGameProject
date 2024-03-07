using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SurvivalGamePlay/Building/Building Settings")]
public class BuildingManager : ScriptableObject
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Init() => SetInstance();
    private const string k_DefaultManagerPath = "Managers/BuildingManager";
    public static LayerMask FreePlacementMask => Instance.m_FreePlacementMask;
    [SerializeField]
    [Tooltip("Tells the controller on what layers can buildables be placed.")]
    private LayerMask m_FreePlacementMask;

    public static LayerMask OverlapCheckMask => Instance.m_overlapCheckMask;
    [SerializeField]
    private LayerMask m_overlapCheckMask;

    public static MaterialEffectInfo PlacementAllowedMaterialEffect => Instance.m_PlacementAllowedMaterial;
    public static MaterialEffectInfo PlacementDeniedMaterialEffect => Instance.m_PlacementDeniedMaterial;
    [Header("Materials")]

    [SerializeField]
    private MaterialEffectInfo m_PlacementAllowedMaterial;

    [SerializeField]
    private MaterialEffectInfo m_PlacementDeniedMaterial;
    protected static void SetInstance(string path = k_DefaultManagerPath)
    {
        if (path == null)
            path = k_DefaultManagerPath;

        var manager = Resources.Load<BuildingManager>(path);

        Instance = manager;

        if (Instance == null)
        {
            Debug.Log("NoBuildingManager");
        }
        //Instance = CreateInstance<T>();

        //Instance.OnInitialized();
    }
    protected static BuildingManager Instance { get; private set; }
    public static StructureManager StructurePrefab => Instance.m_CustomStructure;
    [SerializeField]
    //[NotNull("Structure manager prefab, handles socket based building.")]
    private StructureManager m_CustomStructure;
}
