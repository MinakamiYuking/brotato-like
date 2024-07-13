using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTextManager : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private DamageText DamageTextPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [NaughtyAttributes.Button]
    public void InstantiateDamageText()
    {
        Vector3 spawnPosition = Random.insideUnitCircle * Random.Range(1f, 3f);
        DamageText DamageTextInstantiate = Instantiate(DamageTextPrefab, spawnPosition, Quaternion.identity, transform);
        DamageTextInstantiate.PlayDamageTextAnimation();

    }

}
