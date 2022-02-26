using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Health : MonoBehaviour
{

    public float health = 100;
    public float MaxHealth = 100;

    public GameObject deathEffect;
    public GameObject healthUI;
    public Slider slider;
    
    void Start(){
        health = MaxHealth;
        slider.value = CalculateHealth();
    }

    void Update(){
        slider.value = CalculateHealth();

        if (health < MaxHealth){
            healthUI.SetActive(true);
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
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
