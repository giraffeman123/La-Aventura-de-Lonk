using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


[RequireComponent (typeof(AudioSource))]

public class PlayVideo : MonoBehaviour {

    public MovieTexture movie_clip;
    private AudioSource audio_movie;
    //static PlayVideo instance = null;

    // Use this for initialization
    void Start () {
        GetComponent<RawImage>().texture = movie_clip as MovieTexture;
        audio_movie = GetComponent<AudioSource>();
        audio_movie.clip = movie_clip.audioClip;
        movie_clip.Play();
        audio_movie.Play();
    }
	
	// Update is called once per frame
	void Update () {
        if (movie_clip.isPlaying.Equals(false) && movie_clip.name.Equals("kami_video"))
        {
            SceneManager.LoadScene("Menu");
        }
        if (movie_clip.isPlaying.Equals(false) && movie_clip.name.Equals("La Aventura de Lonk"))
        {
            SceneManager.LoadScene("Level 3(Forest)");
        }
    }
}
