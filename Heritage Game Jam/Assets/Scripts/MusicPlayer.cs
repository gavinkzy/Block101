using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer musicPlayer;
    private void Awake()
    {
        if (musicPlayer == null)
        {
            musicPlayer = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DestroyOnSceneThree();
    }

    void DestroyOnSceneThree()
    {
        var currentScene = SceneManager.GetActiveScene().buildIndex;
        if (currentScene == 3)
        {
            Destroy(gameObject);
        }
    }
}
