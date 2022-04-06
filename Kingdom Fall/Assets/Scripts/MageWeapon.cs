using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageWeapon : MonoBehaviour
{
    //where the attack starts from
    public Transform FirePoint;

    //choose what to attack with
    public GameObject BulletPrefab;
    public GameObject AbilityPrefab;

    //Position of camera and speed of bullet and ability
    private Vector3 MyPos;
    public float speed = 20f;

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
            if (Input.GetButtonDown("Fire1") && ShootDirection()){
                ShootBall();
                NextAttack = Time.time + AttackSpeed;
            }
        }

        if (Time.time > nextFireTime){
            if (Input.GetButtonDown("Fire2") ){
                for(int i = 0; i < 5; i++){
                    
                }
            nextFireTime = Time.time + AbilityCooldown;
        }   
        }
    }

    void ShootBall(){
        GameObject FireBall = (GameObject)Instantiate(BulletPrefab, FirePoint.position, Quaternion.identity);
        Vector3 direction = (Input.mousePosition - MyPos).normalized;
        FireBall.GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x, direction.y) * speed;
    }

    void FireBallRain(){
        GameObject FireBall = (GameObject)Instantiate(AbilityPrefab, FirePoint.position, Quaternion.identity);
        Vector3 direction = (Input.mousePosition - MyPos).normalized;
        FireBall.GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x, direction.y) * speed;
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
