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
    int randomDiceNumber;

    AudioSource audioSource;


    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        randomDiceNumber = Random.Range(0, 6);
        DiceCountTextUpdate(GameManager.instance.player.controller.DiceCount);

        if (GameManager.instance.player.controller.DiceCount == 0)
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
        GameManager.instance.player.controller.DiceCount = randomDiceNumber + 1;
        DiceCountText.text = GameManager.instance.player.controller.DiceCount.ToString();

        GameManager.instance.player.controller.IsPlayerTurn = true;        

        AnimatedDice.SetActive(false);
        NumberedDice.SetActive(true);
        DiceButton.interactable = false;
    }

    void DiceCountTextUpdate(int diceCount)
    {
        DiceCountText.text = diceCount.ToString();
    }
}
