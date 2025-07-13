using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChiledChecker : MonoBehaviour
{
    public GameObject buttonToShow; // Сюда закинь кнопку или меню в инспекторе
    private bool shown = false;

    void Update()
    {
        // Если все дочерние объекты исчезли
        if (transform.childCount == 0 && !shown)
        {
            buttonToShow.SetActive(true);
            shown = true;
        }
    }
}
