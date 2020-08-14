using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public SceneManage sceneManage;
    public bool isRightPortal;
    public DialogueTrigger dialogueTrigger;

    private void Start()
    {
        sceneManage = GameObject.Find("Scene Manage").GetComponent<SceneManage>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name + " has hit the portal.");
        if (collision.GetComponent<Collider2D>().gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (isRightPortal)
            {
                dialogueTrigger.ManualTriggerDialogue();
                PlayerPrefs.DeleteAll();
            }
            else
            {
                sceneManage.LoadPreviousScene();
            }
        }
    }

}
