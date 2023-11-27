using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    DialogueManager dialogueM;
    public void HideUI()
    {
        throw new NotImplementedException();
    }

    public void SettingUI(bool p_flag)
    {
        // 안보일 것 이름.SetActive(p_flag);
        
    }

    IEnumerator WaitCollision()
    {
        yield return new WaitUntil(()=>PlayerController.isCollide);
    }
}
