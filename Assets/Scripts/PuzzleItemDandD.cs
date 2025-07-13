using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PuzzleItemDandD : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public string itemName;
    public Vector2Int[] occupiedCells;
    public RectTransform spawnZone;
    private Vector2 originalPosition;
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private GridController grid;
    private List<Vector2Int> lastOccupiedPositions = new List<Vector2Int>();

    public GameObject buttonToShow;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
        grid = FindObjectOfType<GridController>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = rectTransform.anchoredPosition;
        canvasGroup.blocksRaycasts = false;
        transform.SetAsLastSibling();

        // Очистка ранее занятых клеток
        foreach (Vector2Int pos in lastOccupiedPositions)
        {
            grid.UnmarkOccupied(pos);
        }
        lastOccupiedPositions.Clear();
        grid.ResetAllCellColors();
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;

        Vector2Int cellPos = grid.GetCellFromAnchoredPosition(rectTransform.anchoredPosition);
        Vector2Int minOffset = GetMinOffset();
        Vector2Int anchorCell = cellPos - minOffset;

        grid.HighlightPlacement(anchorCell, occupiedCells);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        grid.ResetAllCellColors();

        Vector2Int cellPos = grid.GetCellFromAnchoredPosition(rectTransform.anchoredPosition);
        Vector2Int minOffset = GetMinOffset();
        Vector2Int anchorCell = cellPos - minOffset;

        if (CanPlaceOnGrid())
        {
            PlaceOnGrid();
        }
        else if (IsInsideZone())
        {
            // Просто остаётся в зоне хранения (ничего не делаем)
        }
        else
        {
            ReturnToOriginalPosition();
        }
    }

    private void ReturnToOriginalPosition()
    {
        // Переместить визуально обратно
        rectTransform.anchoredPosition = originalPosition;
        Vector2Int cellPos = grid.GetCellFromAnchoredPosition(rectTransform.anchoredPosition);

        Vector2Int minOffset = GetMinOffset();
        Vector2Int anchorCell = cellPos - minOffset;


        foreach (Vector2Int offset in occupiedCells)
        {
            Vector2Int placePos = anchorCell + offset;
            grid.MarkOccupied(placePos);
            lastOccupiedPositions.Add(placePos); // запоминаем
        }
    }

    private bool CanPlaceOnGrid()
    {
        Vector2Int cellPos = grid.GetCellFromAnchoredPosition(rectTransform.anchoredPosition);

        foreach (Vector2Int offset in occupiedCells)
        {
            Vector2Int checkPos = cellPos + offset;

            if (!grid.IsInsideGrid(checkPos) || grid.IsOccupied(checkPos))
                return false;
        }

        return true;
    }

    private void PlaceOnGrid()
    {
        Vector2Int cellPos = grid.GetCellFromAnchoredPosition(rectTransform.anchoredPosition);

        Vector2Int minOffset = GetMinOffset();
        Vector2Int anchorCell = cellPos - minOffset;

        lastOccupiedPositions.Clear(); // на всякий случай

        foreach (Vector2Int offset in occupiedCells)
        {
            Vector2Int placePos = anchorCell + offset;
            grid.MarkOccupied(placePos);
            lastOccupiedPositions.Add(placePos); // запоминаем
        }

        rectTransform.anchoredPosition = grid.GetAnchoredPositionFromCell(anchorCell);

        if (grid.IsGridFull())
        {
            SceneManager.LoadSceneAsync(1);
        }
    }

    private Vector2Int GetMinOffset()
    {
        int minX = int.MaxValue;
        int minY = int.MaxValue;

        foreach (var offset in occupiedCells)
        {
            if (offset.x < minX) minX = offset.x;
            if (offset.y < minY) minY = offset.y;
        }

        return new Vector2Int(minX, minY);
    }
    
    private bool IsInsideZone()
    {
        Vector3[] worldCorners = new Vector3[4];
        spawnZone.GetWorldCorners(worldCorners);

        Vector3 itemPos = rectTransform.position;
        return itemPos.x >= worldCorners[0].x && itemPos.x <= worldCorners[2].x &&
               itemPos.y >= worldCorners[0].y && itemPos.y <= worldCorners[2].y;
    }
}


