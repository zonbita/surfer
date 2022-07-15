using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
[ExecuteAlways]
public class Grid2 : MonoBehaviour
{
    [SerializeField] private int width = 5, height = 10;
    [SerializeField] float range = 2;
    [SerializeField] int x2 = 1;
    [SerializeField] GameObject Prefab;
    void Start()
    {
        GenerateGrid();
    }
    void GenerateGrid()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var spawn = Instantiate(Prefab);
                spawn.name = $"Tile {x} {y}";
                spawn.transform.SetParent(this.transform);
                
                if((x + y) % 2 == 1) spawn.transform.localPosition = new Vector3((x + x2) * range, 0, y * range);
                else spawn.transform.localPosition = new Vector3((x) * range, 0, y * range);
                //spawn.transform = is

            }
        }
    }
}
