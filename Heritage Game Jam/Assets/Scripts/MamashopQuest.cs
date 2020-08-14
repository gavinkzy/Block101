using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MamashopQuest : MonoBehaviour
{
    public GameObject dialogue1;
    public GameObject dialogue2;

    // Start is called before the first frame update
    void Start()
    {
        dialogue1 = GameObject.Find("Dialogue1");
        dialogue2 = GameObject.Find("Dialogue2");
    }

    // Update is called once per frame
    void Update()
    {
        
        if (SceneManager.GetActiveScene().buildIndex == 4){

            if (PlayerPrefs.GetInt("isMonsterKilled") == 1)
            {
                dialogue1.SetActive(false);
                dialogue2.SetActive(true);
            }

            if (PlayerPrefs.GetInt("isMonsterKilled") == 0)
            {
                dialogue1.SetActive(true);
                dialogue2.SetActive(false);
            }
        }

    }
}
