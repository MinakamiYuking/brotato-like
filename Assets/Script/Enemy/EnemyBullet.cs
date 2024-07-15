using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D),typeof(CapsuleCollider2D))]
public class EnemyBullet : MonoBehaviour
{
    [Header(" Components ")]
    [SerializeField] private CapsuleCollider2D bulletCollider;
    [SerializeField] private Rigidbody2D bulletRigid;
    private RangeEnemyAttack rangeEnemyAttack;


    private int damage;
    private float bulletMoveSpeed;


    private void Start()
    {
        bulletCollider = GetComponent<CapsuleCollider2D>();
        bulletRigid = GetComponent<Rigidbody2D>();
    }
    public void Configure(float bulletMoveSpeed, RangeEnemyAttack rangeEnemyAttack)
    {
        this.bulletMoveSpeed = bulletMoveSpeed;
        this.rangeEnemyAttack = rangeEnemyAttack;
    }
    
    public void Reload()
    {
        bulletCollider.enabled = true;
        bulletRigid.velocity = Vector2.zero;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            player.TakeDamage(damage);
            bulletCollider.enabled = false;
            rangeEnemyAttack.ReleaseBullet(this);
        }
        else if (collision.tag == "wall")
        {
            bulletCollider.enabled = false;
            rangeEnemyAttack.ReleaseBullet(this);
        }

    }

    public void Shoot(int damage, Vector2 direction)
    {
        this.damage = damage;

        transform.right = direction;
        bulletRigid.velocity = direction * bulletMoveSpeed;
    }
}
