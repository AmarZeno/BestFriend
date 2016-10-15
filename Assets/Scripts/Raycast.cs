using UnityEngine;
using System.Collections;

public class Raycast : MonoBehaviour {

    public GameObject infoboxCanvas;
    public GameObject tvTextPanel;
    public GameObject lampLight;
    public bool isLampLightCoroutineExecuting = false;
    public GameObject crossHair;

    public float thickness = 0.5f;

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

        // Using spherecast to increase the hit area
        //  if (Physics.SphereCast(ray.origin, thickness, ray.direction, out hit))

        if (Physics.Raycast(ray, out hit, 100))
        {
            // Debug.DrawRay(camera.position, camera.forward * 10, Color.green);
         //   Debug.Log(hit.collider.name);
            if (hit.collider.name == "TV")
            {
                infoboxCanvas.SetActive(true);
                if (Input.GetMouseButton(0))
                {
                    tvTextPanel.SetActive(true);
                    crossHair.SetActive(false);
                }
            }
            else if (hit.collider.name == "Lamp") {
                infoboxCanvas.SetActive(true);
                if (Input.GetMouseButton(0))
                {
                    StartCoroutine(ToggleLampLight());
                }
            } else  {
                infoboxCanvas.SetActive(false);

                // Hide all the active components if any
                tvTextPanel.SetActive(false);

                // Show the crosshair
                crossHair.SetActive(true);
            }


        }
    }

    public IEnumerator ToggleLampLight()
    {
        if (isLampLightCoroutineExecuting == true)
        {
            yield return new WaitForSeconds(0.3f);
        }
        else
        {
            isLampLightCoroutineExecuting = true;
            yield return new WaitForSeconds(0.3f);
            lampLight.SetActive(!lampLight.activeSelf);
            isLampLightCoroutineExecuting = false;
        }
    }
}
