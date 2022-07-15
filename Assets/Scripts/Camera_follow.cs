using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_follow : MonoBehaviour
{
    public float smoothTime = 0.12f, CameraPy = 10;
    public Vector3 Offset = new Vector3(14, 6, 3.5f);
    public Vector3 Offset2 = new Vector3(18, 7, 2);
    private Vector3 velocity = Vector3.zero;
    public bool CameraFinish = false;
    int travel = 0;
    private Rigidbody Character;


    private void Awake()
    {
        if (Character == null)
            Character = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
    }

    private void LateUpdate()
    {
        if(CameraFinish == true)
        {
            CameraPy = 15;
            Vector3 targetPosition = Character.transform.position + Offset2;
            targetPosition.y = CameraPy;
            if (travel == 0)
            {                
                transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, 2);
                StartCoroutine(Travel());

            }
            else if(travel == 1)
            {
                transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, 1);
            }
            else if (travel == 2)
            {
                transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, 0.5f);
            }
            else
            {
                
                transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, .25f);
            }

        }
        else
        {
            // update position
            Vector3 targetPosition = Character.transform.position + Offset;
            targetPosition.y = CameraPy;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }

    }
    IEnumerator Travel()
    {
        yield return new WaitForSecondsRealtime(1);
        travel = 1;
        yield return new WaitForSecondsRealtime(1);
        travel = 2;
        yield return new WaitForSecondsRealtime(1);
        travel = 3;
    }

}