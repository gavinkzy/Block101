using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public int healthCurrent;
    public int healthMax = 10000;
    public Rigidbody2D rb;
    public Animator myAnim;
    public Animator deathAnim;
    public HealthBar healthBar;
    public Player player;
    public float stunDuration = 0.5f;
    public float secondsBeforeRestart = 5f;

    private void Start()
    {
        healthCurrent = healthMax;
        healthBar.SetMaxValue(healthMax);
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.SetValue(healthCurrent);
        CheckIfPlayerDied();
    }

    void CheckIfPlayerDied()
    {
        if (healthCurrent <= 0)
        {
            deathAnim.SetBool("deathScreen", true);
            //enable restart scene
            StartCoroutine(RestartSceneAfterDeath(secondsBeforeRestart));
        }
    }

    public void AuraDamage(int damage)
    {
        healthCurrent -= damage;
    }

    public void GrantDamage(int damage, bool playerOnLeft)
    {
        healthCurrent -= damage;
        myAnim.SetTrigger("Hurt");
        StartCoroutine(KnockbackPlayer(stunDuration, playerOnLeft));
    }

    private IEnumerator KnockbackPlayer(float stunDuration, bool playerOnLeft)
    {
        player.allowMovement = false;
        if (playerOnLeft)
        {
            rb.velocity = new Vector2(-4f, 1f);
        }
        else
        {
            rb.velocity = new Vector2(4f, 1f);
        }
        yield return new WaitForSeconds(stunDuration);
        player.allowMovement = true;
    }

    private IEnumerator RestartSceneAfterDeath(float secondsBeforeRestart)
    {
        yield return new WaitForSeconds(secondsBeforeRestart);
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex));
    }
}
