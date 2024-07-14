using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class RangeEnemyAttack : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private GameObject bulletPrefab;
    private Player player;


    [Header(" Sttings ")]
    [SerializeField] private int damage;
    [SerializeField] private int attackFrequency;
    private float attackDelay;
    private float attackTimer;
    private ObjectPool<>
    void Start()
    {
        StartSetAttack();
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
    }

    public void StorePlayer(Player player)
    {
        this.player = player;
    }




}
