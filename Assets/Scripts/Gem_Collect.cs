using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Gem_Collect : MonoBehaviour
{
    public float speed;
    public Transform target;
    public GameObject GemPrefab;
    Camera cam;
    void Start()
    {
        if (!cam) cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    public void StartCoinMove(Vector3 start, Action onComplete)
    {
        onComplete.Invoke();
        // GameObject c = Instantiate(GemPrefab, transform);

        // StartCoroutine(MoveCoin(c.transform, start, target, onComplete));
    }

    IEnumerator MoveCoin(Transform obj, Vector3 start, Transform end, Action onComplete)
    {
        
        Vector3 endPos = cam.ScreenToWorldPoint(new Vector3(end.position.x, end.position.y, cam.transform.position.z * -1));
        obj.position = start;

        while((endPos - obj.position).magnitude > 0.5f)
        {
            obj.Translate((endPos - obj.position).normalized * speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();

            endPos = cam.ScreenToWorldPoint(new Vector3(end.position.x, end.position.y, cam.transform.position.z * -1));
        }

        onComplete.Invoke();
        Destroy(obj.gameObject);
    }
}
