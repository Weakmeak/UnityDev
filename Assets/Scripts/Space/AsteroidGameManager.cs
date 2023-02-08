using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AsteroidGameManager : MonoBehaviour
{
    [SerializeField]private TMP_Text scoreUI;
    [SerializeField] private GameObject gameOverUI;

    int score = 0;

    public void addPoints(int points)
    {
        score+= points;
        scoreUI.text = "Score: " + score;
    }

    public void SetGameOver()
    {
        gameOverUI.SetActive(true);
    }
}
