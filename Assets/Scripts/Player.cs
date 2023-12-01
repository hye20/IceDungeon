using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController controller;
    public Transform playerSpawnPoint;

    private int MaxHP = 100;
    public int HP=100;
    private int MaxMP = 200;
    public int MP=200;
    public int atk;
    public int def;
    public int SP;//spell power

    public int dice=6;
    public int AP=0; //activity point (dice*n)

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        controller = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
