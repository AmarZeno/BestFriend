using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TitleFade: MonoBehaviour {

    public GameObject BlackScreen;
    Graphic BlackScreenImage;

    public GameObject titleTextBox;

    float currTime;
    float fadeDuration = 1;
    int titleState = 0;

    // Use this for initialization
    void Start () {

        BlackScreenImage = BlackScreen.GetComponent<Image>();
        currTime = Time.realtimeSinceStartup;
        titleTextBox.GetComponent<Graphic>().CrossFadeAlpha(0.01f, 0.01f, true);
    }
	
	// Update is called once per frame
	void Update () {

        if(Time.realtimeSinceStartup > currTime + 1 && titleState == 0)
        {
            titleTextBox.GetComponent<Graphic>().CrossFadeAlpha(1, fadeDuration, true);
            titleState++;

        }

        if (Time.realtimeSinceStartup > currTime + 4 && titleState == 1)
        {
            titleTextBox.GetComponent<Graphic>().CrossFadeAlpha(0.0f, fadeDuration, true);
            titleState++;
 
        }

        if (Time.realtimeSinceStartup > currTime + 5 && titleState == 2)
        {
            BlackScreenImage.CrossFadeAlpha(0.01f, fadeDuration, true);
            titleState++;

        }

        if(Time.realtimeSinceStartup > currTime + 6.5 && titleState == 3)
        {
            BlackScreen.SetActive(false);
            titleTextBox.SetActive(false);

        }

    }
}
