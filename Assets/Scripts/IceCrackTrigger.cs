using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IceCrackTrigger : MonoBehaviour
{
    Transform playerPos;
    BoxCollider2D boxCollider;
    bool isTriggered;

    MinigameManager minigameManager;

    SpriteRenderer currentSpriteRenderer;
    public Sprite CrackedSprite;

    AudioSource audioSource;

    private PlayerController _controller;

    void Start()
    {
        if (GameObject.FindGameObjectWithTag("Mao"))
        {
            playerPos = GameObject.Find("Mao").GetComponent<Transform>();
        }

        _controller = GameObject.Find("Mao").GetComponent<PlayerController>();

        currentSpriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();

        audioSource = GetComponent<AudioSource>();

        if(SceneManager.GetActiveScene().name == "Minigame1")
        {
            minigameManager = GameObject.Find("MinigameManager").GetComponent<MinigameManager>();
        }
    }
    
    void Update()
    {
        if (!isTriggered && playerPos.position - new Vector3(0, 0.25f, 0) == transform.position)
        {
            isTriggered = true;
        }

        if (isTriggered && (playerPos.position - new Vector3(0, 0.25f, 0) != transform.position))
        {
            audioSource.Play();

            isTriggered = false;
            currentSpriteRenderer.sprite = CrackedSprite;
            boxCollider.enabled = true;

            minigameManager.IceCracked.Add(gameObject);

            if(SceneManager.GetActiveScene().name == "Minigame1")
            {
                minigameManager.IceCount++;
            }
        }
    }
}
