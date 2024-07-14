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


    public void FollowPlayer()
    {
        Vector2 direction = ((Vector2)player.transform.position - (Vector2)transform.position).normalized;
        Vector2 targetPostion = (Vector2)transform.position + direction * speed * Time.deltaTime;
        transform.position = (Vector3)targetPostion;
    }
    public void AwayPlayer()
    {
        Vector2 direction = ((Vector2)player.transform.position - (Vector2)transform.position).normalized;
        Vector2 targetPostion = (Vector2)transform.position + direction * speed * Time.deltaTime;
        transform.position = -(Vector3)targetPostion;
    }


    public void StorePlayer(Player player)
    {
        this.player = player;
    }
}
