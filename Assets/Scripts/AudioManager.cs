using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    PlayerController playerController;

    AudioSource audioSource;
    public AudioClip DungeonBGM;
    bool isAudioPlaying = false;

    void Start()
    {
        if(GameObject.Find("Mao"))
        {
            playerController = GameObject.Find("Mao").GetComponent<PlayerController>();
        }

        audioSource = GetComponent<AudioSource>();
    }
    
    void Update()
    {
        if(!playerController.isStart && !isAudioPlaying)
        {
            isAudioPlaying = true;
            audioSource.PlayOneShot(DungeonBGM);
        }
    }    
}
