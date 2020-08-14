using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{

    private int npcLayer;
    public Dialogue dialogue;
    public Animator myAnim;

    private void Start()
    {
        npcLayer = LayerMask.NameToLayer("NPC");
        myAnim = GameObject.Find("Dialogue Canvas").GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if (hit.collider != null && hit.transform.gameObject.layer == npcLayer && myAnim.GetBool("IsOpen") == false)
            {
                var myNPC = hit.collider.gameObject;
                TriggerDialogue(myNPC.GetComponent<DialogueTrigger>().dialogue, myNPC.GetComponent<SpriteRenderer>().sprite);
                Debug.Log("NPC Trigger.");
            }
        }
    }

    public void TriggerDialogue(Dialogue dialogue, Sprite npcSprite)
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue, npcSprite);
    }

    public void ManualTriggerDialogue()
    {
        if (myAnim.GetBool("IsOpen") == false)
        {
            var myNPC = gameObject;
            TriggerDialogue(myNPC.GetComponent<DialogueTrigger>().dialogue, myNPC.GetComponent<SpriteRenderer>().sprite);
            Debug.Log("NPC Trigger.");
        }
    }

}
