using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridController : MonoBehaviour
{
    public GameObject cellPrefab;
    public RectTransform gridParent;
    public int gridSize = 10;
    public float cellSize = 50f;

    private bool[,] gridData;
    private GameObject[,] cells;

    void Awake()
    {
        gridData = new bool[gridSize, gridSize];
    }

    void Start()
    {
        cells = new GameObject[gridSize, gridSize];

        for (int y = 0; y < gridSize; y++)
        {
            for (int x = 0; x < gridSize; x++)
            {
                GameObject cell = Instantiate(cellPrefab, gridParent);
                RectTransform rt = cell.GetComponent<RectTransform>();
                rt.anchorMin = rt.anchorMax = new Vector2(0, 1);
                rt.pivot = new Vector2(0, 1);
                rt.sizeDelta = new Vector2(cellSize, cellSize);
                rt.anchoredPosition = new Vector2(x * cellSize, -y * cellSize);
                cells[x, y] = cell;
            }
        }
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
        if (IsInsideGrid(pos))
        {
            gridData[pos.x, pos.y] = true;

            // Изменим цвет ячейки
            Image img = cells[pos.x, pos.y].GetComponent<Image>();
            if (img != null)
            {
                img.color = Color.gray; // Цвет занятых ячеек
            }
        }
    }

    public void UnmarkOccupied(Vector2Int pos)
    {
        if (IsInsideGrid(pos))
        {
            gridData[pos.x, pos.y] = false;

            // Вернём цвет клетки к белому
            Image img = cells[pos.x, pos.y].GetComponent<Image>();
            if (img != null)
            {
                img.color = Color.white;
            }
        }
    }

    public Vector2Int GetCellFromAnchoredPosition(Vector2 anchoredPos)
    {
        int x = Mathf.FloorToInt(anchoredPos.x / cellSize);
        int y = Mathf.FloorToInt(-anchoredPos.y / cellSize);
        return new Vector2Int(x, y);
    }

    public Vector2 GetAnchoredPositionFromCell(Vector2Int cell)
    {
        return new Vector2(cell.x * cellSize, -cell.y * cellSize);
    }

    public void ClearHighlights()
{
    for (int y = 0; y < gridSize; y++)
    {
        for (int x = 0; x < gridSize; x++)
        {
            Image img = cells[x, y].GetComponent<Image>();
            if (img != null)
                img.color = gridData[x, y] ? Color.gray : Color.white;
        }
    }
}
}

