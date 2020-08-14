using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float playerMoveSpeed = 5f;
    [Range(0.0f, 1.0f)]
    public float horizontalDamping = 0.01f;
    public float playerJumpHeight = 5f;
    public Rigidbody2D playerRb;
    public LayerMask enemyLayers;
    public BoxCollider2D boxCollider2D;
    public CircleCollider2D circleCollider2D;

    private Vector2 standingColliderSize;
    private Vector2 standingColliderOffset;

    private float jumpAllowance = 0f;
    private float jumpOffAllowance = 0f;
    public float jumpAllowanceSet = 0.2f;
    public float jumpOffAllowanceSet = 0.2f;
    private bool isRunning;
    public float runningBoost = 2f;
    public float energyMeter = 100f;
    public float energyConsumption = 25f;
    public float regeneration = 10f;
    private bool runningEnabled = true;
    private float originalMaxEnergy;
    private bool isNotInCity = true;
    public int unsafeScene = 3;
    public bool allowMovement = true;
    public float maxJump = 1;
    public float currentJump = 1;

    public Animator myAnimator;
    public ParticleSystem dust;
    public AudioSource[] PlayerSounds;
    public FuelBar fuelBar;
    public PlayerStats playerStats;

    void Start()
    {
        originalMaxEnergy = energyMeter;
        standingColliderSize = boxCollider2D.size;
        standingColliderOffset = boxCollider2D.offset;
        fuelBar.SetMaxValue(energyMeter);
        if (SceneManager.GetActiveScene().buildIndex < unsafeScene)
        {
            isNotInCity = false;
        }
        playerStats = GetComponent<PlayerStats>();
    }

    void Update()
    {
        if (allowMovement)
        {
            Movement();
        }
        FlipPlayer();
        fuelBar.SetValue(energyMeter);
    }

    private void Movement()
    {
        //jumpAllowance -= Time.deltaTime;
        //jumpOffAllowance -= Time.deltaTime;
        var playerPressedJump = Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetAxis("Vertical") > 0;

        if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey((KeyCode.A)))
        {
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                if (Input.GetKey(KeyCode.LeftShift) && runningEnabled && isNotInCity)
                {
                    playerRb.velocity = new Vector2(playerMoveSpeed + runningBoost, playerRb.velocity.y);
                    myAnimator.SetBool("Run", true);
                    isRunning = true;
                    Physics2D.IgnoreLayerCollision(11, 16, true);
                    PlayerSounds[0].Stop();
                    PlayerSounds[1].Stop();
                }
                else
                {
                    playerRb.velocity = new Vector2(playerMoveSpeed, playerRb.velocity.y);
                    myAnimator.SetBool("Walk", true);
                    myAnimator.SetBool("Run", false);
                    isRunning = false;
                }
            }
        }

        if (!Input.GetKey(KeyCode.RightArrow) && !Input.GetKey((KeyCode.D)))
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                if (Input.GetKey(KeyCode.LeftShift) && runningEnabled && isNotInCity)
                {
                    playerRb.velocity = new Vector2(-playerMoveSpeed - runningBoost, playerRb.velocity.y);
                    myAnimator.SetBool("Run", true);
                    isRunning = true;
                    Physics2D.IgnoreLayerCollision(11, 16, true);
                    PlayerSounds[0].Stop();
                    PlayerSounds[1].Stop();
                }
                else
                {
                    playerRb.velocity = new Vector2(-playerMoveSpeed, playerRb.velocity.y);
                    myAnimator.SetBool("Walk", true);
                    myAnimator.SetBool("Run", false);
                    isRunning = false;
                }
            }
        }

        //regenerate energy
        if (isRunning)
        {
            energyMeter -= energyConsumption * Time.deltaTime;
            if (energyMeter <= 0)
            {
                energyMeter = 0;
                runningEnabled = false;
            }
        }

        if (energyMeter > 0)
        {
            runningEnabled = true;
        }

        //stop running
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Physics2D.IgnoreLayerCollision(11, 16, false);
            //breath
            PlayPantingIfNotPlaying();
            CheckIfPanting();
        }

        //stop running too
        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            isRunning = false;
            myAnimator.SetBool("Run", false);
        }

        if (playerRb.velocity.x == 0)
        {
            myAnimator.SetBool("Walk", false);
            CreateDust();

            energyMeter += regeneration * Time.deltaTime;
            if (energyMeter >= originalMaxEnergy)
            {
                energyMeter = originalMaxEnergy;
            }
        }

        /*if (playerPressedJump)
        {
            jumpAllowance = jumpAllowanceSet;
        }
        if ((jumpAllowance > 0) && (jumpOffAllowance > 0) && !isRunning)
        {
            jumpAllowance = 0;
            jumpOffAllowance = 0;
            TriggerJump();
        }*/

        if (playerPressedJump && circleCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, playerJumpHeight);
            CreateDust();
            myAnimator.SetBool("Jump", true);
        }

        if (playerRb.velocity.y < 0 && circleCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            myAnimator.SetBool("Jump", false);
        }

        if (playerRb.velocity.y < 0)
        {
            myAnimator.SetBool("Fall", true);
        }

        if (playerRb.velocity.y > 0)
        {
            myAnimator.SetBool("Jump", true);
        }

        /*if (Input.GetAxis("Vertical") < 0)
        {
            boxCollider2D.size = new Vector2(boxCollider2D.size.x, 0.27f);
            boxCollider2D.offset = new Vector2(boxCollider2D.offset.x, -0.35f);
        }
        else
        {
            boxCollider2D.size = standingColliderSize;
            boxCollider2D.offset = standingColliderOffset;
        }*/
        JumpReset();
    }

    private void FlipPlayer()
    {
        if (Input.GetAxis("Horizontal") > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }

        if (Input.GetAxis("Horizontal") < 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }
    }

    private void JumpReset()
    {
        if (circleCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            //jumpOffAllowance = jumpOffAllowanceSet;
            myAnimator.SetBool("Fall", false);
            myAnimator.SetBool("Jump", false);
        }
    }

    void TriggerJump()
    {
        playerRb.velocity = new Vector2(playerRb.velocity.x, playerJumpHeight);
        jumpOffAllowance = 0;
        CreateDust();
        myAnimator.SetBool("Jump", true);
    }

    void CreateDust()
    {
        dust.Play();
    }

    public void EnableRunning()
    {
        runningEnabled = true;
        // play skill grant anim
    }

    public void PlayPantingIfNotPlaying()
    {
        if (isNotInCity)
        {
            if (PlayerSounds[0].isPlaying)
            {
                return;
            }
            else
            {
                PlayerSounds[0].Play();
            }
        }
    }

    public void CheckIfPanting()
    {
        if (isNotInCity)
        {
            if (PlayerSounds[0].isPlaying)
            {
                PlayerSounds[1].Stop();
            }
            else
            {
                PlayerSounds[1].Play();
            }
        }
    }
}
