using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePoint;

    private enum State
    {
        Patrolling,
        ChaseTarget,
        Struggle,
    }

    State state;

    PlayerControl playerControl;
    EnemyPatrol enemyPatrol;

    Transform player;
    float bulletSpeed = 20f;
    public float sightRange = 10f;
    public float shootingRange = 7f;
    public float maxSightRange = 15f;

    float nextShootTime = 0;
    float shootDelay = 0.5f;

    private void Awake()
    {
        playerControl = GetComponent<PlayerControl>();
        enemyPatrol = GetComponent<EnemyPatrol>();
        state = State.Patrolling;
    }

    private void Update()
    {
        if(GameObject.FindGameObjectWithTag("Player") != null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;

            switch (state)
            {
                default:
                case State.Patrolling:
                    GenerateBound();
                    FindTarget();
                    break;
                case State.ChaseTarget:
                    enemyPatrol.isBound = false;
                    MoveToTarget();

                    if (Vector2.Distance(transform.position, player.position) < shootingRange)
                    {
                        if(Time.time > nextShootTime)
                        {
                            enemyPatrol.SetMoving(false);
                            Shoot();
                            nextShootTime = Time.time + shootDelay;
                        }
                        if (playerControl.resistStarted)
                        {
                            state = State.Struggle;
                        }
                    }
                    else
                    {
                        enemyPatrol.SetMoving(true);
                    }

                    if(Vector2.Distance(transform.position, player.position) > maxSightRange)
                    {
                        state = State.Patrolling;
                    }
                    break;

                case State.Struggle:
                    enemyPatrol.SetMoving(false);
                    if (!playerControl.resistStarted)
                    {
                        state = State.ChaseTarget;
                    }
                    break;
            }
        }

    }

    void GenerateBound()
    {
        float minBound = transform.position.x - Random.Range(5, 10);
        float maxBound = transform.position.x + Random.Range(5, 10);

        enemyPatrol.SetBounds(minBound, maxBound);
    }

    void Shoot()
    {
        GameObject Bullet = (GameObject)Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Vector3 direction = (player.transform.position - firePoint.transform.position).normalized;
        Bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x, direction.y) * bulletSpeed;
    }

    void FindTarget()
    {
        float distanceToPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceToPlayer < sightRange)
        {
            state = State.ChaseTarget;
        }
    }

    void MoveToTarget()
    {
        if (player.position.x < transform.position.x && enemyPatrol.isFacingRight())
        {
            enemyPatrol.ChangeDirection();
        }
        else if (player.position.x > transform.position.x && !enemyPatrol.isFacingRight())
        {
            enemyPatrol.ChangeDirection();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.DrawWireSphere(transform.position, shootingRange);
        Gizmos.DrawWireSphere(transform.position, maxSightRange);
    }
}
