using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public Image npcImg;
    public Animator animator;
    public TextMeshProUGUI actionText;
    private GameObject actionButton;
    public AudioSource audioSource;
    public TextMeshProUGUI dismissText;
    private string questName;
    public Player player;

    public static DialogueManager dialogueManager;
    private void Awake()
    {
        if (dialogueManager == null)
        {
            dialogueManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }
    void Start()
    {
        sentences = new Queue<string>();
        actionButton = GameObject.Find("Action Button");
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    private void FixedUpdate()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    public void StartDialogue(Dialogue dialogue, Sprite npcSprite)
    {
        if (dialogue.action == "")
        {
            actionButton.SetActive(false);
        }
        else
        {
            actionText.text = dialogue.action.ToString();
        }

        player.allowMovement = false;
        questName = dialogue.questName;

        sentences.Clear();

        animator.SetBool("IsOpen", true);
        nameText.text = dialogue.name;

        npcImg.sprite = npcSprite;
        
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 1)
        {
            actionButton.SetActive(true);
        }

        if (sentences.Count > 1)
        {
            actionButton.SetActive(false);
            dismissText.text = "Next";
        }
        else
        {
            dismissText.text = "Dismiss";
        }

        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;

        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.01f);
        }
    }

    void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
        player.allowMovement = true;
        //audioSource.Play();
    }

    public void ExecuteAction()
    {
        DisplayNextSentence();
        QuestLine();
    }

    public void QuestLine()
    {
        var questRewards = ScriptableObject.CreateInstance<QuestRewards>();
        if (questName == "BlackHoleQuest")
        {
            questRewards.BlackHole();
        }

        if (questName == "FirstScenePortal")
        {
            questRewards.MoveToNextScene();
        }

        if (questName == "answerQuestion")
        {
            GameObject.Find("Questionaire").transform.Find("Image").GetComponent<Animator>().SetBool("isVisible", true);
        }

        if (questName == "mamashop")
        {
            questRewards.MoveToNextScene();
        }

        if (questName == "moveToLastScene")
        {
            questRewards.MoveToLastScene();
        }
    }
}
