using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float MaxHP = 100f;
    public float HP=100;
    public float MaxMP = 200;
    public float MP=200;
    public float atk;
    public float def;
    public float SP;//spell power, ������ �������
    public float speed;
    public int priority;

    public int dice;
    public int AP; //activity point (dice*n)

    public PlayerController controller;
    
    void Start()
    {
        dice = 6;
        AP = 0;
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
            MaxHP = 100f;
            MaxHP = 100f;
            HP = MaxHP; MP = MaxMP;
            atk = 30f; def = 0f; SP = 50f;
        }
        else if (name == "Mao")
        {
            MaxHP = 100f;
            MaxHP = 100f;
            HP = MaxHP; MP = MaxMP;
            atk = 80f; def = 0f; SP = 50f;
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
