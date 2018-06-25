﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthColorManager : MonoBehaviour {

    private const float START_HEALTH = 100f;

    public Slider slider;
    public Image img;
    public Color deadHealthColor, fullHealthColor;

    private float currHealth = START_HEALTH;
    private bool dead = false;

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnEnable()
    {
        SetHealthUI();
    }

    private void SetHealthUI()
    {
        slider.value = currHealth;
        float amt = currHealth / START_HEALTH;
        img.color = Color.Lerp(deadHealthColor, fullHealthColor, amt);        
    }

    private void OnDeath()
    {
        dead = true;
    }

    public void TakeDamage(float dmg)
    {
        currHealth -= dmg;
        SetHealthUI();

        if(currHealth <= 0f && !dead)
        {
            OnDeath();
        }
    }
}
