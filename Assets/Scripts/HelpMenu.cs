using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpMenu : MonoBehaviour
{
    Animator animator;

    public GameObject SettingButton;
    public GameObject HelpButton;
    public GameObject QuestPanel;
    public GameObject DicePanel;

    public GameObject QuestHelpPanel;
    public GameObject BattleHelpPanel;
    public GameObject ExitHelpButton;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if((animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") || animator.GetCurrentAnimatorStateInfo(0).IsName("HelpBook_Open")) && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            QuestHelpPanel.SetActive(true);
            BattleHelpPanel.SetActive(true);
            ExitHelpButton.SetActive(true);
        }

        if(animator.GetCurrentAnimatorStateInfo(0).IsName("HelpBook_Closed"))
        {
            QuestHelpPanel.SetActive(false);
            BattleHelpPanel.SetActive(false);
            ExitHelpButton.SetActive(false);

            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                SettingButton.SetActive(true);
                HelpButton.SetActive(true);
                QuestPanel.SetActive(true);
                DicePanel.SetActive(true);

                animator.Play("Idle");
            }
        }
    }
}
