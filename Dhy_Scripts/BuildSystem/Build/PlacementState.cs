using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class PlacementState
{
    public abstract Buildable Buildable { get; }
    public abstract bool ContinueBuildingOnPlaced { get; }
    protected ICharacter Character { get; private set; }
    [SerializeField]
    [Tooltip("Should the placeables follow the rotation of the character?")]
    protected bool m_FollowCharacterRotation;
    [SerializeField, Range(0f, 70f)]
    [Tooltip("Max angle for detecting nearby sockets.")]
    protected float m_ViewAngleThreshold = 35f;
    [SerializeField, Range(0f, 360)]
    [Tooltip("How fast can the placeables be rotated.")]
    protected float m_RotationSpeed = 45f;
    [SerializeField, Range(0f, 10f)]
    [Tooltip("Max building range.")]
    protected float m_BuildRange = 4f;

    private readonly Collider[] m_Results = new Collider[10];
    public virtual void Initialize(ICharacter character) => Character = character;

    /// <returns> True if enabled. </returns>
    public abstract bool TrySetBuildable(Buildable buildable);

    /// <returns> True if placement allowed. </returns>
    public abstract void UpdatePlacement(float rotationOffset);

    /// <summary>
    /// Place active buildable.
    /// </summary>
    public abstract bool TryPlaceActiveBuildable();

    /// <summary>
    /// Selects the next/previous buildable.
    /// </summary>
    public virtual BuildableDefinition SelectNextBuildable(bool next) => Buildable != null ? Buildable.Definition : null;

    protected void DoFreePlacement(Buildable buildable, Quaternion rotation)
    {
        Vector3 targetPosition = buildable.transform.position;
        Vector3 targetNormal = buildable.transform.up;
        //Quaternion targetRotation = !m_FollowCharacterRotation ? rotation : Character.transform.rotation * rotation;
        Quaternion targetRotation = rotation * Character.transform.rotation;
        LayerMask freePlacementMask = BuildingManager.FreePlacementMask;
        Ray ray = Camera.main.ViewportPointToRay(Vector3.one * 0.5f);
        //Ray ray = new Ray(Character.ViewTransform.position, Character.ViewTransform.forward);
        Debug.DrawRay(ray.origin, ray.direction, Color.yellow,1f);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, m_BuildRange, freePlacementMask, QueryTriggerInteraction.Ignore))
        {
            targetPosition = hitInfo.point;
            targetNormal = hitInfo.normal;
            
            //surface = hitInfo.collider;
        }
        else
        {
            Vector3 currentPos = Character.transform.position + Character.transform.forward * m_BuildRange;
            Vector3 startPos = buildable.transform.position + new Vector3(0, 0.25f, 0);

            if (Physics.Raycast(startPos, Vector3.down, out RaycastHit hit, 1f, freePlacementMask, QueryTriggerInteraction.Ignore))
            {
                currentPos.y = hit.point.y;
                //surface = hit.collider;
            }
            //surface = null;

            targetPosition = currentPos;
        }
        if (buildable.Definition.Name == "Floor")
        {
            buildable.transform.position = targetPosition + new Vector3(0, 0.5f, 0);
        }
        else
        {
            buildable.transform.position = targetPosition;
        }
        buildable.transform.up = targetNormal;
        buildable.transform.rotation *= targetRotation;

    }

    protected bool CheckForCollisions(Buildable buildable)
    {
        //bool canPlace = buildable.PlaceOnPlaceables || m_Surface == null || m_Surface.GetComponent<Buildable>() == null;

        //if (!canPlace) return false;
        var bounds = buildable.Bounds;
        
        int size = Physics.OverlapBoxNonAlloc(bounds.center, bounds.extents, m_Results, buildable.transform.rotation, BuildingManager.OverlapCheckMask, QueryTriggerInteraction.Ignore);


        for (int i = 0; i < size; i++)
        {
            var collider = m_Results[i];
            if (!buildable.HasCollider(collider))
                return true;
        }

        return false;
    }
}
