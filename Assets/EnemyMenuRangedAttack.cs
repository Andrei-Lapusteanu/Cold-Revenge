using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMenuRangedAttack : MonoBehaviour
{
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetInteger("EnemyState", 3);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
