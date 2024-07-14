using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Weapon : MonoBehaviour
{
    enum State
    {
        Idle,
        Attack
    }
    private State state;

    [Header(" Elements ")]
    [SerializeField] private Transform hitDetectionTransform;
    [SerializeField] private BoxCollider2D hitBox;

    [Header(" Attack ")]
    [SerializeField] private int damage;
    [SerializeField] private float damageFrequency;
    private float damageDelay;
    private float attackTimer;
    [SerializeField] private Animator animator;

    [Header(" Settings ")]
    [SerializeField]private float range;
    [SerializeField]private LayerMask enemyMask;

    [Header(" Animations ")]
    [SerializeField]private float aimLerp;


    private List<Enemy> damagedEnemies = new List<Enemy>();


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
            
        state = State.Idle;
        damageDelay = 1f / damageFrequency;
        attackTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Idle:
                AutoAim();
                break;

            case State.Attack:
                Attacking();
                break;
        }
        incrementAttackTimer();
    }


    private void AutoAim()
    {
        Enemy closestEnemy = GetClosestEnemy();
        Vector3 tagertVector = Vector3.up;

        if (closestEnemy != null)
        {
            tagertVector = (closestEnemy.transform.position - transform.position).normalized;
            transform.up = tagertVector;
            TryAttack();
        }
        else 
        {
        transform.up = Vector3.Lerp(transform.up, tagertVector, Time.deltaTime * aimLerp);
        }

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

    private void TryAttack()
    {
        if (damageDelay <= attackTimer)
        {
            StartAttack();
            attackTimer = 0f;
        }
    }

    private void incrementAttackTimer()
    {
        attackTimer += Time.deltaTime;
    }


    private void Attacking()
    {
        Attack();
    }
    private void StartAttack()
    {
        animator.speed = damageFrequency;
        animator.Play("Attack");
        state = State.Attack;
    }
    private void StopAttack()
    {
        state = State.Idle;
        damagedEnemies.Clear();
    }

    private void Attack()
    {
        //Collider2D[] enemis = Physics2D.OverlapCircleAll(hitDetectionTransform.position, hitDetectionRadius, enemyMask);
        Collider2D[] enemis = Physics2D.OverlapBoxAll(hitDetectionTransform.position,
                hitBox.bounds.size,
                hitDetectionTransform.localEulerAngles.z,
                enemyMask);
        for (int i = 0; i < enemis.Length; i++)
        {
            Enemy enemy = enemis[i].GetComponent<Enemy>();
            if (!damagedEnemies.Contains(enemy))
            {
                damagedEnemies.Add(enemy);
                enemy.TakeDamage(damage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);


    }
}
