using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DialogueScript : MonoBehaviour {


    public GameObject BlackScreen;
    Graphic BlackScreenImage;

    public GameObject Phone;
    public AudioSource ringtone;

    public GameObject IncomingText;
    public GameObject ResponseText1;
    public GameObject ResponseText2;
    public GameObject ResponseText3;

    public Text displayText1;
    public Text displayText2;
    public Text displayText3;
    public Text displayText4;

    public TextAsset sceneScript1;
    public TextAsset sceneScript2;
    public TextAsset sceneScript3;
    public TextAsset sceneScript4;

    public string[] displayLines1;
    public string[] displayLines2;
    public string[] displayLines3;
    public string[] displayLines4;

    float currTime;
    float soundTime;
    float fadeDuration = 1;
    float waitTime;
    float waterfallTime = 0.4f;

    int selection;
    int endLine =0;
    int currentLine = 0;
    int responseLine = 0;
    int displayState = 5;

    public bool textEnd = false;

    // Use this for initialization
    void Start () {

        ringtone = Phone.GetComponent<AudioSource>();
        BlackScreenImage = BlackScreen.GetComponent<Image>();

        soundTime = Time.realtimeSinceStartup+5;
        currTime = 10000;
        waitTime = fadeDuration + 0.5f;

        IncomingText.GetComponent<Graphic>().CrossFadeAlpha(0.01f, 0.01f, true);
        ResponseText1.GetComponent<Graphic>().CrossFadeAlpha(0.01f, 0.0f, true);
        ResponseText2.GetComponent<Graphic>().CrossFadeAlpha(0.01f, 0.01f, true);
        ResponseText3.GetComponent<Graphic>().CrossFadeAlpha(0.01f, 0.01f, true);

        if (sceneScript1 != null || sceneScript2 != null || sceneScript3 != null || sceneScript4 != null)
        {
            displayLines1 = (sceneScript1.text.Split('\n'));
            displayLines2 = (sceneScript2.text.Split('\n'));
            displayLines3 = (sceneScript3.text.Split('\n'));
            displayLines4 = (sceneScript4.text.Split('\n'));

        }
        if (endLine == 0)
        {
            endLine = displayLines1.Length - 1;
            soundTime = Time.realtimeSinceStartup;

        }

    }
	
	// Update is called once per frame
	void Update () {

        displayText1.text = displayLines1[responseLine];
        displayText2.text = displayLines2[responseLine];
        displayText3.text = displayLines3[responseLine];
        displayText4.text = displayLines4[responseLine];

        if(Time.realtimeSinceStartup > soundTime + 8)
        {
            ringtone.Play();
            soundTime = Time.realtimeSinceStartup;
        }



        if (Time.realtimeSinceStartup> 15 && displayState == 5)
        {
            currTime = Time.realtimeSinceStartup;
            soundTime = 15000;
            displayState =1;

        }

        if (Time.realtimeSinceStartup > currTime + fadeDuration && displayState ==0)
        {
            choice(selection);
            displayState++;

        }







        if (Time.realtimeSinceStartup > currTime + waitTime && displayState == 1)
        {
            IncomingText.GetComponent<Graphic>().CrossFadeAlpha(1.0f, fadeDuration, true);
            displayState++;
        }

        if (Time.realtimeSinceStartup > currTime + waitTime + waterfallTime && displayState == 2)
        {
            ResponseText1.GetComponent<Graphic>().CrossFadeAlpha(1.0f, fadeDuration, true);
            ResponseText2.GetComponent<Graphic>().CrossFadeAlpha(1.0f, fadeDuration, true);
            ResponseText3.GetComponent<Graphic>().CrossFadeAlpha(1.0f, fadeDuration, true);
            
        }

        if (Time.realtimeSinceStartup > currTime + 7.0f && textEnd == true)
        {
            BlackScreenImage.GetComponent<Graphic>().CrossFadeAlpha(0.01f, 0.01f, true);
            BlackScreen.SetActive(true);
            BlackScreenImage.GetComponent<Graphic>().CrossFadeAlpha(1.0f, 3.0f, true);

            IncomingText.SetActive(false);
            ResponseText1.SetActive(false);
            ResponseText2.SetActive(false);
            ResponseText3.SetActive(false);

            textEnd = false;
            DenialSceneScript.textOver = true; 

        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {

            textTransition();
            selection = 1;

        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {

            textTransition();
            selection = 2;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {

            textTransition();
            selection = 3;

        }


    }

    void textTransition()
    {

        IncomingText.GetComponent<Graphic>().CrossFadeAlpha(0.01f, fadeDuration, true);
        ResponseText1.GetComponent<Graphic>().CrossFadeAlpha(0.01f, fadeDuration, true);
        ResponseText2.GetComponent<Graphic>().CrossFadeAlpha(0.01f, fadeDuration, true);
        ResponseText3.GetComponent<Graphic>().CrossFadeAlpha(0.01f, fadeDuration, true);

        currTime = Time.realtimeSinceStartup;
        displayState = 0;

        

    }

    void choice(int input){

         if(responseLine == 0)
        {
            if(input == 1) { responseLine = 1; }
            if (input == 2) { responseLine = 2; }
            if (input == 3) { responseLine = 2; }
        }
        else if (responseLine == 1 || responseLine == 2)
        {
            if (input == 1) { responseLine = 3; }
            if (input == 2) { responseLine = 4; }
            if (input == 3) { responseLine = 4; }
        }
        else if (responseLine == 3 || responseLine == 4)
        {
            if (input == 1) { responseLine = 5; }
            if (input == 2) { responseLine = 6; }
            if (input == 3) { responseLine = 7; }
            textEnd = true;
        }
    }
}
