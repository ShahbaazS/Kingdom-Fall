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
    float startPosition;
    float speed = 100f;
    public float sightRange = 10f;
    public float shootingRange = 7f;
    public float maxSightRange = 15f;

    float nextShootTime = 0;
    float shootDelay = 0.5f;

    private void Awake()
    {
        playerControl = GetComponent<PlayerControl>();
        enemyPatrol = GetComponent<EnemyPatrol>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        state = State.Patrolling;
    }
    private void Start()
    {
        startPosition = transform.position.x;
    }

    private void Update()
    {
        switch (state)
        {
            default:
            case State.Patrolling:
                GenerateBound();
                FindTarget();
                break;
            case State.ChaseTarget:
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

    void GenerateBound()
    {
        float minBound = transform.position.x - Random.Range(5, 10);
        float maxBound = transform.position.x + Random.Range(5, 10);

        enemyPatrol.SetBounds(minBound, maxBound);
    }

    void Shoot()
    {
        Vector2 moveDir = (player.transform.position - bulletPrefab.transform.position).normalized * bulletPrefab.GetComponent<Bullet>().speed;
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bulletPrefab.GetComponent<Bullet>().rb.velocity = new Vector2(moveDir.x, moveDir.y);
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
