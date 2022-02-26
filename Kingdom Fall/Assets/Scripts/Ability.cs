using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 80;
    public Rigidbody2D rb;
    


    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }


    void OnTriggerEnter2D(Collider2D hitInfo){
        Health health = hitInfo.GetComponent<Health>();
        if (health != null){
            health.TakeDamage(damage);
        }

        Destroy(gameObject, 5);
    }
}
