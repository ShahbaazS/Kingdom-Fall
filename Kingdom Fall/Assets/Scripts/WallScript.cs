using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour
{
    [SerializeField] GameObject entity; // player

    void Update()
    {
        if (entity)
        {
            if (entity.tag == "Player")
            {
                Physics2D.IgnoreCollision(entity.GetComponent<Collider2D>(), GetComponent<Collider2D>(), true);
            }
        }
    }
}
