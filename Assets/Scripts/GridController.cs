using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridController : MonoBehaviour
{
    public GameObject cellPrefab;
    public Transform gridParent;
    private GameObject[,] cells;

    public bool[,] gridData;
    public int gridSize = 10;

    void Awake()
    {
        gridData = new bool[gridSize, gridSize];
    }

    public bool IsInsideGrid(Vector2Int pos)
    {
        return pos.x >= 0 && pos.x < gridSize && pos.y >= 0 && pos.y < gridSize;
    }

    public bool IsOccupied(Vector2Int pos)
    {
        return gridData[pos.x, pos.y];
    }

    public void MarkOccupied(Vector2Int pos)
    {
        gridData[pos.x, pos.y] = true;
    }

    public Vector2Int GetCellFromWorldPosition(Vector3 worldPos)
    {
        Vector3 local = gridParent.InverseTransformPoint(worldPos);
        float cellSize = 50f; // настрой по факту

        int x = Mathf.FloorToInt(local.x / cellSize);
        int y = Mathf.FloorToInt(-local.y / cellSize);

        return new Vector2Int(x, y);
    }

    public Vector3 GetWorldPositionFromCell(Vector2Int cell)
    {
        float cellSize = 50f;
        Vector3 local = new Vector3(cell.x * cellSize, -cell.y * cellSize, 0);
        return gridParent.TransformPoint(local);
    }

    void Start()
    {
        cells = new GameObject[gridSize, gridSize];

        for (int y = 0; y < gridSize; y++)
        {
            for (int x = 0; x < gridSize; x++)
            {
                GameObject cell = Instantiate(cellPrefab, gridParent);
                cells[x, y] = cell;
            }
        }
    }
}

