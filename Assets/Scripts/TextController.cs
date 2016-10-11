using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextController : MonoBehaviour {

    public GameObject textBox1;
    public GameObject textBox2;
    public GameObject selectTextBox1;
    public GameObject selectTextBox2;
    public GameObject titleTextBox;

    public Text displayText1;
    public Text displayText2;
    public Text displayText3;
    public Text displayText4;

    public TextAsset openingScript1;
    public TextAsset openingScript2;
    public TextAsset openingScript3;
    public TextAsset openingScript4;

    public string[] displayLines1;
    public string[] displayLines2;
    public string[] displayLines3;
    public string[] displayLines4;

    public GameObject BlackScreen;
    Graphic BlackScreenImage;

    float currTime;
    
    float fadeDuration = 1;
    float waitTime;
    float waterfallTime = 1;

    int currentLine;
    int endLine;

    int[] choices = { 0, 0, 0, 0, };
    int choiceNum = 0;

    int click = 0;
    bool lineUpdate = false;
    int title = 0;

	// Use this for initialization
	void Start () {
        waitTime = fadeDuration + 0.5f;

        BlackScreenImage = BlackScreen.GetComponent<Image>();

        if (openingScript1 != null || openingScript2 != null || openingScript3 != null || openingScript4 != null)
        {
            displayLines1 = (openingScript1.text.Split('\n'));
            displayLines2 = (openingScript2.text.Split('\n'));
            displayLines3 = (openingScript3.text.Split('\n'));
            displayLines4 = (openingScript4.text.Split('\n'));

        }

        if(endLine == 0)
        {
            endLine = displayLines1.Length - 1;


        }
	}
	
    void Update()
    {
        displayText1.text = displayLines1[currentLine];
        displayText2.text = displayLines2[currentLine];
        displayText3.text = displayLines3[currentLine];
        displayText4.text = displayLines4[currentLine];

        //if (Input.GetMouseButtonDown(0) && currentLine != endLine) 
        //{

        //    textTransition();
        //}

        if(currentLine == endLine && title == 0)
        {
            title++;
            textBox1.SetActive(false);
            BlackScreenImage.CrossFadeAlpha(0.01f, 0.01f, true);
            BlackScreen.SetActive(true);
            BlackScreenImage.CrossFadeAlpha(1.0f, 4.0f, true);
            currTime = Time.realtimeSinceStartup;

        }

        if (Time.realtimeSinceStartup > currTime + fadeDuration && lineUpdate == true)
        {
            currentLine++;
            lineUpdate = false;
        }


        if (Time.realtimeSinceStartup > currTime + waitTime && click == 1)
        {
            textBox1.GetComponent<Graphic>().CrossFadeAlpha(1.0f, fadeDuration, true);
            click++;
        }

        if (Time.realtimeSinceStartup > currTime + waitTime + waterfallTime  && click == 2)
        {
            textBox2.GetComponent<Graphic>().CrossFadeAlpha(1.0f, fadeDuration, true);
            click++;
        }
        if (Time.realtimeSinceStartup > currTime + waitTime + waterfallTime*2 && click == 3)
        {
            selectTextBox1.GetComponent<Graphic>().CrossFadeAlpha(1.0f, fadeDuration, true);
            selectTextBox2.GetComponent<Graphic>().CrossFadeAlpha(1.0f, fadeDuration, true);
            click = 0;
        }
        if (Time.realtimeSinceStartup > currTime + 6.0f && title == 1)
        {
            titleTextBox.GetComponent<Graphic>().CrossFadeAlpha(0.01f, 0.01f, true);
            titleTextBox.SetActive(true);
            titleTextBox.GetComponent<Graphic>().CrossFadeAlpha(1.0f, 3.0f, true);
            title++;
        }

    }

    public void textTransition()
    {

       
        //FADE OUT
        textBox1.GetComponent<Graphic>().CrossFadeAlpha(0.01f, fadeDuration, true);
        textBox2.GetComponent<Graphic>().CrossFadeAlpha(0.01f, fadeDuration, true);
        selectTextBox1.GetComponent<Graphic>().CrossFadeAlpha(0.01f, fadeDuration, true);
        selectTextBox2.GetComponent<Graphic>().CrossFadeAlpha(0.01f, fadeDuration, true);

        currTime = Time.realtimeSinceStartup;

        click = 1;
        lineUpdate = true;

    }

    public void UpdateChoice()
    {



    }
}
