using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class KeyCodeScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        // Take the user to title screen
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            SceneManager.LoadScene(2);
        }

    }
}
