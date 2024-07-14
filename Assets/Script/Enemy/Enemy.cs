using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyMovement),typeof(CircleCollider2D))]
public abstract class Enemy : MonoBehaviour
{
    [Header(" Elements ")]
    protected EnemyMovement movement;
    protected Player player;
    protected CircleCollider2D enemyCollider;
    [Header(" Settings ")]
    [SerializeField] protected float playerDetectionRadius;
    [SerializeField] protected int maxHealth;
    protected int health;
    [Header(" Spawn Sequence Related ")]
    [SerializeField] protected SpriteRenderer enemyRenderer;
    [SerializeField] protected SpriteRenderer spawnIndicator;
    [SerializeField] protected float spawnScaleFactor;
    [SerializeField] protected float spawnScaleSpeed;
    [SerializeField] protected int spawnScaleNumberOfTimes;
    [Header(" Effects ")]
    [SerializeField] protected ParticleSystem passAwayParticales;
    [Header(" Action ")]
    public static Action<int, Vector3> onDamageTaken;
    [Header(" Debug ")]
    [SerializeField] protected bool gizmos;

    protected virtual void Start()
    {
        GetPlayer();
        movement = GetComponent<EnemyMovement>();
        movement.StorePlayer(player);
        enemyCollider = GetComponent<CircleCollider2D>();
        health = maxHealth;
        StartSpawnSequence();
    }
    protected bool hasSpawned()
    {
        return enemyRenderer.enabled;
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
        enemyCollider.enabled = visible;
        enemyRenderer.enabled = visible;
        spawnIndicator.enabled = !visible;
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




}
