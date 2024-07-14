using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyMovement), typeof(RangeEnemyAttack))]
public class RangeEnemy : MonoBehaviour
{
    [Header(" Components ")]
    private CircleCollider2D enemyCollider;

    [Header(" Elements ")]
    private Player player;
    private EnemyMovement movement;
    private RangeEnemyAttack enemyAttack;

    [Header(" Settings ")]
    [SerializeField] private float playerDetectionRadius;
    [SerializeField] private int maxHealth;
    private int health;

    [Header(" Spawn Sequence Related ")]
    [SerializeField] private SpriteRenderer enemyRenderer;
    [SerializeField] private SpriteRenderer spawnIndicator;
    [SerializeField] private float spawnScaleFactor;
    [SerializeField] private float spawnScaleSpeed;
    [SerializeField] private int spawnScaleNumberOfTimes;

    [Header(" Effects ")]
    [SerializeField] private ParticleSystem passAwayParticales;

    [Header(" Action ")]
    public static Action<int, Vector3> onDamageTaken;

    [Header(" Debug ")]
    [SerializeField] private bool gizmos;

    private void Awake()
    {
        GetPlayer();
        movement = GetComponent<EnemyMovement>();
        movement.StorePlayer(player);
        enemyAttack = GetComponent<RangeEnemyAttack>();
        enemyAttack.StorePlayer(player);

        enemyCollider = GetComponent<CircleCollider2D>();
    }
    private void GetPlayer()
    {
        player = FindFirstObjectByType<Player>();
        if (player == null)
        {
            Debug.LogWarning("No Player found, destroy enemy.");
            Destroy(gameObject);
        }
    }

    void Start()
    {
        health = maxHealth;
        StartSpawnSequence();
    }

    private void StartSpawnSequence()
    {
        SetRendererVisibility(false);
        Vector3 targetScale = spawnIndicator.transform.localScale * spawnScaleFactor;
        LeanTween.scale(spawnIndicator.gameObject, targetScale, spawnScaleSpeed)
            .setLoopPingPong(spawnScaleNumberOfTimes)
            .setOnComplete(SpawnSequenceComplete);
    }

    private void SpawnSequenceComplete()
    {
        SetRendererVisibility(true);
    }

    private void SetRendererVisibility(bool visible)
    {
        spawnIndicator.enabled = !visible;
        enemyRenderer.enabled = visible;
        enemyCollider.enabled = visible;

    }


    void Update()
    {
        if (!enemyRenderer.enabled)
            return;
        float distanceToPlayer = Vector2.Distance((Vector2)player.transform.position, (Vector2)transform.position);
        if (distanceToPlayer > playerDetectionRadius)
            movement.FollowPlayer();
        else
            enemyAttack.TryAttack();
    }

    public void TakeDamage(int damage)
    {
        int realDamage = Mathf.Min(damage, health);
        health -= realDamage;
        onDamageTaken?.Invoke(damage, transform.position);
        if (health <= 0)
            PassAway();
    }

    private void PassAway()
    {
        passAwayParticales.transform.SetParent(null);
        passAwayParticales.Play();
        Destroy(gameObject);
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
