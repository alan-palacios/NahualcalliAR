using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip buttonClick, targetFound, targetLost, screenShot, recording;

    AudioSource audioSource;

    public void Start(){
            audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(string sound){

        switch(sound){
            case "buttonClick":
                audioSource.pitch = (Random.Range(0.6f, 1.2f));
                audioSource.PlayOneShot(buttonClick);
                break;
            case "targetFound":
                audioSource.PlayOneShot(targetFound);
                break;
            case "targetLost":
                audioSource.PlayOneShot(targetLost);
                break;
            case "screenShot":
                audioSource.PlayOneShot(screenShot);
                break;
            case "recording":
                audioSource.PlayOneShot(recording);
                break;
        }
    }
}
