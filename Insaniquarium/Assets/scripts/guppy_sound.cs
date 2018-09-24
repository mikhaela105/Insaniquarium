using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class guppy_sound : MonoBehaviour {

    public AudioClip splashSound;
    public AudioClip roarSound;
    public AudioClip dieSound;

    public AudioSource audioSource;

    // Use this for initialization
    void Start () {
        audioSource.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void playSplashSound()
    {
        audioSource.clip = splashSound;
        audioSource.Play();
    }

    public void playRoarSound()
    {
        audioSource.clip = roarSound;
        audioSource.Play();
    }

    public void playDieSound()
    {
        audioSource.clip = dieSound;
        audioSource.Play();
    }
}
