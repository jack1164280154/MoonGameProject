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
            //获取物理道具的刚体组件
            Rigidbody propRb = hit.collider.gameObject.GetComponent<Rigidbody>();
            //找到碰撞点
            Vector3 position = hit.collider.ClosestPoint(transform.position);
            //计算碰撞点和人物的方向
            Vector3 dir = (position - transform.position).normalized;
            //print("dir" + dir);
            //对这个点施加一个合适的力
            propRb.AddForceAtPosition(dir * pushForce, position);
        }
    }
}
