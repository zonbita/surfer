using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Optimize_Rotate : MonoBehaviour
{
    public int timeRepeat =  2;
    public float far = 100;
    [SerializeField]GameObject player;

    void Start()
    {
        InvokeRepeating("Find", 1, timeRepeat);
    }
    void Find()
    {
        if (player == null) { player = GameObject.FindWithTag("Player"); return; }
        if (Vector3.Distance(player.transform.position, this.transform.position) <= far)
        {
            Rotate_Object[]  r = GetComponentsInChildren<Rotate_Object>();
            foreach(Rotate_Object o in r)
            {
                o.enabled = true;
            }
        }
        else
        {
            Rotate_Object[] r = GetComponentsInChildren<Rotate_Object>();
            foreach (Rotate_Object o in r)
            {
                o.enabled = false;
            }
        }

    }
}
