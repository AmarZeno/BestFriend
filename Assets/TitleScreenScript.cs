using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TitleScreenScript : MonoBehaviour {

    public GameObject Title;
    public GameObject Credits;
    public AudioSource background;


    public Vector3 CameraCreditsPosition;

    bool IsCameraMoving = false;
    bool CreditsBack;

    void Start()
    {
        background.time = 53.21f;   // assuming that you already have reference to your AudioSource
        background.Play();
    }
    void CameraMove()
        {
        if (IsCameraMoving)
        {
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, CameraCreditsPosition, 1.0f * Time.fixedDeltaTime);
            if (CreditsBack)
            {
                IsCameraMoving = false;
            }
        }
        else
        {
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(1.555f, 4.063f,5.121f), 1.0f * Time.fixedDeltaTime);
        }    

        }
    void FixedUpdate()
    {

        CameraMove();

    }

    public void StartButton()
    {

        SceneManager.LoadScene("Opening");

    }

    public void ExitButton()
    {
        Application.Quit();
    }


    public void CreditsButton()
    {
        Title.SetActive(false);
        Credits.SetActive(true);
        IsCameraMoving = true;
        CameraCreditsPosition = new Vector3(-2.79f, 4.69f, 4.77f);
    }

    public void CreditsBackButton()
    {

        Title.SetActive(true);
        Credits.SetActive(false);
        CreditsBack = true;

    }

    
}
