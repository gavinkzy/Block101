using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class EndCue : MonoBehaviour
{
    public TextMeshProUGUI messageText;
    [TextArea(3, 10)]
    public string[] sentences;
    public Animator canvasAnimator;
    public bool hasPlayed = false;
    private bool canSwitch = false;
    public int currentInt = 0;

    // Start is called before the first frame update
    void Start()
    {
        messageText.text = sentences[currentInt].ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (canSwitch == true && currentInt != sentences.Length)
        {
            canSwitch = false;
            currentInt += 1;
            if (currentInt < sentences.Length)
            {
                messageText.text = sentences[currentInt].ToString();
            }
            canvasAnimator.SetBool("isPlaying", true);
            StartCoroutine(PlayMessages());
        }

        if (currentInt == sentences.Length)
        {
            canvasAnimator.SetBool("isPlaying", false);
            canSwitch = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasPlayed == false)
        {
            canvasAnimator.SetBool("isPlaying", true);
            hasPlayed = true;
            StartCoroutine(PlayMessages());
        }

        StartCoroutine(GoToMainMenu());
    }

    IEnumerator PlayMessages()
    {
        yield return new WaitForSeconds(10f);
        canvasAnimator.SetBool("isPlaying", false);
        canSwitch = true;
    }

    IEnumerator GoToMainMenu()
    {
        yield return new WaitForSeconds(100f);
        SceneManager.LoadScene(0);
    }
}
