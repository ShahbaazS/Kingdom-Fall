using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnchantedArrow : MonoBehaviour
{

    //damage of ability
    public int damage = 80;

    public Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    //detection of collision to deal damage
    void OnTriggerEnter2D(Collider2D hitInfo){
        Health health = hitInfo.GetComponent<Health>();
        if (health != null){
            health.TakeDamage(damage);
        }

        Destroy(gameObject, 5);
    }
}
