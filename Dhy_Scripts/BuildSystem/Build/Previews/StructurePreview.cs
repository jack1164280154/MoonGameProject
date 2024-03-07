using UnityEngine;

namespace PolymindGames.BuildingSystem
{
    public class StructurePreview : BuildablePreview
    {
        private StructureManager m_Structure;
        private void Awake() => m_Structure = GetComponent<StructureManager>();
    }
}