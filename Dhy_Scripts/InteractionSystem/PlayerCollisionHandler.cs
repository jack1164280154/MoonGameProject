using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionHandler : CharacterBehaviour, IPlayerCollisionHandler
{
    private ISelectionManager m_selectionManager;
    private ICharacterAnimationController m_characterAnimationController;
    protected override void OnBehaviourEnabled()
    {
        GetModule(out m_selectionManager);
        GetModule(out m_characterAnimationController);
    }
    /*private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("Equipment"))
        {
            //Debug.Log(hit.transform.name);
            //显示ui信息
            m_selectionManager.ShowIntractInfo(hit.gameObject.GetComponent<InteractableObject>());
        }
    }*/
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Equipment"))
        {
            //显示ui信息
            m_selectionManager.ShowInteractInfo(other.gameObject.GetComponent<InteractableObject>());
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Equipment"))
        {
            Debug.Log(other.name);
            if (Input.GetKey(KeyCode.E))
            {
                m_characterAnimationController.BeginEventing(EventDefinitionType.PickUp);
                Debug.Log("按下E");
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Equipment"))
        {
            m_selectionManager.CloseInteractInfo();
        }
    }
}
