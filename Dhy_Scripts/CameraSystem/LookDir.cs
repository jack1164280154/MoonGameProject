using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookDir : MonoBehaviour
{
    public float sensitivityX;
    public float sensitivityY;
    void Start()
    {
        sensitivityX = 2f;
        sensitivityY = 1.5f;
    }
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        transform.rotation *= Quaternion.AngleAxis(mouseX * sensitivityX, transform.up);
        transform.rotation *= Quaternion.AngleAxis(-mouseY * sensitivityY, transform.right);
    }
}
