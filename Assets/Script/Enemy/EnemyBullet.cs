using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D),typeof(Collider2D))]
public class EnemyBullet : MonoBehaviour
{
    [Header(" Components ")]
    [SerializeField] private Rigidbody2D rigid;
    [SerializeField] private Collider2D bulletCollider;
    private RangeEnemyAttack rangeEnemyAttack;

    [Header(" Settings ")]
    [SerializeField] private float moveSpeed;
    private int damage;


    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        bulletCollider = GetComponent<CapsuleCollider2D>();
    }
    public void Configure(RangeEnemyAttack rangeEnemyAttack)
    {
        this.rangeEnemyAttack = rangeEnemyAttack;
    }
    public void Shoot(int damage,Vector2 direction)
    {
        this.damage = damage;

        transform.right = direction;
        rigid.velocity = direction * moveSpeed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player)) 
        {
            player.TakeDamage(damage);
            bulletCollider.enabled = false;
            rangeEnemyAttack.ReleaseBullet(this);
        }else if (collision.tag == "wall")
        {
            bulletCollider.enabled = false;
            rangeEnemyAttack.ReleaseBullet(this);
        }
        
    }

    public void Reload()
    {
        rigid.velocity = Vector2.zero;
        bulletCollider.enabled = true;
    }

}
