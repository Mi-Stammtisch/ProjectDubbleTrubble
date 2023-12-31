using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiShait : MonoBehaviour
{
   //Intstance this
    public static UiShait Instance;

    public GameObject Player1HealthBar;
    public GameObject Player2HealthBar;

    public GameObject Player1StaminaBar;
    public GameObject Player2StaminaBar;

    [SerializeField] private GameObject player1Ui;
    [SerializeField] private GameObject player2Ui;

    [SerializeField] float UIyOffset = 1f;

    private GameObject[] players = new GameObject[2];
    
    private void Awake()
    {
        Instance = this;       

        GameObject[] besserePOlayers = GameObject.FindGameObjectsWithTag("Player");
        PlayerMovement player = besserePOlayers[0].GetComponent<PlayerMovement>();
        if(player.playerIndex == 0)
        {
            players[0] = besserePOlayers[0];
            players[1] = besserePOlayers[1];
        }
        else
        {
            players[0] = besserePOlayers[1];
            players[1] = besserePOlayers[0];
        }
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    { 
        //set the ui to the player position
        player1Ui.transform.position = players[0].transform.position + new Vector3(0,UIyOffset,0);
        player2Ui.transform.position = players[1].transform.position + new Vector3(0,UIyOffset,0);
    }

    public void updateHealth(byte index, int currentHealth){
        if(index == 0)
        {
            updatePlayer1HealthBar(currentHealth);
        }
        else if(index == 1)
        {
            updatePlayer2HealthBar(currentHealth);
        }

    }

    public void updateStamina(byte index, float currentStamina){
        if(index == 0)
        {
            updatePlayer1StaminaBar(currentStamina);
        }
        else if(index == 1)
        {
            updatePlayer2StaminaBar(currentStamina);
        }
    }


    public void updatePlayer1HealthBar(int currentHealth)
    {
        Player1HealthBar.GetComponent<HealthBarController>().setHealth(currentHealth);
    }

    public void updatePlayer2HealthBar(int currentHealth)
    {
        Player2HealthBar.GetComponent<HealthBarController>().setHealth(currentHealth);
    }

    public void updatePlayer1StaminaBar(float currentStaminapercentage)
    {
        Player1StaminaBar.GetComponent<BoostMeter>().SetBoost(currentStaminapercentage);
    }

    public void updatePlayer2StaminaBar(float currentStaminapercentage)
    {
        Player2StaminaBar.GetComponent<BoostMeter>().SetBoost(currentStaminapercentage);
    }

}
