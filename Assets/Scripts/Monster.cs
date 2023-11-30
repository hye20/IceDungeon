using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public int HP=100;
    public int atk;
    public string[] skills;
    public string[] actions;
    public bool is_dead;
    // Start is called before the first frame update
    void Start()
    {
        HP = 100;
        is_dead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(HP<=0)
        {
            is_dead = true;
        }
        else is_dead = false;
    }
    public void Action()//argument = akt, def, counter
    {
        //akt, def, counter
    }
}
