using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public GameObject cellPrefab;
    public Vector2 fieldSize;
    public int columns;
    public int rows;
    public float cellW { get; private set; }
    public float cellH { get; private set; }
    public bool[,] IsOccupiedGrid;

    public void SetupGrid(int cols, int rws)
    {
        columns = cols;
        rows = rws;

        foreach (Transform child in transform)
            Destroy(child.gameObject);
        bool[,] IsOccupiedGrid = new bool[columns, rows];
        GenerateGrid();
        cellW = transform.lossyScale.x / columns;
        cellH = transform.lossyScale.y / rows;
    }

    void GenerateGrid()
    {
        float cellWidth = 1f / columns;
        float cellHeight = 1f / rows;
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
                cell.transform.localScale = new Vector3(cellWidth * 0.95f, cellHeight * 0.95f, 1);
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
