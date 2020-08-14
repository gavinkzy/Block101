using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RainCue : MonoBehaviour
{
    public GameObject Rain;
    private ParticleSystem rainPS;
    private AudioSource rainAS;
    public TextMeshProUGUI messageText;
    [TextArea(3, 10)]
    public string message = "First String";
    public Animator myAnim;

    // Start is called before the first frame update
    void Start()
    {
        rainPS = Rain.GetComponent<ParticleSystem>();
        rainAS = Rain.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        rainPS.Play();
        StartCoroutine(PlayRainAudio());
        messageText.text = message.ToString();
        myAnim.SetTrigger("messagePopBottom");
        Debug.Log("Pop Bottom");
    }

    IEnumerator PlayRainAudio()
    {

        yield return new WaitForSeconds(3f);
        rainAS.Play();
    }
}
