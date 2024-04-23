using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CrossHair : MonoBehaviour
{
    private Camera _camera;
    void Start()
    {
        _camera = GetComponent<Camera>();
    }
    void OnGUI()
    {
        int size = 12;
        float posX = _camera.pixelWidth / 2 - size / 4;
        float posY = _camera.pixelHeight / 2 - size / 2;
        GUI.Label(new Rect(posX, posY, size, size), "*");
    }
    // Update is called once per frame
    void Update()
    {
    }
}
