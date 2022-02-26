using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour{
    
    //where the attack starts from
    public Transform FirePoint;

    //choose what to attack with
    public GameObject BulletPrefab;
    public GameObject AbilityPrefab;

    //cooldown on ability
    public float AbilityCooldown = 2;
    private float nextFireTime = 0;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")){
            Shoot();
        }

        if (Time.time > nextFireTime){
            if (Input.GetButtonDown("Fire2") ){
            ability();
            nextFireTime = Time.time + AbilityCooldown;
        }   
        }
    }

    void Shoot (){
        Instantiate(BulletPrefab, FirePoint.position, FirePoint.rotation);
    }

    void ability (){
        Instantiate(AbilityPrefab, FirePoint.position, FirePoint.rotation);
    }
}
