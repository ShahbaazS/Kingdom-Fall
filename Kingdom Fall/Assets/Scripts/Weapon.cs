using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour{
    
    public Transform FirePoint;
    public GameObject BulletPrefab;
    public GameObject BulletPrefab1;
    public float FireRate = 2f;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")){
            Shoot();
        }

        if (Input.GetButtonDown("Fire2")){
            ability();
        }
    }

    void Shoot (){
        Instantiate(BulletPrefab, FirePoint.position, FirePoint.rotation);
    }

    void ability (){
        Instantiate(BulletPrefab1, FirePoint.position, FirePoint.rotation);
    }
}
