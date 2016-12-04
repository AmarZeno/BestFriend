using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScreenControl : MonoBehaviour {

    public GameObject BlackScreen;
    Graphic BlackScreenImage;
    bool click = false;

    float currTime;
    float waitTime = 5.0f;

	void Start () {

        
        BlackScreenImage = BlackScreen.GetComponent<Image>();

    }

    public void FadeBlack()
    {
        if(click == false && Time.timeSinceLevelLoad > 0.5f)
        {
            currTime = Time.realtimeSinceStartup;
            BlackScreenImage.CrossFadeAlpha(0.0f, 4.0f, true);
            click = true;
        } 
    }

    void Update()
    {

        if (click == true)
        {
            if(Time.realtimeSinceStartup > currTime + waitTime)
            {
                gameObject.SetActive(false);
                BlackScreen.SetActive(false);

            }

        }


    }

    public void Deactivate(GameObject i_object)
    {
        i_object.SetActive(false);
    }

}
