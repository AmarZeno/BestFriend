﻿using UnityEngine;
using System.Collections;

public class Raycast : MonoBehaviour {

    public GameObject infoboxCanvas;

    void Awake()
    {
        infoboxCanvas.SetActive(false);
    }

    void Update()
    {
        //  Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Ray ray = new Ray(transform.position, Vector3.forward);

        Transform camera = Camera.main.transform;
        Ray ray = new Ray(camera.position, camera.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            Debug.DrawRay(camera.position, camera.forward * 10, Color.green);

            Debug.Log(hit.collider.name);
            if (hit.collider.name == "TV")
            {
                infoboxCanvas.SetActive(true);
            }
            else
            {
                infoboxCanvas.SetActive(false);
            }
        }
    }
}