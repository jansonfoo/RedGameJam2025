using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuggagePack_Script : MonoBehaviour
{
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
                shape[x, y] = rows[y][x];

        return shape;
    }


    // Start is called before the first frame update
    void Start()
    {
        var item11 = new LuggageItem(createRect(1, 1), "00");
        var item22 = new LuggageItem(createRect(2, 2), "00");
        var item31 = new LuggageItem(createRect(3, 1), "10");
        var itemT = new LuggageItem(createCustom("010#111"), "11");

        Debug.Log("T anchor: " + itemT.anchorPoint);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
