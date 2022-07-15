using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Block : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other == null) return;
        if (other.CompareTag("Wall"))
        {
           this.enabled = false;
           GameManager.Instance.GameOver();
        }
        if (other.CompareTag("Finish"))
        {
            this.enabled = false;
            GameManager.Instance.UpPlus();
            GameManager.Instance.cam.GetComponent<Camera_follow>().CameraFinish = true;
            GameManager.Instance.GameVictory();
        }
        if (other.CompareTag("Finish2"))
        {
            this.enabled = false;
            GameManager.Instance.UpPlus();
            GameManager.Instance.cam.GetComponent<Camera_follow>().CameraFinish = true;
            GameManager.Instance.GameVictory();
        }
    }
 
}
