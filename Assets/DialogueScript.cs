using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DialogueScript : MonoBehaviour {

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
    float fadeDuration = 1;
    float waitTime;
    float waterfallTime = 0.4f;

    int endLine =0;
    int currentLine = 0;
    int displayState = 0;

    // Use this for initialization
    void Start () {

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


        }

    }
	
	// Update is called once per frame
	void Update () {

        displayText1.text = displayLines1[currentLine];
        displayText2.text = displayLines2[currentLine];
        displayText3.text = displayLines3[currentLine];
        displayText4.text = displayLines4[currentLine];

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
    }
}
