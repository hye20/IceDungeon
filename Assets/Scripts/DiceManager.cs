using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DiceManager : MonoBehaviour
{
    public GameObject AnimatedDice;
    public GameObject NumberedDice;
    public Sprite[] DiceNumberSprites = new Sprite[6];
    public TextMeshProUGUI DiceCountText;
    public Button DiceButton;

    public int diceCount;
    int randomDiceNumber;

    AudioSource audioSource;

    PlayerController playerController;

    void Awake()
    {
        diceCount = 0;

        audioSource = GetComponent<AudioSource>();

        if (GameObject.FindGameObjectWithTag("Pram"))
        {
            playerController = GameObject.Find("Pram").GetComponent<PlayerController>();
        }
        else if (GameObject.FindGameObjectWithTag("Mao"))
        {
            playerController = GameObject.Find("Mao").GetComponent<PlayerController>();
        }
    }

    void Update()
    {
        randomDiceNumber = Random.Range(0, 6);
        DiceCountTextUpdate(diceCount);

        if (playerController.DiceCount == 0)
        {
            AnimatedDice.SetActive(true);
            NumberedDice.SetActive(false);
            DiceButton.interactable = true;
        }
    }

    public void DiceClicked()
    {
        audioSource.Play();


        NumberedDice.GetComponent<Image>().sprite = DiceNumberSprites[randomDiceNumber];
        diceCount = randomDiceNumber + 1;
        DiceCountText.text = diceCount.ToString();

        playerController.DiceCount = diceCount;
        playerController.IsPlayerTurn = true;

        AnimatedDice.SetActive(false);
        NumberedDice.SetActive(true);
        DiceButton.interactable = false;
    }

    void DiceCountTextUpdate(int diceCount)
    {
        DiceCountText.text = diceCount.ToString();
    }
}
