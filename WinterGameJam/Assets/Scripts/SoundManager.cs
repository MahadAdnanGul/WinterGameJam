using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    AudioSource main;
    [SerializeField] AudioClip[] clips; 
    // Start is called before the first frame update
    void Start()
    {
        main=GetComponent<AudioSource>();
    }

    public void PlayButtonSound()
    {
        main.PlayOneShot(clips[0]);
    }
    public void PlayLoseSound()
    {
        main.PlayOneShot(clips[1]);
    }
    public void PlayAttackSound()
    {
        main.PlayOneShot(clips[2]);
    }
    public void PlayAttackPickup()
    {
        main.PlayOneShot(clips[3]);
    }
    public void PlayWinSound()
    {
        main.PlayOneShot(clips[4]);
    }
    public void PlayPush()
    {
        main.PlayOneShot(clips[5]);
    }

    // Update is called once per frame

}
