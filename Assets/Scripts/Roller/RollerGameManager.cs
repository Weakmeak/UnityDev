using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RollerGameManager : Singleton<RollerGameManager>
{
    [SerializeField] Slider healthBar;
    [SerializeField] TMP_Text Score;

    [SerializeField] GameObject PlayerPrefab;
    [SerializeField] Transform SpawnPoint;

    [SerializeField] GameObject GameOverUI;
    [SerializeField] GameObject TitleUI;
    [SerializeField] GameObject WinUI;
    //private int score = 0;

    [Header("Events")]
    [SerializeField] EventRouter startGameEvent;
    [SerializeField] EventRouter stopGameEvent;
    [SerializeField] EventRouter winGameEvent;


    public enum State
    {
        Title,
        StartGame,
        PlayGame,
        GameOver,
        GameWin
    }
    State state = State.Title;
    float stateTimer = 0;

    private void Update()
    {
        switch (state)
        {
            case State.Title:
                GameOverUI.SetActive(false);
                WinUI.SetActive(false);
                TitleUI.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;
            case State.StartGame:
                startGameEvent.Notify();
                TitleUI.SetActive(false);
                GameOverUI.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Instantiate(PlayerPrefab, SpawnPoint.position, SpawnPoint.rotation);
                state = State.PlayGame;
                break;
            case State.GameOver:
                //Debug.Log(stateTimer);
                stateTimer -= Time.deltaTime;
                if (stateTimer < 0)
                {
                    state = State.StartGame;
                }
                break;
            default:
                break;
            
        }
    }

    public void SetGameWin()
    {
        //Debug.Log("Win!!!");
        state = State.GameWin;
        WinUI.SetActive(true);
    }


    private void Start()
    {
        winGameEvent.onEvent += SetGameWin;
    }

    public void SetHealth(int health)
    {
        healthBar.value = Mathf.Clamp(health, 0 ,100);
    }

    public void SetScore(int score)
    {
        //this.score = score;
        Score.text = "Score: " + score;
    }

    public void setGameOver()
    {
        if (state == State.GameWin) return;
        stopGameEvent.Notify();
        GameOverUI.SetActive(true);
        state = State.GameOver;
        stateTimer = 3;
    }

    public void OnStartGame()
    {
        state = State.StartGame;
    }
}
