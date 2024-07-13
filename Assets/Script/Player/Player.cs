using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerHealth))]
public class Player : MonoBehaviour
{
    [Header(" Components")]
    private PlayerHealth playerHealth;



    private void Awake()
    {
        playerHealth = GetComponent<PlayerHealth>();

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


}