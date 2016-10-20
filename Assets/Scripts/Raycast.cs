using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class Raycast : MonoBehaviour {

    public GameObject tvScreen;
    public GameObject infoboxCanvas;
    public GameObject mainCanvas;
    public GameObject tvTextPanel;
    public GameObject lampLight;
    public GameObject crossHair;
    public GameObject denialSceneManager;
    public GameObject laptopScrollingPanel;
    public GameObject mobileScrollingPanel;
    public GameObject laptopContainer;
    public GameObject mobileContainer;
    public GameObject letterContainer;


    private VideoPlayback videoPlayBackScript;
    private DialogueScript dialogueScript;
    private ScrollRect laptopScrollRectScript;
    private ScrollRect mobileScrollRectScript;


    private bool isLampLightCoroutineExecuting = false;
    
   // public float thickness = 0.5f;

    private bool isRadioCoroutineExecuting = false;
    private bool isRadioPlaying = false;
    private bool isViewingLaptop = false;
    private bool isViewingMobile = false;
    private bool isViewingLetter = false;
    private AudioSource radioAudio;

    Graphic tvScreenImage;

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Awake()
    {
        tvScreenImage = tvScreen.GetComponent<Image>();
        infoboxCanvas.SetActive(false);
        radioAudio = GetComponent<AudioSource>();
        videoPlayBackScript = mainCanvas.GetComponent<VideoPlayback>();
        dialogueScript = denialSceneManager.GetComponent<DialogueScript>();
        laptopScrollRectScript = laptopScrollingPanel.GetComponent<ScrollRect>();
        mobileScrollRectScript = mobileScrollingPanel.GetComponent<ScrollRect>();
    }

    void FixedUpdate() {

        // Show and close laptop if required
        ShowAndCloseLaptop();
        // Show and close mobile if requried
        ShowAndCloseMobile();
        // Show and close letter
        ShowAndCloseLetter();
    }

    void Update()
    {

        GetMouseScroll();

        // Cases to avoid ray casting
        if (isViewingLaptop == true || isViewingMobile == true || isViewingLetter == true) {
            return;
        }

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
                case "cellPhone":
                    {
                        infoboxCanvas.SetActive(true);
                        if (Input.GetMouseButton(0)) {
                            CheckMobile();
                        }
                    }
                    break;
                case "Laptop":
                    {
                        infoboxCanvas.SetActive(true);
                        if (Input.GetMouseButton(0))
                        {
                            CheckLaptop();
                        }
                    }
                    break;
                case "Card":
                    infoboxCanvas.SetActive(true);
                    if (Input.GetMouseButton(0)) {
                        CheckLetter();
                    }
                    
                    break;
                default:
                    infoboxCanvas.SetActive(false);

                    // Hide all the active components if any
                    tvTextPanel.SetActive(false);
                    tvScreen.SetActive(false);
                    StopWatchingTV();

                    // Show the crosshair
                    crossHair.SetActive(true);
                    break;
            }
        }    
    }

    public void  WatchTV() {
        crossHair.SetActive(false);

        if (dialogueScript.canSurpassTV == true)
        {
            // Hide action text if shown
            dialogueScript.ActionText.GetComponent<Graphic>().CrossFadeAlpha(0.01f, 0.01f, true);
            // Show the black screen
            tvScreenImage.CrossFadeAlpha(0.00f, 0.01f, true);
            tvScreen.SetActive(true);
            tvScreenImage.CrossFadeAlpha(1.0f, 2.0f, true);
            // Move to next scene
            StartCoroutine(MoveToNextScene());
        }
        else {
            videoPlayBackScript.movieRawImage.enabled = true;
            videoPlayBackScript.movieTexture.Play();
            videoPlayBackScript.movieAudio.Play();
        }
    }

    public void StopWatchingTV() {
        videoPlayBackScript.movieRawImage.enabled = false;
        videoPlayBackScript.movieTexture.Stop();
        videoPlayBackScript.movieAudio.Stop();
    }

    public IEnumerator MoveToNextScene()
    {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene(2);
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

    public void CheckLaptop() {
        isViewingLaptop = true;
    }

    public void ShowAndCloseLaptop() {
        if (isViewingLaptop)
        {
            crossHair.SetActive(false);
            laptopContainer.transform.localPosition = Vector2.Lerp(laptopContainer.transform.localPosition, new Vector2(0, 0), 1.0f * Time.fixedDeltaTime);
            if (Input.GetMouseButton(0))
            {
                isViewingLaptop = false;
            }
        }
        else
        {
            laptopContainer.transform.localPosition = Vector2.Lerp(laptopContainer.transform.localPosition, new Vector2(0, -500), 1.0f * Time.fixedDeltaTime);
            crossHair.SetActive(true);
        }
    }

    public void CheckMobile()
    {
        isViewingMobile = true;
    }

    public void ShowAndCloseMobile() {
        if (isViewingMobile)
        {
            crossHair.SetActive(false);
            mobileContainer.transform.localPosition = Vector2.Lerp(mobileContainer.transform.localPosition, new Vector2(0, 240), 1.0f * Time.fixedDeltaTime);
            if (Input.GetMouseButton(0))
            {
                isViewingMobile = false;
            }
        }
        else {
            mobileContainer.transform.localPosition = Vector2.Lerp(mobileContainer.transform.localPosition, new Vector2(0, -300), 1.0f * Time.fixedDeltaTime);
            crossHair.SetActive(true);
        }
    }

    public void CheckLetter() {
        isViewingLetter = true;
    }

    public void ShowAndCloseLetter() {
        if (isViewingLetter == true)
        {
            crossHair.SetActive(false);
            letterContainer.SetActive(true);
            if (Input.GetMouseButton(0)) {
                isViewingLetter = false;
            }
        }
        else {
            crossHair.SetActive(true);
            letterContainer.SetActive(false);
        }
    }


    public void GetMouseScroll() {
        float scrollWheelValue = Input.GetAxis("Mouse ScrollWheel");
        if (isViewingLaptop == true)
        {
            laptopScrollRectScript.verticalScrollbar.value += scrollWheelValue;
        }
        else if (isViewingMobile == true) {
           // mobileScrollRectScript.verticalScrollbar.value += scrollWheelValue;
        }
    }
}
