using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    public GameObject monsterPrefab;
    public void spawnMonster(float spawnX)
    {
        Instantiate(monsterPrefab, new Vector2(spawnX, 0.1f), Quaternion.identity);
        Debug.Log("Spawned");
    }
}
