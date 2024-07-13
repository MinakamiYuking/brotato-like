using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header(" Components ")]
    private EnemyMovement movement;

    [Header(" Element ")]
    private Player player;

    [Header(" Settings ")]
    [SerializeField] private int maxHealth;
    private int health;

    [Header(" Spawn Sequence Related ")]
    [SerializeField] private SpriteRenderer enemyRenderer;
    [SerializeField] private SpriteRenderer spawnIndicator;
    [SerializeField] private float spawnScaleFactor;
    [SerializeField] private float spawnScaleSpeed;
    [SerializeField] private int spawnScaleNumberOfTimes;
    private bool hasBeenSpawn;


    [Header(" Effects ")]
    [SerializeField] private ParticleSystem passAwayParticales;

    [Header(" Attack ")]
    [SerializeField] private int damage;
    [SerializeField] private int attackFrequency;
    [SerializeField] private float playerDetectionRadius;
    private float attackDelay;
    private float attackTimer;

    [Header(" Debug ")]
    [SerializeField] private bool gizmos;



    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<EnemyMovement>();
        health = maxHealth;
        StartGetPlayer();
        StartSpawnSequence();
        StartSetAttack();
    }

    private void StartGetPlayer()
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
        hasBeenSpawn = false;
        SetRendererVisibility(false);
        Vector3 targetScale = spawnIndicator.transform.localScale * spawnScaleFactor;
        LeanTween.scale(spawnIndicator.gameObject, targetScale, spawnScaleSpeed)
            .setLoopPingPong(spawnScaleNumberOfTimes)
            .setOnComplete(SpawnSequenceComplete);
    }

    private void SpawnSequenceComplete()
    {
        SetRendererVisibility(true);
        hasBeenSpawn = true;
        movement.StorePlayer(player);
    }

    private void SetRendererVisibility(bool visible)
    {
        enemyRenderer.enabled = visible;
        spawnIndicator.enabled = !visible;
    }


    private void StartSetAttack()
    {
        attackDelay = 1f / attackFrequency;
        attackTimer = 0f; 
    }



    // Update is called once per frame
    void Update()
    {
        if (!hasBeenSpawn)
            return;
        if (attackTimer >= attackDelay)
            TryAttack();
        else
            WaitForAttack();
    }


    private void TryAttack()
    {
        float distanceToPlayer = Vector2.Distance((Vector2)player.transform.position, (Vector2)transform.position);
        if (distanceToPlayer <= playerDetectionRadius)
        {
            Attack();
        }
    }
    private void Attack()
    {
        player.TakeDamage(damage);
        attackTimer = 0f;
    }
    private void WaitForAttack()
    {
        attackTimer += Time.deltaTime;
    }

    public void TakeDamage(int damage)
    {
        int realDamage = Mathf.Min(damage, health);
        health -= realDamage;
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
