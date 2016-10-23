using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent (typeof (AudioSource))]

public class VideoPlayback : MonoBehaviour {

    public RawImage movieRawImage;
    public MovieTexture movieTexture;
    public AudioSource movieAudio;

    // Use this for initialization
    void Start()
    {
        movieRawImage = GetComponent<UnityEngine.UI.RawImage>();
        movieRawImage.texture = movieTexture as MovieTexture;
        movieAudio = GetComponent<AudioSource>();
        
        movieAudio.clip = movieTexture.audioClip;

        movieRawImage.enabled = false;
        movieTexture.Stop();
        movieAudio.Stop();
    }

    void Update() {

    }
   
}
