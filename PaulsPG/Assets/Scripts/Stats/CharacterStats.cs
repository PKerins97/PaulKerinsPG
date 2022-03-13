
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth { get; private set;  }

    public int damage;
    

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    void Update() 
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(10);
        }
    
    }

    public void TakeDamage(int damage)
    {
        
        damage = Mathf.Clamp(damage, 0, int.MaxValue);


        currentHealth -= damage;
        print(transform.name + "Takes" + damage + "Damage.");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        print(transform.name + "died");
    }
}
