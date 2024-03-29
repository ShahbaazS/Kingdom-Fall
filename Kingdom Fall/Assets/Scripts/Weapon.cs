using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour{
    
    //where the attack starts from
    public Transform FirePoint;

    //choose what to attack with
    public GameObject BulletPrefab;
    public GameObject AbilityPrefab;

    //Position of camera and speed of bullet and ability
    private Vector3 MyPos;
    public float speed = 20f;

    public PlayerMovement Movement;

    //cooldown on ability
    public float AbilityCooldown = 2;
    private float nextFireTime = 0;

    // Update is called once per frame
    void Start()
    {
        MyPos = Camera.main.WorldToScreenPoint(this.transform.position);

    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && ShootDirection()){
            ShootBullet();
        }

        if (Time.time > nextFireTime){
            if (Input.GetButtonDown("Fire2") ){
            ShootAbility();
            nextFireTime = Time.time + AbilityCooldown;
        }   
        }
    }

    void ShootBullet(){
        GameObject shot = (GameObject)Instantiate(BulletPrefab, FirePoint.position, Quaternion.identity);
        Vector3 direction = (Input.mousePosition - MyPos).normalized;
        shot.GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x, direction.y) * speed;
    }

    void ShootAbility (){
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
