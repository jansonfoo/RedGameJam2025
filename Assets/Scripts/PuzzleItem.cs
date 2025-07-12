using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleItem : MonoBehaviour
{
    public Vector2Int[] occupiedCells = {
        new Vector2Int(0, 1),
        new Vector2Int(1, 1)
    };

    private Vector3 startPosition;
    private RectTransform rectTransform;
    private Canvas canvas;
    private GridController grid; // твой скрипт с полем

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        grid = FindObjectOfType<GridController>();
    }

    public void OnBeginDrag()
    {
        startPosition = rectTransform.position;
    }

    public void OnDrag(Vector3 screenPos)
    {
        rectTransform.position = screenPos;
    }

    public void OnEndDrag()
    {
        if (CanPlace())
        {
            Place();
        }
        else
        {
            rectTransform.position = startPosition;
        }
    }

    bool CanPlace()
    {
        Vector2Int cellPos = grid.GetCellFromWorldPosition(rectTransform.position);

        foreach (Vector2Int offset in occupiedCells)
        {
            Vector2Int checkPos = cellPos + offset;

            if (!grid.IsInsideGrid(checkPos) || grid.IsOccupied(checkPos))
                return false;
        }

        return true;
    }

    void Place()
    {
        Vector2Int cellPos = grid.GetCellFromWorldPosition(rectTransform.position);

        foreach (Vector2Int offset in occupiedCells)
        {
            Vector2Int placePos = cellPos + offset;
            grid.MarkOccupied(placePos);
        }

        // фиксируем позицию (можно центрировать по сетке)
        rectTransform.position = grid.GetWorldPositionFromCell(cellPos);
    }
}

