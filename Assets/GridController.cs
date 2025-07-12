using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public GameObject cellPrefab;
    public Vector2 fieldSize = new Vector2(9, 5);
    public int columns = 9;
    public int rows = 5;

    void GenerateGrid()
    {
        float cellWidth = 1f / columns;
        Debug.Log(cellWidth + "vWidth");
        float cellHeight = 1f / rows;
        Debug.Log(cellHeight + "vHeight");
        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                Vector2 tmp_pos = new Vector2(
                    x * cellWidth + cellWidth / 2 - 0.5f,
                    y * cellHeight + cellHeight / 2 - 0.5f
                );

                GameObject cell = Instantiate(cellPrefab, transform);
                cell.transform.localPosition = new Vector3(tmp_pos.x, tmp_pos.y, 0);

                float scaleX = cellWidth*0.95f;
                float scaleY = cellHeight*0.95f;
                cell.transform.localScale = new Vector3(scaleX, scaleY, 1);
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        GenerateGrid();
    }

    void Awake()
    {
        columns = 9;
        rows = 5;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
