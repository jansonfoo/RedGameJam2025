using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuggagePack_Script : MonoBehaviour
{
    public List<GameObject> itemPrefabs;
    private List<LuggageItem> items = new List<LuggageItem>();
    public Vector2 spawnAreaMin = new Vector2(3.5f, 0f);
    public Vector2 spawnAreaMax = new Vector2(7.5f, 4f);

    public class LuggageItem
    {
        public int width;
        public int height;
        public bool[,] grid;
        public Vector2Int anchorPoint;

        public LuggageItem(bool[,] shape, string anchor_Point)
        {
            grid = shape;
            width = shape.GetLength(0);
            height = shape.GetLength(1);
            int x = int.Parse(anchor_Point) / 10;
            int y = int.Parse(anchor_Point) % 10;
            anchorPoint = new Vector2Int(x, y);
        }

        public void Snap() //TODO
        {
            return;
        }

        public void Rotate()
        {
            if (width == height)
            {
                return;
            }
            else
            {
                int tmp = anchorPoint.y;
                anchorPoint.y = anchorPoint.x;
                anchorPoint.x = height - tmp - 1;
                (width, height) = (height, width);
            }
        }
    }

    public void SpawnAllItems()
    {
        var gridController = FindFirstObjectByType<GridController>();
        gridController.SetupGrid(9, 5);

        float cellW = gridController.cellW;
        float cellH = gridController.cellH;

        for (int i = 0; i < items.Count; i++)
        {
            Vector2 spawnPos = new Vector2(
                Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                Random.Range(spawnAreaMin.y, spawnAreaMax.y)
            );

            var visual = Instantiate(itemPrefabs[i], spawnPos, Quaternion.identity);
            visual.GetComponent<LuggageItem_Visual>().Render(items[i], cellW, cellH);
            visual.AddComponent<LuggageDraggable>();

            BoxCollider2D col = visual.AddComponent<BoxCollider2D>();

            Vector3 worldSize = new Vector3(cellW * items[i].width, cellH * items[i].height, 0);
            Vector3 localSize = visual.transform.InverseTransformVector(worldSize);

            col.size = new Vector2(localSize.x, localSize.y);
            col.offset = Vector2.zero;

            foreach (var renderer in visual.GetComponentsInChildren<SpriteRenderer>())
                renderer.sortingOrder = i+10;
        }

    }

    public static bool[,] createRect(int width, int height)
    {
        var shape = new bool[width, height];
        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
                shape[x, y] = true;
        return shape;
    }

    public static bool[,] createCustom(string custom_pattern)
    {
        var rows = new List<List<bool>>();
        var currentRow = new List<bool>();

        foreach (char c in custom_pattern)
        {
            if (c == '#')
            {
                rows.Add(new List<bool>(currentRow));
                currentRow.Clear();
            }
            else if (c == '0' || c == '1')
            {
                currentRow.Add(c == '1');
            }
        }

        if (currentRow.Count > 0)
            rows.Add(currentRow);

        int width = rows[0].Count;
        int height = rows.Count;
        var shape = new bool[width, height];

        for (int y = 0; y < height; y++)
            for (int x = 0; x < rows[y].Count; x++)
                shape[x, height - 1 - y] = rows[y][x];

        return shape;
    }


    // Start is called before the first frame update
    void Start()
    {
        items.Add(new LuggageItem(createCustom("01#11"), "11"));

        SpawnAllItems();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
