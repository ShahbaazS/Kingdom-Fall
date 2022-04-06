using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    //damage of Arrow 
    public float speed;
    public int damage = 40;
    public Rigidbody2D rb;
    
    private Vector3 mousePos;

    //time until bullet disappears (range of bullet)
    public float time = 15f;
    

    // Start is called before the first frame update
    void Start()
    {
    }

    void Update(){
        Destroy(this.gameObject, time);
    }

    //detection for collision and deal damage
    void OnTriggerEnter2D(Collider2D hitInfo){

        Health health = hitInfo.GetComponent<Health>();
        if (health != null){
            health.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
