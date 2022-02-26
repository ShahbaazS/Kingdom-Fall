using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Health : MonoBehaviour
{

    public float health = 100;
    public float MaxHealth = 100;

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

    float CalculateHealth(){
        return health / MaxHealth;
    }

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
