using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIItemSpawn : MonoBehaviour
{
    public GameObject itemPrefab;                  // Префаб UI Image
    public RectTransform spawnArea;                // Контейнер (например, Canvas или Panel)
    public ItemDataWithCount[] itemsToSpawn;       // Список предметов с типом и количеством

    void Start()
    {
        SpawnItems();
    }

    void SpawnItems()
    {
        foreach (var data in itemsToSpawn)
        {
            for (int i = 0; i < data.count; i++)
            {
                GameObject item = Instantiate(itemPrefab, spawnArea); // parent = spawnArea
                RectTransform rt = item.GetComponent<RectTransform>();

                // Установка спрайта и типа
                Image img = item.GetComponent<Image>();
                img.sprite = data.sprite;

                ItemUI itemUI = item.GetComponent<ItemUI>();
                itemUI.itemName = data.itemName;
                itemUI.itemType = data.category;
                itemUI.spawnZone = spawnArea;

                // Спавн ВНУТРИ границ spawnArea
                float width = spawnArea.rect.width;
                float height = spawnArea.rect.height;

                float x = Random.Range(0, width - rt.sizeDelta.x);
                float y = Random.Range(-rt.sizeDelta.y, -height);

                rt.anchoredPosition = new Vector2(x, y);
            }
        }
    }
}


