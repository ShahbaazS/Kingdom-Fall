using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed = 20f;
    public int damage = 40;
    public Rigidbody2D rb;
    public float time = 0.2f;
    

    // Start is called before the first frame update
    void Start()
    {
    }

    void Update(){
        Destroy(this.gameObject, time);
    }

    void OnTriggerEnter2D(Collider2D hitInfo){

        Health health = hitInfo.GetComponent<Health>();
        if (health != null){
            health.TakeDamage(damage);
        }

        Destroy(gameObject);
    }

}
