using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueScript : MonoBehaviour {


    public GameObject BlackScreen;
    Graphic BlackScreenImage;

    public GameObject Phone;
    public AudioSource ringtone;

    public GameObject IncomingText;
    public GameObject ResponseText1;
    public GameObject ResponseText2;
    public GameObject ResponseText3;
    public GameObject ActionText;
    public GameObject SunBlocker;
    public GameObject ConversationContainer;

    public Text displayText1;
    public Text displayText2;
    public Text displayText3;
    public Text displayText4;
    public Text displayText5;

    public TextAsset sceneScript1;
    public TextAsset sceneScript2;
    public TextAsset sceneScript3;
    public TextAsset sceneScript4;
    public TextAsset sceneScript5;

    public Canvas DigitalItemCanvas;
    public Image ActionImage;
    public Image ResponseImage1;
    public Image ResponseImage2;
    public Image ResponseImage3;

    public Sprite[] ConversationSprites; 

    public string[] displayLines1;
    public string[] displayLines2;
    public string[] displayLines3;
    public string[] displayLines4;
    public string[] displayLines5;

    float currTime;
    float soundTime;
    float fadeDuration = 1;
    float waitTime;
    float waterfallTime = 0.4f;

    int selection;
    int endLine =0;
    int currentLine = 0;
    int responseLine = 0;
    int conversationSet = 0;
    int displayState = 5;
    int actionTextLine = 0;
    const int responseSpriteOffset = 8;

    public bool textEnd = false;
    private bool canShowActionText = false;
    public bool canSurpassTV = false;
    private bool isShowingConversation = false;

    // Padding constants
    const int responseTextPadding = 20;

    // Use this for initialization
    void Start () {

        ringtone = Phone.GetComponent<AudioSource>();
        BlackScreenImage = BlackScreen.GetComponent<Image>();

        soundTime = Time.timeSinceLevelLoad+5;
        currTime = 10000;
        waitTime = fadeDuration + 0.5f;

        IncomingText.GetComponent<Graphic>().CrossFadeAlpha(0.01f, 0.01f, true);
        ResponseText1.GetComponent<Graphic>().CrossFadeAlpha(0.01f, 0.0f, true);
        ResponseText2.GetComponent<Graphic>().CrossFadeAlpha(0.01f, 0.01f, true);
        ResponseText3.GetComponent<Graphic>().CrossFadeAlpha(0.01f, 0.01f, true);
        ActionText.GetComponent<Graphic>().CrossFadeAlpha(0.01f, 0.01f, true);
       

        if (sceneScript1 != null || sceneScript2 != null || sceneScript3 != null || sceneScript4 != null)
        {
            displayLines1 = (sceneScript1.text.Split('\n'));
            displayLines2 = (sceneScript2.text.Split('\n'));
            displayLines3 = (sceneScript3.text.Split('\n'));
            displayLines4 = (sceneScript4.text.Split('\n'));
            displayLines5 = (sceneScript5.text.Split('\n'));
        }

        if (endLine == 0)
        {
            endLine = displayLines1.Length - 1;
            soundTime = Time.timeSinceLevelLoad;
        }

       ConversationSprites = Resources.LoadAll<Sprite>("Sprites/");
    }

    void FixedUpdate() {
        // Set Native Sizes
        ActionImage.SetNativeSize();
        ResponseImage1.SetNativeSize();
        ResponseImage2.SetNativeSize();
        ResponseImage3.SetNativeSize();

        ShowAndCloseConversation();
    }
	
	// Update is called once per frame
	void Update () {

        displayText1.text = displayLines1[responseLine];
        displayText2.text = displayLines2[responseLine];
        displayText3.text = displayLines3[responseLine];
        displayText4.text = displayLines4[responseLine];
        displayText5.text = displayLines5[actionTextLine];

        ActionImage.sprite = ConversationSprites[responseLine];

        // Prevent array out of bounds
        if (conversationSet <= 6)
        {
            // Logic = Offset + ResponseIndex + ConversationSet
            ResponseImage1.sprite = ConversationSprites[responseSpriteOffset + 0 + conversationSet];
            ResponseImage2.sprite = ConversationSprites[responseSpriteOffset + 1 + conversationSet];
            ResponseImage3.sprite = ConversationSprites[responseSpriteOffset + 2 + conversationSet];
        }
        else {
            ResponseImage1.gameObject.SetActive(false);
            ResponseImage2.gameObject.SetActive(false);
            ResponseImage3.gameObject.SetActive(false);
        }

        if (Time.timeSinceLevelLoad > soundTime + 8)
        {
            ringtone.Play();
            soundTime = Time.timeSinceLevelLoad;
        }

        if (Time.timeSinceLevelLoad> 15 && displayState == 5)
        {
            currTime = Time.timeSinceLevelLoad;
            soundTime = 15000;
            displayState = 1;
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
            displayState++;
            isShowingConversation = true;
        }


        if (Time.timeSinceLevelLoad > currTime + 10.0f && canShowActionText == true && displayState == 3)
        {
           // SunBlocker.SetActive(false);
            ActionText.GetComponent<Graphic>().CrossFadeAlpha(1.0f, fadeDuration, true);
        }

        if (Time.timeSinceLevelLoad > currTime + 18.0f && canShowActionText == true && displayState == 3)
        {
            ActionText.GetComponent<Graphic>().CrossFadeAlpha(0.00f, 0.01f, true);
            actionTextLine = 2;
            displayState++;
            isShowingConversation = false;
        }

        if (Time.timeSinceLevelLoad > currTime + 108.0f && canShowActionText == true)
        {
            ActionText.GetComponent<Graphic>().CrossFadeAlpha(1.0f, fadeDuration, true);
            displayState++;
            canSurpassTV = true;
        }

        if (Time.timeSinceLevelLoad > currTime + 118.0f && canShowActionText == true)
        {
            ActionText.GetComponent<Graphic>().CrossFadeAlpha(0.01f, 0.01f, true);
            isShowingConversation = false;
        }



        if (Time.timeSinceLevelLoad > currTime + 7.0f && textEnd == true)
        {
            textEnd = false;
            DenialSceneScript.textOver = true;
            isShowingConversation = false;
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
        isShowingConversation = false;
        currTime = Time.timeSinceLevelLoad;
        displayState = 0;

    }

    void choice(int input){

        if (responseLine == 0)
        {
            if (input == 1) { responseLine = 1; }
            if (input == 2) { responseLine = 2; }
            if (input == 3) { responseLine = 2; }
            conversationSet = 3;
        }
        else if (responseLine == 1 || responseLine == 2)
        {
            if (input == 1) { responseLine = 3; }
            if (input == 2) { responseLine = 4; }
            if (input == 3) { responseLine = 4; }
            conversationSet = 6;
        }
        else if (responseLine == 3 || responseLine == 4)
        {
            if (input == 1) { responseLine = 5; actionTextLine = 0; }
            if (input == 2) { responseLine = 6; actionTextLine = 1; }
            if (input == 3) { responseLine = 7; actionTextLine = 0; }
            conversationSet = 9;
            canShowActionText = true;
            textEnd = true;
        }
    }

    void updateTextLayout() {
        //   ResponseText2.transform.position = new Vector2(ResponseText1.transform.position.x + ResponseText1.GetComponent<RectTransform>().rect.width + responseTextPadding, ResponseText2.transform.position.y);
        //  ResponseText3.transform.position = new Vector2(ResponseText2.transform.position.x + ResponseText2.GetComponent<RectTransform>().rect.width + responseTextPadding, ResponseText3.transform.position.y);
        ActionImage.transform.localPosition = new Vector2((DigitalItemCanvas.GetComponent<RectTransform>().rect.width / 2) - (ResponseImage1.transform.GetComponent<RectTransform>().rect.width / 2) - 70, ActionImage.transform.localPosition.y);
        ResponseImage1.transform.localPosition = new Vector2((DigitalItemCanvas.GetComponent<RectTransform>().rect.width/2) - (ResponseImage1.transform.GetComponent<RectTransform>().rect.width/2), ActionImage.transform.localPosition.y - ActionImage.transform.GetComponent<RectTransform>().rect.height - 10);
        if (conversationSet == 3)
        {
            // Hack for wrong alignment in the second set
            ResponseImage2.transform.localPosition = new Vector2((DigitalItemCanvas.GetComponent<RectTransform>().rect.width / 2) - (ResponseImage2.transform.GetComponent<RectTransform>().rect.width / 2), ResponseImage1.transform.localPosition.y - ResponseImage1.transform.GetComponent<RectTransform>().rect.height);
            ResponseImage3.transform.localPosition = new Vector2((DigitalItemCanvas.GetComponent<RectTransform>().rect.width / 2) - (ResponseImage3.transform.GetComponent<RectTransform>().rect.width / 2), ResponseImage2.transform.localPosition.y - ResponseImage2.transform.GetComponent<RectTransform>().rect.height - 20);
        }
        else {
            ResponseImage2.transform.localPosition = new Vector2((DigitalItemCanvas.GetComponent<RectTransform>().rect.width / 2) - (ResponseImage2.transform.GetComponent<RectTransform>().rect.width / 2), ResponseImage1.transform.localPosition.y - ResponseImage1.transform.GetComponent<RectTransform>().rect.height - 10);
            ResponseImage3.transform.localPosition = new Vector2((DigitalItemCanvas.GetComponent<RectTransform>().rect.width / 2) - (ResponseImage3.transform.GetComponent<RectTransform>().rect.width / 2), ResponseImage2.transform.localPosition.y - ResponseImage2.transform.GetComponent<RectTransform>().rect.height - 10);
        }
        
    }

    void ShowAndCloseConversation() {
        if (isShowingConversation == false)
        {
            ConversationContainer.gameObject.SetActive(false);
        }
        else {
            ConversationContainer.gameObject.SetActive(true);
            updateTextLayout();
        }
    }
}
