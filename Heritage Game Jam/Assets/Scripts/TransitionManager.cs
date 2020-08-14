using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    private bool hasPlayed = false;
    private AudioSource audioSource;
    public GameObject MessagePopCanvas;
    public Animator UICanvasAnim;
    [TextArea(3, 10)]
    public string message;
    public bool isAbove;
    public bool hasAudio;
    public bool enableScript = false;
    public string cueName;
    public Animator unveilAnim;
    public MobSpawner mobSpawner;
    public bool playerIsRight = false;

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 6)
        {
            PlayerPrefs.DeleteAll();
        }
        
        UICanvasAnim = GameObject.Find("Canvas UI").GetComponent<Animator>();
        mobSpawner = GameObject.Find("Mob Spawner").GetComponent<MobSpawner>();

        if (PlayerPrefs.GetInt("SavedProgress") != 0) 
        {
            unveilAnim = GameObject.Find("Curtain Unveal").GetComponent<Animator>();
            unveilAnim.SetBool("Unveal", true);
            GameObject.Find("Player").transform.position = new Vector2(PlayerPrefs.GetFloat("SpawnX"), 0f);
            UICanvasAnim.SetBool("isEnabled", true);
        }

        if (hasAudio)
        {
            audioSource = GetComponent<AudioSource>();
        }
        MessagePopCanvas = GameObject.Find("Message Pop Canvas");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasPlayed == false)
        {
            if (hasAudio)
            {
                audioSource.Play();
            }

            //Change TMPro message
            if (isAbove)
            {
                MessagePopCanvas.GetComponent<MessagePop>().MessagePopAbove(message);
            }
            else
            {
                MessagePopCanvas.GetComponent<MessagePop>().MessagePopBottom(message);
            }
            if (enableScript == true)
            {
                UniqueScript();
            }
        }
        hasPlayed = true;


    }

    private void UniqueScript()
    {
        var progressInt = 0;
        if (cueName == "UICue")
        {
            UICanvasAnim.SetBool("isEnabled", true);

            Debug.Log("UI Script running.");
            //save progress int.
            progressInt = 1;
            if (PlayerPrefs.GetInt("SavedProgress") < progressInt)
            {
                PlayerPrefs.SetInt("SavedProgress", progressInt);
            }
            if (playerIsRight == false)
            {
                PlayerPrefs.SetFloat("SpawnX", gameObject.transform.position.x - 10f);
            }
            else
            {
                PlayerPrefs.SetFloat("SpawnX", gameObject.transform.position.x + 10f);
            }
        }

        if (cueName == "crux2")
        {
            Debug.Log("Crux2 Script running.");
            //save progress int.
            progressInt = 2;
            if (PlayerPrefs.GetInt("SavedProgress") < progressInt)
            {
                PlayerPrefs.SetInt("SavedProgress", progressInt);
            }
            if (playerIsRight == false)
            {
                PlayerPrefs.SetFloat("SpawnX", gameObject.transform.position.x - 10f);
            }
            else
            {
                PlayerPrefs.SetFloat("SpawnX", gameObject.transform.position.x + 10f);
            }

            //Do Stuff; Instantiate 3 monsters behind
            mobSpawner.spawnMonster(gameObject.transform.position.x - 4f);
            mobSpawner.spawnMonster(gameObject.transform.position.x - 3f);
            mobSpawner.spawnMonster(gameObject.transform.position.x - 2f);
        }

        if (cueName == "crux3")
        {
            Debug.Log("Crux3 Script running.");
            //save progress int.
            progressInt = 3;
            if (PlayerPrefs.GetInt("SavedProgress") < progressInt)
            {
                PlayerPrefs.SetInt("SavedProgress", progressInt);
            }
            if (playerIsRight == false)
            {
                PlayerPrefs.SetFloat("SpawnX", gameObject.transform.position.x - 10f);
            }
            else
            {
                PlayerPrefs.SetFloat("SpawnX", gameObject.transform.position.x + 10f);
            }

            //Do Stuff;
        }

        if (cueName == "nextScene")
        {
            //Do stuff
            SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1));
        }

        if (cueName == "secondUICue")
        {
            UICanvasAnim.SetBool("isEnabled", true);
        }

        if (cueName == "spawnMonsters")
        {
            var spawnNo = Random.Range(3, 6);
            for (int i = 0; i< spawnNo + 1; i++)
            {
                mobSpawner.spawnMonster(gameObject.transform.position.x - (10f - i));
                Debug.Log("Spawn monsters" +i);
            }
            if (playerIsRight == false)
            {
                PlayerPrefs.SetFloat("SpawnX", gameObject.transform.position.x - 2f);
            }
            else
            {
                PlayerPrefs.SetFloat("SpawnX", gameObject.transform.position.x + 0f);
            }
        }

        if (cueName == "UIDisable")
        {
            UICanvasAnim.SetBool("isEnabled", false);
            audioSource.Play();
        }
    }

}
