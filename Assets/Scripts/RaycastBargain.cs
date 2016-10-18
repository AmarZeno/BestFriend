using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class RaycastBargain : MonoBehaviour {

    public GameObject infoboxCanvas;
    public GameObject crossHair;


    void Awake()
    {

        infoboxCanvas.SetActive(false);
    }

    void Update()
    {

        Transform camera = Camera.main.transform;
        Ray ray = new Ray(camera.position, camera.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            // Debug.DrawRay(camera.position, camera.forward * 10, Color.green);
            Debug.Log(hit.collider.name);
            switch (hit.collider.name) {
                case "TV":
                    
                    if (Input.GetMouseButton(0))
                    {


                    }
                    break;
                case "Lamp":
                    infoboxCanvas.SetActive(true);
                    if (Input.GetMouseButton(0))
                    {
                     

                    }
                    break;
                case "Stereo":
                    infoboxCanvas.SetActive(true);
                    if (Input.GetMouseButton(0))
                    {
                    

                    }
                    break;
                default:
                    infoboxCanvas.SetActive(false);

                    // Hide all the active components if any
                    
                    // Show the crosshair
                    crossHair.SetActive(true);
                    break;
            }
        }
    }
}
