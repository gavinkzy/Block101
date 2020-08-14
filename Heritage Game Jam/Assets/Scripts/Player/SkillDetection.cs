using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkillDetection : MonoBehaviour
{
    public static SkillDetection skillDetection;
    public Animator PlayerAnimator;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public float knockbackPower = 2f;
    public int knockbackMaxCount = 3;
    public float attackRate = 2f;
    float nextAttackTime = 0f;

    void Update()
    {
        AttackDetection();
    }

    private void AttackDetection()
    {
        if (Time.time >= nextAttackTime)
        {
            //attack
            if (Input.GetKey(KeyCode.Mouse0) && (SceneManager.GetActiveScene().buildIndex == 5 || SceneManager.GetActiveScene().buildIndex == 6))
            {
                PlayerAnimator.SetTrigger("Attack");
                AttackDetection(50);
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }


    }
    private void AttackDetection(int damage)
    {
        //Detect enemies in range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        //Damage enemies in array
        foreach (Collider2D enemy in hitEnemies)
        {
            float knockback;
            if (enemy.transform.position.x < transform.position.x)
            {
                knockback = -knockbackPower;
            }
            else
            {
                knockback = knockbackPower;
            }
            enemy.GetComponent<Enemy>().ReceiveDamage(damage, knockback);
            Debug.Log("Player hit " + enemy.name);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }


}
