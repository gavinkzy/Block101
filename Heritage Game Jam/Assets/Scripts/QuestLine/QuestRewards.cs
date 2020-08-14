using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestRewards : ScriptableObject
{
    public void BlackHole()
    {
        Debug.Log("Player falls into blackhole.");
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1));
        PlayerPrefs.SetInt("isMonsterKilled", 0);
    }

    public void MoveToNextScene()
    {
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void MoveToLastScene()
    {
        SceneManager.LoadScene((6));
    }
}
