using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    enum Mode{QuestMode,BattleMode}
    private Mode mode;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(mode == Mode.QuestMode)
        {

        }
        else if(mode == Mode.BattleMode)
        {

        }
    }
}
