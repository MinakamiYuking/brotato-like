using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class Weapon : MonoBehaviour
{

    [Header(" Elements ")]
    [SerializeField] protected Transform hitDetectionTransform;

    [Header(" Attack ")]
    [SerializeField] protected int damage;
    [SerializeField] protected float damageFrequency;
    protected float damageDelay;
    protected float attackTimer;

    [Header(" Settings ")]
    [SerializeField] protected float range;
    [SerializeField] protected LayerMask enemyMask;

    [Header(" Animations ")]
    [SerializeField] protected float aimLerp;


    protected virtual void Start()
    {
        damageDelay = 1f / damageFrequency;
        attackTimer = 0f;
    }


    protected bool AutoAimAndHasEnemyInRange()
    {
        Enemy closestEnemy = GetClosestEnemy();
        Vector3 tagertVector = Vector3.up;
        
        if (closestEnemy != null)
        {
            tagertVector = (closestEnemy.transform.position - transform.position).normalized;
            transform.up = tagertVector;
            if(TryAttack()) return true;
        }
        else 
        {
        transform.up = Vector3.Lerp(transform.up, tagertVector, Time.deltaTime * aimLerp);
        }
        return false;
    }

    private Enemy GetClosestEnemy()
    {
        Enemy closestEnemy = null;
        Collider2D[] enemis = Physics2D.OverlapCircleAll(transform.position, range, enemyMask);
        float minDistance = range;
        for (int i = 0; i < enemis.Length; i++)
        {
            Enemy enemyChecked = enemis[i].GetComponent<Enemy>();
            float distanceToEnemy = Vector2.Distance(enemyChecked.transform.position, transform.position);
            if (minDistance >= distanceToEnemy)
            {
                closestEnemy = enemyChecked;
                minDistance = distanceToEnemy;
            }
        }
        return closestEnemy;
    }

    private bool TryAttack()
    {
        if (damageDelay <= attackTimer)
        {
            attackTimer = 0f; 
            return true;
        }
        return false;
    }

    protected void WaitForAttack()
    {
        attackTimer += Time.deltaTime;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
