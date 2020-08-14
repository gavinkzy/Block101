using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public float speed;
    private Transform target;
    private Rigidbody2D rb;
    public int health = 100;
    public Animator myAnim;
    public float stunDuration = 0.2f;
    private bool isAggro = false;
    public float evasionTime = 3f;
    private float evasionTimer;
    public AudioSource scaryMusic;
    public AudioClip slapSound;
    private PlayerStats playerStats;
    public int damage = 10;
    public float knockbackPower = 10f;
    public float auraDamageStopwatch = 3f;
    public bool auraEnabled = true;
    private int currentScene;
    public bool isDoingQuest = true;
    public GameObject deathPS;

    void Start()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        evasionTimer = evasionTime;
        Physics2D.IgnoreLayerCollision(11, 16, false);
    }

    void Update()
    {
        if (currentScene != 3)
        {
            auraEnabled = false;
        }

        if (currentScene == 6)
        {
            isDoingQuest = false;
        }

        ToggleScaryMusic();
        evasionTimer -= Time.deltaTime;
        auraDamageStopwatch -= Time.deltaTime;

        if (evasionTimer <= 0f)
        {
            isAggro = false;
        }

        if (Vector2.Distance(transform.position, target.position) < 5f)
        {
            isAggro = true;
            evasionTimer = evasionTime;
        }

        if (isAggro)
        {
            if (scaryMusic.volume < 0.4f)
            {
                scaryMusic.volume += 0.1f * Time.deltaTime;
            }
            if (auraEnabled == true)
            {
                playerStats.AuraDamage(1);
            }
        }

        if (health <= 0)
        {
            Debug.Log("Enemy died");
            //play enemy die anim
            myAnim.SetBool("isDead", true);
            isAggro = false;
            Instantiate(deathPS, transform.position, Quaternion.identity);
            if (currentScene == 3 || currentScene == 5)
            {
                Physics2D.IgnoreLayerCollision(11, 16, true);
            }
            else
            {
                Destroy(gameObject);
            }
            //Start coroutine to transport back to safe house
            if (isDoingQuest == true)
            {
                StartCoroutine(TransportBackToSafehouse());
            }
            //Set PlayerPref to indicate that monster is killed
            PlayerPrefs.SetInt("isMonsterKilled", 1);
        }
        FlipSprite();
        if (Vector2.Distance(transform.position, target.position) > 0.5f && myAnim.GetBool("isTakingDamage") == false && isAggro)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            myAnim.SetBool("Walk", true);
        }

        FlipSprite();
    }

    private void FlipSprite()
    {
        if (transform.position.x < target.position.x)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }

        if (transform.position.x > target.position.x)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }
    }

    public void ReceiveDamage(int damage, float knockback)
    {
        health -= damage;
        rb.velocity = new Vector2(knockback, rb.velocity.y);
        StartCoroutine(TakeDamage(stunDuration));
    }

    IEnumerator TakeDamage(float stunDuration)
    {
        myAnim.SetBool("isTakingDamage", true);
        AudioSource.PlayClipAtPoint(slapSound, gameObject.transform.position, 2f);
        yield return new WaitForSeconds(stunDuration);
        myAnim.SetBool("isTakingDamage", false);
    }

    void ToggleScaryMusic()
    {
        if (isAggro)
        {
            if (scaryMusic.isPlaying)
            {
                return;
            }
            else
            {
                scaryMusic.Play();
            }
        }

        else if (!isAggro)
        {
            if (!scaryMusic.isPlaying)
            {
                return;
            }
            else
            {
                scaryMusic.Stop();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            bool playerOnLeft = false;
            if (collision.transform.position.x < gameObject.transform.position.x)
            {
                //player on the left
                playerOnLeft = true;
            }
            playerStats.GrantDamage(damage, playerOnLeft);
        }
    }

    IEnumerator TransportBackToSafehouse()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(4);
    }

    IEnumerator DeathAnim()
    {
        
        yield return new WaitForSeconds(0.5f);
    }
}
