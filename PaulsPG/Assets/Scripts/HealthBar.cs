using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour, IDamageable
{
    public Image healthBar;
    public float health;
    public float startHealth = 100;


   

    public void takeDamage(int amountOfDamage)
    {
        health = health - amountOfDamage;
        healthBar.fillAmount = health / startHealth;
    }

    public void heal(object healthAmount)
    {
        health = startHealth;
        health = Mathf.Min(health, startHealth);
    }
}
