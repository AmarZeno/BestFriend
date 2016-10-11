using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayVideo : MonoBehaviour {

    public MovieTexture background;

	void Start () {

        GetComponent<RawImage>().texture = background as MovieTexture;
        background.Play();
        background.loop = true; 
	}
	

	void Update () {


	
	}
}
