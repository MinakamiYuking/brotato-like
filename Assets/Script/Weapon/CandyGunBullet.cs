using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D),typeof(Rigidbody2D))]
public class CandyGunBullet : MonoBehaviour
{
    [Header(" Components ")]
    [SerializeField] CircleCollider2D bulletCollider;
    [SerializeField] Rigidbody2D bulletRigid;
    private RangeWeapon rangeWeapon;

    [Header(" Settings ")]
    private float bulletMoveSpeed;
    private int damage;
    private Enemy firstTouchTarget;
    void Start()
    {
        bulletCollider = gameObject.GetComponent<CircleCollider2D>();
        bulletRigid = gameObject.GetComponent<Rigidbody2D>();
    }

    public void Configure(float bulletMoveSpeed, RangeWeapon rangeWeapon)
    {
        this.bulletMoveSpeed = bulletMoveSpeed;
        this.rangeWeapon = rangeWeapon;
    }
    public void Reload()
    {
        firstTouchTarget = null;
        bulletRigid.velocity = Vector2.zero;
        bulletCollider.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (firstTouchTarget != null)
            return ;
        if (collision.TryGetComponent(out Enemy enemy))
        {
            firstTouchTarget = enemy;
            enemy.TakeDamage(damage);
            bulletCollider.enabled = false;
            rangeWeapon.ReleaseBullet(this);
        }
        else if (collision.tag == "wall")
        {
            bulletCollider.enabled = false;
            rangeWeapon.ReleaseBullet(this);
        }

    }

    public void Shoot(int damage, Vector2 direction)
    {
        this.damage = damage;

        transform.right = direction;
        bulletRigid.velocity = direction * bulletMoveSpeed;
    }

}
