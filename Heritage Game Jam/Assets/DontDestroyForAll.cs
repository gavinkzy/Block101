using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyForAll : MonoBehaviour
{
    public static DontDestroyForAll dontDestroyForAll;
    // Start is called before the first frame update
    private void Awake()
    {
        if (dontDestroyForAll == null)
        {
            dontDestroyForAll = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex > 1 && SceneManager.GetActiveScene().buildIndex != 6)
        {
            Destroy(gameObject);
        }
    }
}
