using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_Collet : MonoBehaviour
{
    public bool collet = false;
    Player_Index player;
    Vector3 vector;
        
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Index>();
    }

    public void localPos(Vector3 v)
    {
        vector = v;
        this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, vector, Time.deltaTime);
        this.transform.localPosition = vector;
    }
    void Reset()
    {
        this.transform.localPosition = vector;
        CancelInvoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == null) return;
        if (other.CompareTag("Wall"))
        {
            this.enabled = false;
            player?.RemoveCube(this.gameObject);
        }
        if (other.CompareTag("Finish"))
        {
            this.enabled = false;
            player?.RemoveCube(this.gameObject);
            GameManager.Instance.UpPlus();
            GameManager.Instance.cam.GetComponent<Camera_follow>().CameraFinish = true;
            player.GetComponent<Movement_Input>().enabled = false;
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
