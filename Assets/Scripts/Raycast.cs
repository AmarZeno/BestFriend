using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class Raycast : MonoBehaviour {

    public GameObject tvScreen;
    public GameObject infoboxCanvas;
    public GameObject tvTextPanel;
    public GameObject lampLight;
    public GameObject crossHair;
    public MovieTexture movTexture;

    private bool isLampLightCoroutineExecuting = false;
    
   // public float thickness = 0.5f;

    private bool isRadioCoroutineExecuting = false;
    private bool isRadioPlaying = false;
    private AudioSource radioAudio;

    Graphic tvScreenImage;

    void Awake()
    {
        tvScreenImage = tvScreen.GetComponent<Image>();
        infoboxCanvas.SetActive(false);
        radioAudio = GetComponent<AudioSource>();
    }

    void Start() {
        GetComponent<Renderer>().material.mainTexture = movTexture;
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
                    infoboxCanvas.SetActive(true);
                    if (Input.GetMouseButton(0))
                    {
                        WatchTV();
                    }
                    break;
                case "Lamp":
                    infoboxCanvas.SetActive(true);
                    if (Input.GetMouseButton(0))
                    {
                        StartCoroutine(ToggleLampLight());
                    }
                    break;
                case "Stereo":
                    infoboxCanvas.SetActive(true);
                    if (Input.GetMouseButton(0))
                    {
                        StartCoroutine(ToggleRadio());
                    }
                    break;
                default:
                    infoboxCanvas.SetActive(false);

                    // Hide all the active components if any
                    tvTextPanel.SetActive(false);
                    tvScreen.SetActive(false);

                    // Show the crosshair
                    crossHair.SetActive(true);
                    break;
            }
        }
    }

    public void  WatchTV() {
        //crossHair.SetActive(false);
        //tvScreenImage.CrossFadeAlpha(0.01f, 0.01f, true);
        //tvScreen.SetActive(true);
        //tvScreenImage.CrossFadeAlpha(1.0f, 2.0f, true);
        movTexture.Play();
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

    public IEnumerator ToggleRadio() {
        if (isRadioCoroutineExecuting == true)
        {
            yield return new WaitForSeconds(0.3f);
        }
        else {
            isRadioCoroutineExecuting = true;
            yield return new WaitForSeconds(0.3f);
            if (isRadioPlaying == false)
            {
                radioAudio.Play();
                radioAudio.PlayDelayed(44100);
                isRadioPlaying = true;
            }
            else
            {
                radioAudio.Stop();
                isRadioPlaying = false;
            }
            isRadioCoroutineExecuting = false;
        }
    }
}
