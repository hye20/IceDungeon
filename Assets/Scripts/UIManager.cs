using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject SettingButton;
    public GameObject HelpButton;
    public GameObject QuestPanel;
    public GameObject DicePanel;

    //public Animator BookAnimator;
    //public Button HelpExitButton;

    public Camera StarterCamera;
    EventManager eventManager;

    void Start()
    {
        eventManager = GameObject.Find("EventManager").GetComponent<EventManager>();

        //HelpButton.GetComponent<Button>().onClick.AddListener(OnHelpButtonClicked);
        //HelpExitButton.onClick.AddListener(OnHelpExitButtonClicked);
    }
    
    void Update()
    {
        if (!GameManager.instance.player.controller.isStart && GameManager.instance.mode == GameManager.Mode.QuestMode)
        {
            if (eventManager.isAction)
            {
                SettingButton.SetActive(false);
                HelpButton.SetActive(false);
                QuestPanel.SetActive(false);
                DicePanel.SetActive(false);
            }
            else
            {
                SettingButton.SetActive(true);
                HelpButton.SetActive(true);
                QuestPanel.SetActive(true);
                DicePanel.SetActive(true);

                Destroy(StarterCamera);
            }
        }
    }

    //void OnHelpButtonClicked()
    //{
    //    eventManager.isAction = true;
    //}

    //void OnHelpExitButtonClicked()
    //{
    //    if (BookAnimator.GetCurrentAnimatorStateInfo(0).IsName("HelpBook_Closed")
    //        && BookAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
    //    {
    //        eventManager.isAction = false;
    //    }
    //}
}
