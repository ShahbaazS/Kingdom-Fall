using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightWeapon : MonoBehaviour
{
    //where the attack starts from
    public Transform FirePoint;

    //choose what to attack with
    public GameObject BulletPrefab;
    public GameObject SwordPrefab;
    public Health health;

    //Position of camera and speed of bullet and ability
    private Vector3 MyPos;
    public float speed = 20f;

    public PlayerMovement Movement;
    public float duration = 5;
    public bool powering = false;

    public float AttackSpeed = 0.1f;
    private float NextAttack = 0;

    //cooldown on ability
    public float AbilityCooldown = 8;
    private float nextFireTime = 0;

    // Update is called once per frame
    void Start()
    {
        MyPos = Camera.main.WorldToScreenPoint(this.transform.position);
    }

    void Update()
    {
        if (Time.time > NextAttack){
            if (Input.GetButtonDown("Fire1") && ShootDirection()){
                ThrowSword();
                NextAttack = Time.time + AttackSpeed;
            }
        }

        if (Time.time > nextFireTime){
            if (Input.GetButtonDown("Fire2") ){ 
                Sword swords = SwordPrefab.GetComponent<Sword>();
                swords.PowerUp();
                powering = true;
                duration = 5;
                nextFireTime = Time.time + AbilityCooldown;
        }   
        }
        if(powering == true){
        duration -= Time.deltaTime;
        if (duration <= 0){
            Sword swords = SwordPrefab.GetComponent<Sword>();
            swords.PowerDown();
            powering = false;
        }
        }
        
    }

    void ThrowSword(){
        GameObject sword = (GameObject)Instantiate(BulletPrefab, FirePoint.position, Quaternion.identity);
        Vector3 direction = (Input.mousePosition - MyPos).normalized;
        sword.GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x, direction.y) * speed;
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
