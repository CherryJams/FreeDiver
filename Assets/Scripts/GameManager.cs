using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private bool isGameActive = false;
    private bool isPlayerUnderwater = false;
    private Leaderboard leaderboard;

    private void OnEnable()
    {
        leaderboard = FindObjectOfType<Leaderboard>();
        
    }

    public void StartGame()
    {
    }

    public void WaitAndStartLevel()
    {
        StartCoroutine(waitAndStartLevel());
    }

    IEnumerator waitAndStartLevel()
    {
        yield return new WaitForSeconds(0.1f);
        StartGame();
    }

    public void EndGame()
    {
        var score = FindObjectOfType<DepthGauge>();
        StartCoroutine(leaderboard.SubmitScoreRoutine(score.GetDepth()));
        CanvasManager.GetInstance().SwitchCanvas(CanvasType.GameOverScreen);
        StartCoroutine(leaderboard.FetchTopHighscoresRoutine());
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Victory()
    {
        CanvasManager.GetInstance().SwitchCanvas(CanvasType.EndScreen);
    }

    public void ChangeScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void StartBurnPhase()
    {
    }

    public bool IsGameActive()
    {
        return isGameActive;
    }
    public bool IsPlayerUnderwater()
    {
        return isPlayerUnderwater;
    }

    public void SetGameActive(bool gameActive)
    {
        isGameActive = gameActive;
    }
}