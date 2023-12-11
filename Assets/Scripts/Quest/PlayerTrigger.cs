using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTrigger : MonoBehaviour
{
    // Quest game Manager
<<<<<<< Updated upstream
    GManager gManager;
=======
    EventManager eventManager;
>>>>>>> Stashed changes
    InteractionController interactionController;

    // interactionType 가 소지하고있는 이름 혹은 그 값들
    InteractionType interactionType;
    public GameObject player;

    public float lateTime;

    bool isTalk;


    void Start()
    {
        interactionType = GetComponent<InteractionType>();
    }

    private void Update()
    {
        if (isTalk && Input.GetKeyDown(KeyCode.Q))
        {
            TalkPlay(); 
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name == "Mao" || collision.name == "Pram")
        {
            player = collision.gameObject;
            interactionController = player.GetComponent<InteractionController>();
            if (!isTalk)
            {
                Invoke("TalkPlay", lateTime);
            }
           
            isTalk = true;
        }
    }

    public void TalkPlay()
    {
        interactionController.Action();
    }

    public void OnDestroy()
    {
        Destroy(this.gameObject);
    }
}
