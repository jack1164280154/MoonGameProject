using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureBuildable : Buildable
{
    public override void OnCreated(bool playEffects = true)
	{
		Debug.Log("OnCreated");
		/*EnableColliders(false);
		EnableSockets(false);

		gameObject.SetLayerRecursively(BuildingManager.BuildablePreviewLayer);
		MaterialEffect.EnableCustomEffect(BuildingManager.PlacementAllowedMaterialEffect);*/
	}
}
