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

    // Start is called before the first frame update
    void Start()
    {
        player =  FindFirstObjectByType<Player>();
        if (player == null)
        {
            Debug.LogWarning("No Player found, destroy enemy.");
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = ((Vector2)player.transform.position - (Vector2)transform.position).normalized ;
        Vector2 targetPostion = (Vector2)transform.position + direction * speed * Time.deltaTime ;
        transform.position = (Vector3)targetPostion;
    }
}
