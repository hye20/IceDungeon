using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int MaxHP = 100;
    public int HP=100;
    private int MaxMP = 200;
    public int MP=200;
    public int atk;
    public int def;
    public int SP;//spell power, 마법은 방관있음

    public int dice=6;
    public int AP=0; //activity point (dice*n)

    public PlayerController controller;
    
    void Start()
    {
        gameObject.name = this.tag;
        DontDestroyOnLoad(this.gameObject);
        controller = GetComponent<PlayerController>();
        Setstatus(gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Setstatus(string name)
    {
        if(name=="Pram")
        {
            HP = MaxHP; MP = MaxMP;
            atk = 30; def = 0; SP = 50;
        }
        else if (name == "Mao")
        {
            HP = MaxHP; MP = MaxMP;
            atk = 80; def = 0; SP = 20;
        }
    }
    public void UpdateMaxHP(int MaxHP)
    {
        this.MaxHP = MaxHP;
    }
    public void UpdateMaxMP(int MaxMP)
    {
        this.MaxMP = MaxMP;
    }
}
