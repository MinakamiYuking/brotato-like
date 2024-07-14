using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class DamageText : MonoBehaviour
{

    [SerializeField]private Animator animator;
    [SerializeField]private TextMeshPro damageText;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayDamageTextAnimation(int damage)
    {
        damageText.text = damage.ToString();
        animator.Play("Damage Text Animation");
    }


}
