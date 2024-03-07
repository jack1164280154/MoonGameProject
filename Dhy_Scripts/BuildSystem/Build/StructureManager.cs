using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureManager : MonoBehaviour
{
    public List<StructureBuildable> Buildables => m_Buildables;
    private readonly List<StructureBuildable> m_Buildables = new();

    public void AddPart(StructureBuildable buildable)
    {
        if (!m_Buildables.Contains(buildable))
        {
            m_Buildables.Add(buildable);

            //buildable.ParentStructure = this;
            buildable.transform.SetParent(transform);

            //OccupySurroundingSockets(buildable);
        }
    }
}
