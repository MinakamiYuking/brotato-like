using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class DamageText : MonoBehaviour
{

    [SerializeField]private Animator animator;
    [SerializeField]private TextMeshPro damageText;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayDamageTextAnimation(int damage)
    {
        damageText.text = damage.ToString();
        animator.Play("Damage Text Animation");
    }


}
