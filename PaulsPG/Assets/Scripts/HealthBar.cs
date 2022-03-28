using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthBar;
    public float health;
    public float startHealth = 100;


    public void  onTakeDamage(int damage)
    {
        health = health - damage;
        healthBar.fillAmount = health / startHealth;
    }

    public void Healing(int healPoints)
    {
        startHealth += healPoints;
        startHealth = Mathf.Clamp(startHealth, 0, 100);

        healthBar.fillAmount = startHealth / 100;
    }
}
