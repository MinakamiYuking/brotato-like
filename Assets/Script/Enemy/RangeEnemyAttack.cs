using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class RangeEnemyAttack : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private EnemyBullet bulletPrefab;
    private Player player;

    [Header(" Sttings ")]
    [SerializeField] private int damage;
    [SerializeField] private int attackFrequency;
    private float attackDelay;
    private float attackTimer;

    [SerializeField] private float bulletMoveSpeed;

    private ObjectPool<EnemyBullet> bulletPool;
    void Start()
    {
        StartSetAttack();
        bulletPool = new ObjectPool<EnemyBullet>(CreateFunc, ActionOnGet, ActionOnRelease, ActionOnDestroy);
    }
    private EnemyBullet CreateFunc() 
    {
        EnemyBullet bulletInstace = Instantiate(bulletPrefab, shootingPoint.position,Quaternion.identity);
        bulletInstace.Configure(bulletMoveSpeed, this);
        return bulletInstace;
    }
    private void ActionOnGet(EnemyBullet bullet) 
    {
        bullet.Reload();
        bullet.transform.position = shootingPoint.position;
        bullet.gameObject.SetActive(true);
    }
    private void ActionOnRelease(EnemyBullet bullet) 
    { 
        bullet.gameObject.SetActive(false);
    }
    private void ActionOnDestroy(EnemyBullet bullet) 
    { 
        Destroy(bullet.gameObject);
    }
    public void ReleaseBullet(EnemyBullet bullet)
    {
        if (bullet != null) bulletPool.Release(bullet);
    }
    private void StartSetAttack()
    {
        attackDelay = 1f / attackFrequency;
        attackTimer = 0f;
    }
    private void WaitForAttack()
    {
        attackTimer += Time.deltaTime;
    }
    void Update()
    {
    }
    public void TryAttack()
    {
        WaitForAttack();
        if (attackTimer >= attackDelay)
        {
            Shoot();
            attackTimer = 0f;
        }
    }
    private void Shoot()
    {
        Vector2 direction = (player.GetCenter() - (Vector2)shootingPoint.position).normalized;
        EnemyBullet bulletInstance = bulletPool.Get();
        bulletInstance.Shoot(damage, direction);
    }
    public void StorePlayer(Player player)
    {
        this.player = player;
    }

}
