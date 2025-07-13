using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuggageDraggable : MonoBehaviour
{
    private Vector3 offset;
    private Camera cam;
    private bool dragging = false;

    void Start()
    {
        cam = Camera.main;
    }

    void OnMouseDown()
    {
        Vector3 mouseWorld = cam.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = transform.position.z;
        offset = transform.position - mouseWorld;
        dragging = true;
    }

    void OnMouseUp()
    {
        dragging = false;
    }

    void Update()
    {
        if (dragging)
        {
            Vector3 mouseWorld = cam.ScreenToWorldPoint(Input.mousePosition);
            mouseWorld.z = transform.position.z;
            transform.position = mouseWorld + offset;
        }
    }
}
