using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueScriptBargain : MonoBehaviour {


    public GameObject BlackScreen;
    Graphic BlackScreenImage;

    public GameObject IncomingText;
    public GameObject ResponseText1;
    public GameObject ResponseText2;
    public GameObject ResponseText3;
    //public GameObject ActionText;

    public Text displayText1;
    public Text displayText2;
    public Text displayText3;
    public Text displayText4;
   // public Text displayText5;

    public TextAsset sceneScript1;
    public TextAsset sceneScript2;
    public TextAsset sceneScript3;
    public TextAsset sceneScript4;
    //public TextAsset sceneScript5;

    public string[] displayLines1;
    public string[] displayLines2;
    public string[] displayLines3;
    public string[] displayLines4;
    //public string[] displayLines5;

    float currTime;
    float fadeDuration = 1;
    float waitTime;
    float waterfallTime = 0.4f;

    int selection;
    int endLine =0;
    int currentLine = 0;
    int responseLine = 0;
    int displayState = 5;
    int actionTextLine = 0;


    public bool textEnd = false;

    // Padding constants
    const int responseTextPadding = 20;

    // Use this for initialization
    void Start () {

        BlackScreenImage = BlackScreen.GetComponent<Image>();
        
        currTime = 10000;
        waitTime = fadeDuration + 0.5f;

        IncomingText.GetComponent<Graphic>().CrossFadeAlpha(0.01f, 0.01f, true);
        ResponseText1.GetComponent<Graphic>().CrossFadeAlpha(0.01f, 0.0f, true);
        ResponseText2.GetComponent<Graphic>().CrossFadeAlpha(0.01f, 0.01f, true);
        ResponseText3.GetComponent<Graphic>().CrossFadeAlpha(0.01f, 0.01f, true);
        //ActionText.GetComponent<Graphic>().CrossFadeAlpha(0.01f, 0.01f, true);

        if (sceneScript1 != null || sceneScript2 != null || sceneScript3 != null || sceneScript4 != null)
        {
            displayLines1 = (sceneScript1.text.Split('\n'));
            displayLines2 = (sceneScript2.text.Split('\n'));
            displayLines3 = (sceneScript3.text.Split('\n'));
            displayLines4 = (sceneScript4.text.Split('\n'));
            //displayLines5 = (sceneScript5.text.Split('\n'));
        }


        updateTextLayout();
    }
	
	// Update is called once per frame
	void Update () {

        displayText1.text = displayLines1[responseLine];
        displayText2.text = displayLines2[responseLine];
        displayText3.text = displayLines3[responseLine];
        displayText4.text = displayLines4[responseLine];
        //displayText5.text = displayLines5[actionTextLine];

        

        if (Time.timeSinceLevelLoad> 10 && displayState == 5)
        {
            currTime = Time.timeSinceLevelLoad;;
            displayState =1;

        }

        if (Time.timeSinceLevelLoad > currTime + fadeDuration && displayState ==0)
        {
            choice(selection);
            displayState++;

        }


        if (Time.timeSinceLevelLoad > currTime + waitTime && displayState == 1)
        {
            IncomingText.GetComponent<Graphic>().CrossFadeAlpha(1.0f, fadeDuration, true);
            displayState++;
        }

        if (Time.timeSinceLevelLoad > currTime + waitTime + waterfallTime && displayState == 2)
        {
            ResponseText1.GetComponent<Graphic>().CrossFadeAlpha(1.0f, fadeDuration, true);
            ResponseText2.GetComponent<Graphic>().CrossFadeAlpha(1.0f, fadeDuration, true);
            ResponseText3.GetComponent<Graphic>().CrossFadeAlpha(1.0f, fadeDuration, true);
        }


        //if (Time.timeSinceLevelLoad > currTime + 13.0f && displayState == 2 && textEnd == false)
        //{
        //    //textEnd = false;
        //    ActionText.GetComponent<Graphic>().CrossFadeAlpha(1.0f, fadeDuration, true);
        //}


       


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

        currTime = Time.timeSinceLevelLoad;
        displayState = 0;

        updateTextLayout();

    }

    void choice(int input){

        if (responseLine == 0)
        {
            if (input == 1) { responseLine = 1; }
            if (input == 2) { responseLine = 2; }
            if (input == 3) { responseLine = 3; }
        }
        else if (responseLine == 1)
        {
            if (input == 1) { responseLine = 2; }
            if (input == 2) { responseLine = 4; }
            if (input == 3) { responseLine = 2; }
        }
        else if (responseLine == 2)
        {
            if (input == 1) { responseLine = 5; }
            if (input == 2) { responseLine = 6; }
            if (input == 3) { responseLine = 6; }
        }
        else if (responseLine == 3)
        {
            if (input == 1) { responseLine = 2; }
            if (input == 2) { responseLine = 5; }
            if (input == 3) { responseLine = 2; }
        }
        else if (responseLine == 4)
        {
            if (input == 1) { responseLine = 7; }
            if (input == 2) { responseLine = 8; }
            if (input == 3) { responseLine = 8; }
        }
        else if (responseLine == 5)
        {
            if (input == 1) { responseLine = 9; }
            if (input == 2) { responseLine = 7; }
            if (input == 3) { responseLine = 7; }
        }
        else if (responseLine == 6)
        {
            if (input == 1) { responseLine = 9; }
            if (input == 2) { responseLine = 9; }
            if (input == 3) { responseLine = 8; }
        }
        else if (responseLine == 7)
        {
            if (input == 1) { responseLine = 6; }
            if (input == 2) { responseLine = 8; }
            if (input == 3) { responseLine = 9; }
        }
        else if (responseLine == 8)
        {
            if (input == 1) { responseLine = 9; }
            if (input == 2) { responseLine = 9; }
            if (input == 3) { responseLine = 9; }
        }
        else if (responseLine == 9)
        {
            if (input == 1) { responseLine = 10; actionTextLine = 0; }
            if (input == 2) { responseLine = 10; actionTextLine = 1; }
            if (input == 3) { responseLine = 10; actionTextLine = 0; }
            textEnd = true;
        }
    }

    void updateTextLayout() {
        ResponseText2.transform.position = new Vector2(ResponseText1.transform.position.x + ResponseText1.GetComponent<RectTransform>().rect.width + responseTextPadding, ResponseText2.transform.position.y);
        ResponseText3.transform.position = new Vector2(ResponseText2.transform.position.x + ResponseText2.GetComponent<RectTransform>().rect.width + responseTextPadding, ResponseText3.transform.position.y);
    }
}
