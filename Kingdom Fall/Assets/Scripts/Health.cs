using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Health : MonoBehaviour
{
    //current health and max health
    public float health = 100;
    public float MaxHealth = 100;

    //UI and slider for health bar
    public GameObject HealthUI;
    public Slider HealthBar;
    
    void Start(){
        health = MaxHealth;
        HealthBar.value = CalculateHealth();

    }

    void Update(){
        HealthBar.value = CalculateHealth();

        if (health < MaxHealth){
            HealthUI.SetActive(true);
        }
        
    }

    //returns the current health percentage
    float CalculateHealth(){
        return health / MaxHealth;
    }

    //inflict damage onto the character
    public void TakeDamage (int damage){
        health -= damage;

        if (health <= 0){
            Die();
        }
    }

    void Die(){
        Destroy(gameObject);
    }
}
