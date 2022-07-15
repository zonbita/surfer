using UnityEngine;

public class Grid : MonoBehaviour
{
    public GameObject Prefab;

    public int gridWidth = 11;
    public int gridHeight = 11;

    public float hexWidth = 1.732f;
    public float hexHeight = 2.0f;
    public float gap = 0.0f;

    Vector3 startPos;

    void Start()
    {
        AddGap();
        CalcStartPos();
        CreateGrid();
    }

    void AddGap()
    {
        hexWidth += hexWidth * gap;
        hexHeight += hexHeight * gap;
    }

    void CalcStartPos()
    {
        float offset = 0;
        if (gridHeight / 2 % 2 != 0)
            offset = hexWidth / 2;

        float x = -hexWidth * (gridWidth / 2) - offset;
        float z = hexHeight * 0.75f * (gridHeight / 2);

        startPos = new Vector3(x, 0, z);
    }

    Vector3 CalcWorldPos(Vector2 gridPos)
    {
        float offset = 0;
        if (gridPos.y % 2 != 0)
            offset = hexWidth / 2;

        float x = startPos.x + gridPos.x * hexWidth + offset;
        float z = startPos.z - gridPos.y * hexHeight * 0.75f;

        return new Vector3(x, 0, z);
    }

    void CreateGrid()
    {
        for (int y = 0; y < gridHeight; y++)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                GameObject hex = Instantiate(Prefab);
                Vector2 gridPos = new Vector2(x + (this.transform.position.x/2), y + (this.transform.position.y/2));
                hex.transform.position = CalcWorldPos(gridPos);
                hex.transform.parent = this.transform;
                hex.name = "Hex_" + x + "_" + y;
            }
        }
    }
}