using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class DamageTextManager : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private DamageText damageTextPrefab;

    [Header(" Pool ")]
    private ObjectPool<DamageText> damageTextPool;

    // Start is called before the first frame update
    void Start()
    {
        Enemy.onDamageTaken += InstantiateDamageText;

        damageTextPool = new ObjectPool<DamageText>(CreateFunc,ActionOnGet,ActionOnRelease,ActionOnDestroy);
    }

    private DamageText CreateFunc() { return Instantiate(damageTextPrefab,transform); }
    private void ActionOnGet(DamageText damageText) 
    { 
        damageText.gameObject.SetActive(true);
    }
    private void ActionOnRelease(DamageText damageText) 
    {
        damageText.gameObject.SetActive(false);
    }
    private void ActionOnDestroy(DamageText damageText)
    {
        Destroy(damageText.gameObject);
    }




    // Update is called once per frame
    void Update()
    {
        
    }

    public void InstantiateDamageText(int damage,Vector3 enemyPos)
    {
        DamageText DamageTextInstantiate = damageTextPool.Get();
        DamageTextInstantiate.transform.position = enemyPos + Vector3.up * 0.7f ;
        DamageTextInstantiate.PlayDamageTextAnimation(damage);
        LeanTween.delayedCall(1, () => damageTextPool.Release(DamageTextInstantiate));
    }
    public void OnDestroy()
    {
        Enemy.onDamageTaken -= InstantiateDamageText;
    }
}
