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

    public GameOver GameOver;

    Enemy enemy;
    
    void Start(){
        health = MaxHealth;
        HealthBar.value = CalculateHealth();
        enemy = GetComponent<Enemy>();
    }

    void Update(){
        HealthBar.value = CalculateHealth();

        if (health < MaxHealth){
            HealthUI.SetActive(true);
        }
        if (health > MaxHealth){
            health = MaxHealth;
        }
    }

    public void Increase(){
        health = health + 30;
        MaxHealth = MaxHealth + 30;
    }

    public void Decrease(){
        MaxHealth = MaxHealth - 30;
    }

    //returns the current health percentage
    float CalculateHealth(){
        return health / MaxHealth;
    }

    //inflict damage onto the character
    public void TakeDamage (int damage){
        health -= damage;

        if (health <= 0){
            if (enemy != null)
            {
                StartCoroutine(enemy.Die());
            }
            else
            {
                Die();
            }
        }
    }

    void Die(){
        if (gameObject.name == "Player"){
            GameOver.Setup();
        }
        Destroy(gameObject);
    }
}
