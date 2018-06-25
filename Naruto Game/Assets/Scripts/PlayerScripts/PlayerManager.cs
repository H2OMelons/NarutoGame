using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerManager{

    public Transform spawnPoint;                    // Reference to the spawn point to spawn this player
    [HideInInspector] public int playerNumber;      // The player this player manager is for
    [HideInInspector] public GameObject instance;   // Reference to the playable character 

    private PlayerController playerController;      // Reference to the player controller script
    private WeaponThrow weaponThrow;                // Reference to the weapon throwing script
    private GameManager gameManager;
    private Action<float> managerCallback;

    /**
     * Setup() sets up the player 
     */ 
    public void Setup()
    {
        // Get references to the components
        playerController = instance.GetComponent<PlayerController>();
        weaponThrow = instance.GetComponent<WeaponThrow>();

        // Set the player numbers for the scripts
        playerController.playerNumber = playerNumber;

        playerController.SetPlayerManager(this);
    }

    /**
     * DisableControl() disables play controls so it won't move while game is inactive
     */ 
    public void DisableControl()
    {
        playerController.enabled = false;
        weaponThrow.enabled = false;
    }

    /**
     * EnableControl() enables play controls so it will move while game is active
     */ 
    public void EnableControl()
    {
        playerController.StartGame();
        playerController.enabled = true;
        weaponThrow.enabled = true;
    }

    public void Reset()
    {
        playerController.Reset();
    }

    public void SetAction(Action<float> action)
    {
        this.managerCallback = action;
    }

    public void InflictDamage(float amt)
    {
        this.managerCallback(amt);
    }

    public void TakeDamage(bool takeDamage)
    {

    }
}
