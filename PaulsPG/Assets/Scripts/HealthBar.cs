using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour, IDamageable
{
    public float updatedhealth;
    public float maxHealth;
    public float pointIncreasePerSecond;
    public Text healthUI;

    void Start()
    {
        maxHealth = 100;
        updatedhealth = 100;
        pointIncreasePerSecond = 1f;

    }

    void Update()
    {
        updatedhealth += pointIncreasePerSecond * Time.deltaTime;

        if (updatedhealth > maxHealth)
        {
            updatedhealth = 100;
        }
        if (updatedhealth < 0)
        {
            updatedhealth = 0;
        }
        healthUI.text = (int)updatedhealth + " Health";
    }

    public void takeDamage(int amountOfDamage)
    {
        updatedhealth = updatedhealth - amountOfDamage;
        healthUI.text = (int)updatedhealth + "Health";
    }

   
}
