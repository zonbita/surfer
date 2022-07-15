using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_RotateAroundPlayer : MonoBehaviour
{
    public Transform obj;
    public float speed;
    public float radius = 3;
    void Update()
    {
        //transform.position = obj.position - (transform.forward * radius);
        transform.RotateAround(obj.position, new Vector3(0,1,0), speed * Time.deltaTime);
    }

}
