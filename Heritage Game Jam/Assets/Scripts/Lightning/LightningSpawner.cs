using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;
using UnityEngine.Rendering.Universal;

public class LightningSpawner : MonoBehaviour
{
    public bool isSpawning = false;
    public float coolDown = 10f;
    private Vector2 spawnPos;
    public GameObject Player;
    public GameObject[] LightningPrefab;
    public Animator myAnim;
    public AudioClip[] thunderSounds;
    public float thunderInSeconds = 0.5f;

    // Update is called once per frame
    void Update()
    {
        if (isSpawning == false)
        {
            RandomizeSpawnInterval();
            Debug.Log("Lightning process.");
            isSpawning = true;
            //Randomize lightning spawn pos according to player pos
            RandomizeSpawnPos();
            //Instantiate lightning with default entry anim (appear and disappear)
            Debug.Log("instantiate at " + spawnPos);
            Instantiate(LightningPrefab[Random.Range(0, LightningPrefab.Length)], spawnPos, Quaternion.identity);
            //2d Light anim
            myAnim.SetTrigger("Lightning");
            //cue thunder sound x seconds after.
            thunderInSeconds = Random.Range(0.3f, 0.6f);
            StartCoroutine(ThunderCue(thunderInSeconds));
            StartCoroutine(ResetSpawning(coolDown));
        }
    }

    IEnumerator ResetSpawning(float coolDown)
    {
        yield return new WaitForSeconds(coolDown);
        isSpawning = false;
    }

    void RandomizeSpawnPos()
    {
        var randX = Random.Range(Player.transform.position.x - 8f, Player.transform.position.x + 8f);
        var randY = Random.Range(Player.transform.position.y + 4.5f, Player.transform.position.y + 4.7f);
        spawnPos = new Vector2(randX, randY);
    }

    void RandomizeSpawnInterval()
    {
        coolDown = Random.Range(3f, 10f);
    }

    IEnumerator ThunderCue(float thunderInSeconds)
    {
        yield return new WaitForSeconds(thunderInSeconds);
        AudioSource.PlayClipAtPoint(thunderSounds[Random.Range(0,thunderSounds.Length)], spawnPos, Random.Range(1f,2f));
    }

}
