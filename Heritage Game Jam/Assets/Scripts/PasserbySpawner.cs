using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PasserbySpawner : MonoBehaviour
{
    public GameObject[] passerbyPrefabs;
    public bool isSpawning = false;
    public float timerDuration = 5f;
    private float[] XValues = new float[2];

    private void Start()
    {
        XValues[0] = -9f;
        XValues[1] = 9.3f;
    }

    void Update()
    {
        Spawn();
    }

    void Spawn()
    {
        if (!isSpawning)
        {
            var randInteger = Random.Range(0, XValues.Length);
            var randPos = new Vector2(XValues[randInteger], Random.Range(-0.5f, -0.1f));
            var randInt = Random.Range(0, passerbyPrefabs.Length);
            Instantiate(passerbyPrefabs[randInt], randPos, Quaternion.identity);
            isSpawning = true;
            StartCoroutine(ResetTimer(timerDuration));
        }
    }

    IEnumerator ResetTimer(float timerDuration)
    {
        yield return new WaitForSeconds(timerDuration);
        isSpawning = false;
    }
}
