using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RangeEnemyAttack))]
public class RangeEnemy : Enemy
{
    private RangeEnemyAttack enemyAttack;
    protected override void Start()
    {
        base.Start();
        enemyAttack = GetComponent<RangeEnemyAttack>();
        enemyAttack.StorePlayer(player);
    }
    void Update()
    {
        if (!hasSpawned())
            return;
        float distanceToPlayer = Vector2.Distance((Vector2)player.transform.position, (Vector2)transform.position);
        if (distanceToPlayer > playerDetectionRadius)
            movement.FollowPlayer();
        else
            enemyAttack.TryAttack();

        transform.localScale = (transform.position.x < player.transform.position.x) ? Vector3.one: new Vector3(-1,1,1);
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
