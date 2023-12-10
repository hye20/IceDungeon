using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    PlayerController playerController;

    public AudioSource audioSource;
    public AudioClip DungeonBGM;
    bool isAudioPlaying = false;

    public DoorTrigger DoorTrigger;
    public AudioSource EnvironmentAudioSource;
    public AudioClip EnvironmentClip;
    bool isEnvironmentAudioPlaying = false;

    void Start()
    {
        if(GameObject.Find("Mao"))
        {
            playerController = GameObject.Find("Mao").GetComponent<PlayerController>();
        }        
    }
    
    void Update()
    {
        if(!playerController.isStart && !isAudioPlaying)
        {
            isAudioPlaying = true;
            audioSource.PlayOneShot(DungeonBGM);
        }

        if(DoorTrigger.doorOpened && !isEnvironmentAudioPlaying)
        {
            isEnvironmentAudioPlaying = true;
            EnvironmentAudioSource.PlayOneShot(EnvironmentClip);
        }
    }    
}
