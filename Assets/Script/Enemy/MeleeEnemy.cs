using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    [Header(" Attack ")]
    [SerializeField] private int damage;
    [SerializeField] private int attackFrequency;
    private float attackDelay;
    private float attackTimer;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        StartSetAttack();
    }

    private void StartSetAttack()
    {
        attackDelay = 1f / attackFrequency;
        attackTimer = 0f; 
    }

    void Update()
    {
        if (!hasSpawned())
            return;
        if (attackTimer >= attackDelay)
            TryAttack();
        else
            WaitForAttack();
        movement.FollowPlayer();
    }

    private void TryAttack()
    {
        float distanceToPlayer = Vector2.Distance((Vector2)player.transform.position, (Vector2)transform.position);
        if (distanceToPlayer <= playerDetectionRadius)
        {
            player.TakeDamage(damage);
            attackTimer = 0f;
        }
    }
    private void WaitForAttack()
    {
        attackTimer += Time.deltaTime;
    }
    private void OnDrawGizmos()
    {
        if (!gizmos)
        {
            return;
        }

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerDetectionRadius);
    }

}
