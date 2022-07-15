using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Index : MonoBehaviour
{
    public Transform PosCheck;
    public Transform StackParent;
    public Transform StackChilds;
    public Transform TextPos;
    [SerializeField] AudioClip CoinSound;
    [SerializeField] AudioClip BlockSound;
    [SerializeField] AudioClip DropSound;
    public GameObject TextPlus;
    public GameObject Particle;
    [SerializeField] Material ColorCube, ColorGem;
    public List<GameObject> ColletCube = new List<GameObject>();
    Animator ani;
    Movement_Input move;
    bool isOverlapWall = false;
    public LayerMask m_LayerMask;
    public enum aniState { Jump, Die, Victory }



    private void Awake()
    {
        ani = GetComponentInChildren<Animator>();
        move = GetComponent<Movement_Input>();

    }
    private void FixedUpdate()
    {
        if (move.enabled == false)
        {
            if (move.moveSpeed != 0) ani.SetInteger("State", 2);
        }
       
    }
    public void SetState(aniState state)
    {
        ani.SetTrigger(state.ToString());
    }

    public void RemoveCube(GameObject g)
    {
        g.transform.parent = null;
        ColletCube.Remove(g);
        CheckWall();
        GameManager.Instance.Play_Sound_At(DropSound, this.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other) return;
        // Add cube
        if (other.CompareTag("Cube"))
        {

            var v = other.gameObject.GetComponent<Block_Collet>();
            if (v && v.collet == false)
            {

                Vector3 Pos = StackParent.localPosition;
                Pos.y += 1;
                Pos.x = 0;
                Pos.z = 0;
                StackParent.localPosition = Pos;

                ColletCube.Add(other.gameObject);
                // Put cube childs
                Pos.y *= -1;
                other.gameObject.transform.SetParent(StackChilds);
                other.gameObject.transform.localPosition = Pos;
                other.gameObject.transform.localRotation = Quaternion.identity;

                // Animation
                ani.SetTrigger("Jump");
                // Play sound
                GameManager.Instance.Play_Sound_At(BlockSound, this.transform.position);

                GameObject text = Instantiate(TextPlus, this.transform);
                if (text) text.transform.SetParent(TextPos);
                text.transform.localPosition = TextPos.localPosition;
                Destroy(text, .5f);

                // Effect
                var p = Particle.GetComponent<ParticleSystem>();
                p.GetComponent<ParticleSystemRenderer>().material = ColorCube;
                p.Play();
            }
            v.collet = true;
        }

        // Add Gem
        if (other.CompareTag("Gem"))
        {
            GameManager.Instance.Play_Sound_At(CoinSound, this.transform.position);
            GameManager.Instance.gem.StartCoinMove(this.transform.position, () =>
            {
                GameManager.Instance.Add_Coin(1);

            });
            Destroy(other.gameObject);
            var p = Particle.GetComponent<ParticleSystem>();
            p.GetComponent<ParticleSystemRenderer>().material = ColorGem;
            p.Play();
        }

    }

    public void CheckWall()
    {
        if (isOverlapWall == false)
        {
            isOverlapWall = true;
            InvokeRepeating("Check", 0, 0.1F);
        }
    }

    void Check()
    {
        Collider[] hit = Physics.OverlapBox(transform.position, transform.localScale + new Vector3(1, 200f, 1), Quaternion.identity, m_LayerMask);
        if (hit.Length == 0)
            Invoke("UnCheckWall", 0);
    }

    void UnCheckWall()
    {

        // Animation
        // ani.SetTrigger("Jump");

        int k = 1, c = ColletCube.Count;
        StackParent.localPosition = new Vector3(StackParent.localPosition.x, c, StackParent.localPosition.z);


        for (int i = c - 1; i >= 0; i--)
        {
            Vector3 Pos = ColletCube[i].transform.localPosition;
            Pos.y = -k;
            Pos.x = 0;
            Pos.z = 0;
            ColletCube[i].GetComponent<Block_Collet>().localPos(Pos);
            k++;
        }
        isOverlapWall = false;
        CancelInvoke();
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawWireCube(PosCheck.transform.position, transform.localScale + new Vector3(1, 200f, 0));
    }
}
