using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterGameplay;
public class Tree : MonoBehaviour
{
    Animator animator;

    GameObject Log;
    //Transform[] points;
    Vector3[] points;
    float Hp;
    private void Awake()
    {
        
    }
    void Start()
    {
        points = new Vector3[2];
        points[0] = new Vector3(0,0,0);
        points[1] = new Vector3(0, 4f, 0);
        //points[2] = new Vector3(0, 5.7f, 0);
        animator = GetComponent<Animator>();
        Log = Resources.Load<GameObject>("Dhy_Prefab/Props/Log");
        Hp = 3;
    }
    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    if (animator != null)
        //    {
        //        animator.SetTrigger("Shake");
        //    }
        //}
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Logging") && collision.transform.root.GetComponent<CharacterInput>().CurrentState == CharacterInput.CharacterState.Logging)
        {
            print(collision.collider.name + " CollisionEnter");
            if (--Hp <= 0)
            {
                for (int i = 0; i < points.Length; i++)
                {
                    GameObject log = Instantiate(Log);
                    log.transform.SetParent(this.transform);
                    log.transform.localPosition = points[i];
                    log.transform.localRotation = Quaternion.Euler(new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), 90f));
                    log.transform.SetParent(this.transform.parent);
                }
                Destroy(this.gameObject);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //print(other.name + " TriggerEnter");
        if (other.CompareTag("Logging") && other.transform.root.GetComponent<CharacterInput>().CurrentState == CharacterInput.CharacterState.Logging)
        {
            //print("被砍了一下，剩下:" + Hp);
            if (animator != null)
            {
                animator.SetTrigger("Shake");
            }
            if (--Hp <= 0)
            {
                for(int i = 0; i<points.Length; i++)
                {
                    GameObject log = Instantiate(Log);
                    log.transform.SetParent(this.transform);
                    log.transform.localPosition = points[i];
                    log.transform.localRotation = Quaternion.Euler(new Vector3(Random.Range(-10f,10f), Random.Range(-10f, 10f), 90f));
                    log.transform.SetParent(this.transform.parent);
                }
                Destroy(this.gameObject);
            }
        }
    }
}
