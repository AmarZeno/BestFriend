using UnityEngine;
using System.Collections;

public class TriggerScript : MonoBehaviour {

    public GameObject infoboxCanvas;

    void Awake() {
        infoboxCanvas.SetActive(false);
    }

    void OnTriggerEnter(Collider other) {
        Debug.Log("Trigger Enter");
        infoboxCanvas.SetActive(true);
    }

    void OnTriggerStay(Collider other) {
        Debug.Log("Trigger stay");
    }

    void OnTriggerExit(Collider other) {
        Debug.Log("Trigger exit");
        infoboxCanvas.SetActive(false);
    }

}
