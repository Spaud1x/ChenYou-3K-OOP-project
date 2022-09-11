using System.Collections;
using UnityEngine;

public class TargetPractice : MonoBehaviour
{
    public Animator animator;
    private bool hitTarget = false;

    public float health = 10f;
    public float healthReset = 10f;  //ensures all targets have same health after it falls and resets

    void OnEnable()
    {
        hitTarget = false;
        animator.SetBool("isHit", false);
    }

    public void Hit(float amount)
    {
        health -= amount;

        if(health <= 0)
        {
            health = healthReset;
            StartCoroutine(Fall());
        }
    }

    IEnumerator Fall()
    {
        hitTarget = true;
        animator.SetBool("isHit", true);

        yield return new WaitForSeconds(1f);

        hitTarget = false;
        animator.SetBool("isHit", false);
    }

}
