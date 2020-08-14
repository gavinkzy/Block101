using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MonsterHealth : MonoBehaviour
{
    public Enemy monster;
    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        monster = GameObject.Find("Monster").GetComponent<Enemy>();
        healthBar.SetMaxValue(monster.health);

    }

    // Update is called once per frame
    void Update()
    {
        healthBar.SetValue(monster.health);
    }
}
