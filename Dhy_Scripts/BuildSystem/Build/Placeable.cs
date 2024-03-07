using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Placeable : MonoBehaviour
{
    protected Collider[] m_colliders;
    private void Awake()
    {
        m_colliders = GetComponentsInChildren<Collider>();
        Debug.Log(m_colliders.Length);
    }

    public bool HasCollider(Collider col)
    {
        for (int i = 0; i < m_colliders.Length; i++)
        {
            if (m_colliders[i] == col)
            {
                return true;
            }
        }
        return false;
    }
    public void EnableColliders()
    {
        for (int i = 0; i < m_colliders.Length; i++)
        {
            m_colliders[i].enabled = true;
        }
    }
    public void DisableColliders()
    {
        for (int i = 0; i < m_colliders.Length; i++)
        {
            m_colliders[i].enabled = false;
        }
    }
}
