using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuggageItem_Visual : MonoBehaviour
{
    public GameObject cellVisualPrefab;

    public void Render(LuggagePack_Script.LuggageItem item, float cellW, float cellH)
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        if (sprite != null)
        {
            Vector2 spriteSize = sprite.bounds.size;
            float scaleX = (item.width * cellW) / spriteSize.x;
            float scaleY = (item.height * cellH) / spriteSize.y;
            transform.localScale = new Vector3(scaleX, scaleY, 1f);
        }


        for (int x = 0; x < item.width; x++)
        {
            for (int y = 0; y < item.height; y++)
            {
                if (!item.grid[x, y]) continue;

                GameObject cell = Instantiate(cellVisualPrefab);
                Vector2 offset = new Vector2(
                    (x - item.anchorPoint.x + 0.5f) * cellW,
                    (y - item.anchorPoint.y + 0.5f) * cellH
                );
                cell.transform.position = transform.position + (Vector3)offset;
                cell.transform.localScale = Vector3.one;
                cell.transform.SetParent(transform);
            }
        }
    }
}
