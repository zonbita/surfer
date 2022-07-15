using System;
using UnityEngine;
using TigerForge;
public class Movement_Input : MonoBehaviour
{
    public Vector3 vector;
    [HideInInspector] public float moveSpeed = 1f;
    float z,x;
    Touch touch;
    bool start=true;

    private void Start()
    {
        touch.phase = TouchPhase.Canceled;
    }

    void Update()
    {

        if (start == false)
        {
            x = Mathf.Clamp(z + transform.position.z, -2, 2);
            Vector3 v = new Vector3(vector.x + transform.position.x, vector.y + transform.position.y, 0);
            this.transform.position = Vector3.Lerp(this.transform.position, v, Time.deltaTime);
            this.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, x);
        }


        if (Input.touchCount > 0)
            {
                touch = Input.GetTouch(0);
                if (GameManager.Instance.mode == GameManager.modes.Ready && touch.phase == TouchPhase.Began)
                {
                    EventManager.EmitEvent("GamePlay");
                    start = false;
                }
                else if (GameManager.Instance.mode == GameManager.modes.Play)
                {
                    
                    if (touch.phase == TouchPhase.Moved)
                    {
                        z = touch.deltaPosition.x * 0.5f * Time.deltaTime;

                    }


                }


            }


        

    }

}
