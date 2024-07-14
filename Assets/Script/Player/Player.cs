using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerHealth),typeof(Collider2D))]
public class Player : MonoBehaviour
{
    [Header(" Components")]
    private PlayerHealth playerHealth;
    private Collider2D playerCollider;



    private void Awake()
    {
        playerHealth = GetComponent<PlayerHealth>();
        playerCollider = GetComponent<CircleCollider2D>();
    }
    void Start()
    {
    }

    void Update()
    {
        
    }
    public void TakeDamage(int damage)
    {
        playerHealth.TakeDamage(damage);
    }

    public Vector2 GetCenter()
    { 
        return (Vector2)transform.position + playerCollider.offset ;
    }

}
