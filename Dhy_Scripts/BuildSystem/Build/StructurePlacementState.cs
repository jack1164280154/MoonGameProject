using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SurvivalGames.Util;

[System.Serializable]
public class StructurePlacementState : PlacementState
{
    public override Buildable Buildable => m_Buildable;

    public override bool ContinueBuildingOnPlaced => true;

    private StructureBuildable m_Buildable;
    private bool m_PlacementAllowed;
    private Quaternion m_BuildablePrefabRotation;
    private Collider m_Surface;
    public override bool TrySetBuildable(Buildable buildable)
    {
        if (buildable == null)
        {
            m_Buildable = null;
            return false;
        }
        if (m_Buildable != null)
        {
            m_Buildable.MaterialEffect.EnableBaseEffect();

            m_Buildable.GetComponent<Collider>().enabled = true;
        }
        else
        {
            DebugUtil.Log("SetBuildable is null");
        }
        m_Buildable = buildable as StructureBuildable;
        m_BuildablePrefabRotation = buildable.Definition.Prefab.transform.rotation;
        if (m_Buildable != null)
        {
            m_Buildable.MaterialEffect.EnableCustomEffect(BuildingManager.PlacementAllowedMaterialEffect);
            return true;
        }
        return false;
    }
    public override bool TryPlaceActiveBuildable()
    {
        if (CheckForCollisions(Buildable))
        {
            return false;
        }

        return true;
    }
    public override void UpdatePlacement(float rotationOffset)
    {
        if (!CheckForCollisions(Buildable))
        {
            m_Buildable.MaterialEffect.EnableCustomEffect(BuildingManager.PlacementAllowedMaterialEffect);
        }
        else
        {
            m_Buildable.MaterialEffect.EnableCustomEffect(BuildingManager.PlacementDeniedMaterialEffect);
        }
        DoFreePlacement(m_Buildable, m_BuildablePrefabRotation  * Quaternion.Euler(0, rotationOffset * m_RotationSpeed, 0));
    }
}
