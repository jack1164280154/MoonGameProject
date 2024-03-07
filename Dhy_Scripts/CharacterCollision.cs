using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCollision : MonoBehaviour
{
    public float pushForce = 5f;
    private float y;
    private void Start()
    {
        y = transform.position.y;
    }
    private void Update()
    {
        
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("RigidProp"))
        {
            //print(hit.transform.name);
            //��ȡ������ߵĸ������
            Rigidbody propRb = hit.collider.gameObject.GetComponent<Rigidbody>();
            //�ҵ���ײ��
            Vector3 position = hit.collider.ClosestPoint(transform.position);
            //������ײ�������ķ���
            Vector3 dir = (position - transform.position).normalized;
            //print("dir" + dir);
            //�������ʩ��һ�����ʵ���
            propRb.AddForceAtPosition(dir * pushForce, position);
        }
    }
}
