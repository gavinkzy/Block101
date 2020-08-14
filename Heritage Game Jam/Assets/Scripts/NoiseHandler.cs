using System.Collections;
using UnityEngine;

public class NoiseHandler : MonoBehaviour
{
    public AudioClip[] RandomNoises;
    public bool isSpawning = true;
    public float timer = 20f;
    void Start()
    {
        StartCoroutine(ResetTimer(timer));
    }

    // Update is called once per frame
    void Update()
    {
        if (isSpawning == false)
        {
            isSpawning = true;
            timer = Random.Range(30f, 60f);
            AudioSource.PlayClipAtPoint(RandomNoises[Random.Range(0, RandomNoises.Length)], Camera.main.transform.position, 0.3f);
            StartCoroutine(ResetTimer(timer));
        }
    }

    IEnumerator ResetTimer(float timer)
    {
        yield return new WaitForSeconds(timer);
        isSpawning = false;
    }
}
