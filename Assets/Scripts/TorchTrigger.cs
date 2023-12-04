using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchTrigger : MonoBehaviour
{
    public GameObject[] TorchPrefabs = new GameObject[2];
    public ParticleSystem[] TorchParticle = new ParticleSystem[2];
    SpriteRenderer[] torchSpriteRenderer = new SpriteRenderer[2];

    public Sprite LightedTorchSprite;

    void Start()
    {
        for (int i = 0; i < TorchPrefabs.Length; i++)
        {
            torchSpriteRenderer[i] = TorchPrefabs[i].GetComponent<SpriteRenderer>();
        }
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Mao")
        {
            for(int i = 0; i < TorchPrefabs.Length; i++)
            {
                torchSpriteRenderer[i].sprite = LightedTorchSprite;
                TorchParticle[i].Play();
            }
        }
    }
}
