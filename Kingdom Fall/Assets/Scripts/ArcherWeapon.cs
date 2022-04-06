using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherWeapon : MonoBehaviour
{
    //where the attack starts from
    public Transform FirePoint;

    //choose what to attack with
    public GameObject BulletPrefab;
    public GameObject AbilityPrefab;

    //Position of camera and speed of bullet and ability
    private Vector3 MyPos;
    public float speed = 20f;
    public int Arrows = 10;

    public PlayerMovement Movement;

    public float AttackSpeed = 0.7f;
    private float NextAttack = 0;

    //cooldown on ability
    public float AbilityCooldown = 2f;
    private float nextFireTime = 0;

    // Update is called once per frame
    void Start()
    {
        MyPos = Camera.main.WorldToScreenPoint(this.transform.position);

    }

    void Update()
    {
        MyPos = Camera.main.WorldToScreenPoint(this.transform.position); 
        if (Time.time > NextAttack){
            if (Input.GetButtonDown("Fire1") && ShootDirection() && Arrows > 0){
                ShootArrow();
                Arrows -= 1;
                NextAttack = Time.time + AttackSpeed;
            }
        }

        if (Time.time > nextFireTime){
            if (Input.GetButtonDown("Fire2") && Arrows > 0){
                EnchantedArrow();
                Arrows -= 1;
                nextFireTime = Time.time + AbilityCooldown;
        }   
        }
    }

    void ShootArrow(){
        GameObject arrow = (GameObject)Instantiate(BulletPrefab, FirePoint.position, Quaternion.identity);
        Vector3 direction = (Input.mousePosition - MyPos).normalized;
        arrow.GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x, direction.y) * speed;
    }

    void EnchantedArrow (){
        GameObject Ability = (GameObject)Instantiate(AbilityPrefab, FirePoint.position, Quaternion.identity);
        Vector3 direction = (Input.mousePosition - MyPos).normalized;
        Ability.GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x, direction.y) * speed;
    }

    private bool ShootDirection()
    {
        Vector3 direction = Input.mousePosition;
        if (Movement.facingRight == true && direction.x > MyPos.x){
            return true;
        }else if (Movement.facingRight == false && direction.x < MyPos.x){
            return true;
        }
        return false;
    }
}
