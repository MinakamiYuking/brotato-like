using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class MeleeWeapon : Weapon
{
    protected enum State
    {
        Idle,
        Attack
    }
    protected State state;
    [Header(" Elements ")]
    [SerializeField] protected Transform hitDetectionTransform;
    [SerializeField] private BoxCollider2D hitBox;
    [SerializeField] protected Animator animator;
    protected List<Enemy> damagedEnemies = new List<Enemy>();

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        state = State.Idle;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        WaitForAttack();
        switch (state)
        {
            case State.Idle:
                if(AutoAimAndHasEnemyInRange())
                    StartAttack();
                break;
            case State.Attack:
                Attacking();
                break;
        }
    }

    private void StartAttack()
    {
        state = State.Attack;
        animator.speed = damageFrequency;
        animator.Play("Attack");
    }
    private void StopAttack()
    {
        damagedEnemies.Clear();
        state = State.Idle;
    }
    private void Attacking()
    {
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

}
