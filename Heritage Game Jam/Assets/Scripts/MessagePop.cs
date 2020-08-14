using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MessagePop : MonoBehaviour
{
    public TextMeshProUGUI Above;
    public TextMeshProUGUI Bottom;
    public Animator myAboveAnim;
    public Animator myBottomAnim;

    public void MessagePopBottom(string message)
    {
        Bottom.text = message.ToString();
        myBottomAnim.SetTrigger("messagePopBottom");
    }

    public void MessagePopAbove(string message)
    {
        Above.text = message.ToString();
        myAboveAnim.SetTrigger("messagePop");
    }
}
