using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IceCrackTrigger : MonoBehaviour
{
    BoxCollider2D boxCollider;
    bool isTriggered;

    MinigameManager minigameManager;

    SpriteRenderer currentSpriteRenderer;
    public Sprite CrackedSprite;

    AudioSource audioSource;

    void Start()
    { 
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
        if (!isTriggered && (GameManager.instance.player.transform.position - new Vector3(0, 0.25f, 0)) == transform.position)
        {
            isTriggered = true;
        }

        if (isTriggered && (GameManager.instance.player.transform.position - new Vector3(0, 0.25f, 0) != transform.position))
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
