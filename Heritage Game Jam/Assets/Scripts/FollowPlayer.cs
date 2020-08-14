using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject Player;


    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(Player.transform.position.x, transform.position.y);
    }
}
