using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    public Animator myAnim;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        myAnim.SetTrigger("messagePop");
    }
}
