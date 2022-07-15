using UnityEngine;

public class Rotate_Object : MonoBehaviour
{
    [SerializeField] Vector3 vector = new Vector3(40,0,0);
    void FixedUpdate() => transform.Rotate(vector * Time.deltaTime);
}
