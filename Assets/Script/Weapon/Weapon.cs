using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Weapon : MonoBehaviour
{
    [Header(" Settings ")]
    [SerializeField]private float range;
    [SerializeField]private LayerMask enemyMask;

    [Header(" Animations ")]
    [SerializeField]private float aimLerp;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        AutoAim();

    }
    private Enemy GetClosestEnemy()
    {
        Enemy closestEnemy = null;
        Collider2D[] enemis = Physics2D.OverlapCircleAll(transform.position, range, enemyMask);
        float minDistance = range;
        for (int i = 0; i < enemis.Length; i++)
        {
            Enemy enemyChecked = enemis[i].GetComponent<Enemy>();
            float distanceToEnemy = Vector2.Distance(enemyChecked.transform.position, transform.position);
            if (minDistance >= distanceToEnemy)
            {
                closestEnemy = enemyChecked;
                minDistance = distanceToEnemy;
            }
        }
        return closestEnemy ;
    }

    private void AutoAim()
    {
        Enemy closestEnemy = GetClosestEnemy();
        Vector3 tagertVector = Vector3.right;

        if (closestEnemy != null)
            tagertVector = (closestEnemy.transform.position - transform.position).normalized;
        transform.up = Vector3.Lerp(transform.up, tagertVector, Time.deltaTime * aimLerp);
    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
