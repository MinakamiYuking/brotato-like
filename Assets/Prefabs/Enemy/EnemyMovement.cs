using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyMovement : MonoBehaviour
{
    [Header(" Element ")]
    private Player player;


    [Header(" Spawn Sequence Related ")]
    [SerializeField] private SpriteRenderer enemyRenderer ;
    [SerializeField] private SpriteRenderer spawnIndicator ;
    [SerializeField] private float spawnScaleFactor ;
    [SerializeField] private float spawnScaleSpeed ;
    [SerializeField] private int spawnScaleNumberOfTimes ;
    private bool hasBeenSpawn ;


    [Header(" Settings ")]
    [SerializeField] private float speed ;
    [SerializeField] private float playerDetectionRadius;


    [Header(" Effects ")]
    [SerializeField] private ParticleSystem passAwayParticales;


    [Header(" Debug ")]
    [SerializeField] private bool gizmos;



    // Start is called before the first frame update
    void Start()
    {
        player =  FindFirstObjectByType<Player>();
        if (player == null)
        {
            Debug.LogWarning("No Player found, destroy enemy.");
            Destroy(gameObject);
        }
        hasBeenSpawn = false;
        enemyRenderer.enabled = false;
        spawnIndicator.enabled = true;

        Vector3 targetScale = spawnIndicator.transform.localScale * spawnScaleFactor;

        LeanTween.scale(spawnIndicator.gameObject, targetScale, spawnScaleSpeed)
            .setLoopPingPong(spawnScaleNumberOfTimes)
            .setOnComplete(SpawnSequenceComplete);


    }
    private void SpawnSequenceComplete()
    {
        enemyRenderer.enabled = true;
        spawnIndicator.enabled = false;
        hasBeenSpawn = true;
    }



    // Update is called once per frame
    void Update()
    {
        if (!hasBeenSpawn)
            return;
        FollowPlayer();
        Attack();
    }

    private void FollowPlayer()
    {
        Vector2 direction = ((Vector2)player.transform.position - (Vector2)transform.position).normalized;
        Vector2 targetPostion = (Vector2)transform.position + direction * speed * Time.deltaTime;
        transform.position = (Vector3)targetPostion;
    }
    private void Attack()
    {
        float distanceToPlayer = Vector2.Distance((Vector2)player.transform.position, (Vector2)transform.position);
        if (distanceToPlayer <= playerDetectionRadius)
        {
            PassAway();
        }
    }
    private void PassAway()
    {
        passAwayParticales.transform.SetParent(null);
        passAwayParticales.Play();

        Destroy(gameObject);

    }


    private void OnDrawGizmos()
    {
        if (!gizmos) {
            return;
        }

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerDetectionRadius);
    }

}
