using DG.Tweening;
using UnityEngine;
using TigerForge;

public class Ready : MonoBehaviour
{
    public GameObject HudPrefab;
    public GameObject handPrefab;
    public float range = 100;

    void Start()
    {
        EventManager.StartListening("GamePlay", Stop);
        handPrefab.transform.DOLocalMoveX(-range, 2).SetLoops(-1,LoopType.Yoyo) ;
    }

    void Stop()
    {
        HudPrefab.SetActive(false);
        this.enabled = false;
    }
}
