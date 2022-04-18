using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    //damage of Arrow 
    public float speed;
    public int damage = 40;
    public Rigidbody2D rb;
    

    //time until bullet disappears (range of bullet)
    public float time = 0.6f;

    float currentTime = 0f;

    void Update(){
        Destroy(this.gameObject, time);
    }

    //detection for collision and deal damage
    void OnTriggerEnter2D(Collider2D hitInfo){

        Health health = hitInfo.GetComponent<Health>();

        if(hitInfo.gameObject.layer == LayerMask.NameToLayer("Enemy")){
            if (health != null){
                if (Time.time > currentTime)
                {
                    health.TakeDamage(damage);
                    currentTime = Time.time + 1;
                }
            }
            Destroy(gameObject);
        }
    }
}
