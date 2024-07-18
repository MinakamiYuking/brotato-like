using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UIElements;

public class RangeWeapon : Weapon
{
    [Header(" Elenments ")]
    [SerializeField] private CandyGunBullet candyGunBulletPrefab;
    [SerializeField] private Transform shootingPoint;

    [Header(" Settings ")]
    [SerializeField] private float bulletMoveSpeed;

    private ObjectPool<CandyGunBullet> bulletPool;

    protected override void Start()
    {
        base.Start();
        bulletPool = new ObjectPool<CandyGunBullet>(CreateFunc, ActionOnGet, ActionOnRelease, ActionOnDestroy);
    }

    private void Update()
    {
        WaitForAttack();
        
        if (AutoAimAndHasEnemyInRange())
            Shoot();
        }

    private CandyGunBullet CreateFunc()
    {
        CandyGunBullet bulletInstace = Instantiate(candyGunBulletPrefab, shootingPoint.position, Quaternion.identity);
        bulletInstace.Configure( bulletMoveSpeed, criticalHitProbilities, this);
        return bulletInstace;
    }
    private void ActionOnGet(CandyGunBullet bullet)
    {
        bullet.transform.position = shootingPoint.position;
        bullet.gameObject.SetActive(true);
        bullet.Reload();
    }
    private void ActionOnRelease(CandyGunBullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }
    private void ActionOnDestroy(CandyGunBullet bullet)
    {
        Destroy(bullet.gameObject);
    }
    public void ReleaseBullet(CandyGunBullet bullet)
    {
        if (bullet != null) bulletPool.Release(bullet); 
        
    }

    private void Shoot()
    {
        /*        
        Vector3 axis_z = Vector3.forward;
        Vector3 axis_y = Vector3.up;
        float angle = transform.rotation.eulerAngles.z;
        Quaternion rotation = Quaternion.AngleAxis(angle, axis_z);
        Vector2 direction = (Vector2)(rotation * axis_y);
        Debug.Log(angle);
        Debug.Log(direction);
        Debug.Log(transform.up);
        */

        Vector2 direction = transform.up.normalized;
        CandyGunBullet bulletInstance = bulletPool.Get();
        bulletInstance.Shoot(damage, transform.up);

    }


}
