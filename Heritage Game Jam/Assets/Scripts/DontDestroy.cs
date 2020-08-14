using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{
    public Canvas canvas;

    public static DontDestroy dontDestroy;
    private void Awake()
    {
        if (dontDestroy == null)
        {
            dontDestroy = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    private void FixedUpdate()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Camera cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        canvas = GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = cam;
    }
}
