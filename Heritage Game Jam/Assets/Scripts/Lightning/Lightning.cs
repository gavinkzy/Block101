using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    public float secondsToDestroy = 5f;
    // Update is called once per frame

    private void Start()
    {
        StartCoroutine(DestroyLightning(secondsToDestroy));
    }

    IEnumerator DestroyLightning(float secondsToDestroy)
    {
        yield return new WaitForSeconds(secondsToDestroy);
        Destroy(gameObject);
    }
}
