using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler
{
    public CategoryType acceptedType;

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        if (dropped == null) return;

        ItemUI item = dropped.GetComponent<ItemUI>();
        if (item == null) return;

        if (item.itemType == acceptedType)
        {
            // ✔ Правильная сортировка — удаляем объект
            Debug.Log($"✔ Успешно: {item.itemName} отсортирован в {acceptedType}");
            Destroy(dropped); // или можно сделать fade-out перед удалением
        }
        else
        {
            // ❌ Неверный тип — вернём обратно
            Debug.LogWarning($"❌ Ошибка: {item.itemName} нельзя положить в {acceptedType}");
            item.ReturnToOriginalPosition();
        }
    }
}

