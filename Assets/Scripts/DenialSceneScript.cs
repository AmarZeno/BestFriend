using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DenialSceneScript : MonoBehaviour {

    public static bool textOver;

    public GameObject BlackScreen;
    Graphic BlackScreenImage;


    public GameObject IncomingText;
    public GameObject ResponseText1;
    public GameObject ResponseText2;
    public GameObject ResponseText3;

    public GameObject Player;
    public GameObject Camera;

    private UnitPlayer unitPlayer;
    private CameraController cameraController;

    float currTime;
    float textOverTime;

    int transState = 0;

    void Awake()
    {
        unitPlayer = Player.GetComponent<UnitPlayer>();
        cameraController = Camera.GetComponent<CameraController>();

    }

    // Use this for initialization
    void Start () {

        BlackScreenImage = BlackScreen.GetComponent<Image>();
        unitPlayer.enabled = false;
        cameraController.enabled = false;
        currTime = Time.realtimeSinceStartup;
        textOver = false;
        textOverTime = 15000;
    }

    // Update is called once per frame
    void Update()
    {

        if (Time.realtimeSinceStartup > currTime + 9 && Time.realtimeSinceStartup < currTime + 11)
        {
            Camera.transform.Rotate(Vector3.up * Time.deltaTime * -35);
        }

        if (textOver == true)
        {
            textOverTime = Time.realtimeSinceStartup;
            textOver = false;
        }


        if (Time.realtimeSinceStartup > textOverTime && transState == 0)
        {
            BlackScreenImage.CrossFadeAlpha(0.01f, 0.01f, true);
            BlackScreen.SetActive(true);
            BlackScreenImage.CrossFadeAlpha(1.0f, 2.0f, true);

            transState++;
        }

        if (Time.realtimeSinceStartup > textOverTime + 2 && transState == 1)
        {
            IncomingText.SetActive(false);
            ResponseText1.SetActive(false);
            ResponseText2.SetActive(false);
            ResponseText3.SetActive(false);

            unitPlayer.enabled = true;
            cameraController.enabled = true;
            BlackScreenImage.CrossFadeAlpha(0.01f, 2.0f, true);
            transState++;
        }

        if (Time.realtimeSinceStartup > textOverTime + 4 && transState == 2)
        {
            BlackScreen.SetActive(false);
            transState++;
        }
    }
}
