using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyMovement : MonoBehaviour
{
    [Header(" Element ")]
    private Player player;

    [Header(" Settings ")]
    [SerializeField] private float speed ;


    // Update is called once per frame
    void Update()
    {
        if (player == null)
            return;
        FollowPlayer();
    }

    public void StorePlayer(Player player)
    {
        this.player = player;   
    }
    private void FollowPlayer()
    {
        Vector2 direction = ((Vector2)player.transform.position - (Vector2)transform.position).normalized;
        Vector2 targetPostion = (Vector2)transform.position + direction * speed * Time.deltaTime;
        transform.position = (Vector3)targetPostion;
    }
}