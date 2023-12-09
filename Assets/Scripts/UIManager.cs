using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject SettingButton;
    public GameObject HelpButton;
    public GameObject QuestPanel;
    public GameObject DicePanel;

    public Camera StarterCamera;

    void Start()
    {
        
    }
    
    void Update()
    {
        if (!GameManager.instance.player.controller.isStart && GameManager.instance.mode == GameManager.Mode.QuestMode)
        {
            SettingButton.SetActive(true);
            HelpButton.SetActive(true);
            QuestPanel.SetActive(true);
            DicePanel.SetActive(true);

            Destroy(StarterCamera);
        }
    }
}
