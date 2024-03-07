using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PolymindGames.BuildingSystem
{
    public abstract class BuildablePreview : MonoBehaviour
    {
        public static List<BuildablePreview> AllPreviewsInScene = new();
        public Vector3 PreviewCenter { get; private set; }

        public event UnityAction<bool> MaterialAdded;
        #region Save & Load
        public virtual void LoadMembers(object[] members) { }
        public virtual object[] SaveMembers() => System.Array.Empty<object>();
        #endregion
    }
}