using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private Animator playerAnimator;
    private AudioManager audioManager;
    private AudioSource currentAudioSource;
    private bool isGameActive = true;
    private bool isPlayerUnderwater = false;
    private Leaderboard leaderboard;
    private DepthGauge depthGauge;
    private GameObject player;
    [SerializeField] private float slowdownFactor = 0.5f;
    [SerializeField] private float slowdownTime = 1f;
    [SerializeField] private float stopTime = 0.5f;
    private Vector3 initialPlayerPosition;
    private Level level;

    private void OnEnable()
    {
        level = FindObjectOfType<Level>();
        player = GameObject.FindWithTag("Player");
        initialPlayerPosition = player.transform.position;
        leaderboard = FindObjectOfType<Leaderboard>();
        depthGauge = FindObjectOfType<DepthGauge>();
        audioManager = AudioManager.GetInstance();
        playerAnimator = GameObject.FindWithTag("PlayerAnimator").GetComponent<Animator>();
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
        StartCoroutine(EndGameCoroutine());
    }

    private IEnumerator EndGameCoroutine()
    {
        SetGameActive(false);
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        yield return Stop();
        yield return ApplySlowMotionEffect();
        yield return new WaitForSecondsRealtime(1f);
        StartCoroutine(leaderboard.SubmitScoreRoutine(depthGauge.GetMaxDepth()));
        CanvasManager.GetInstance().SwitchCanvas(CanvasType.GameOverScreen);
        StartCoroutine(leaderboard.FetchTopHighscoresRoutine());
    }

    public void RestartGame()
    {
        player.transform.position = initialPlayerPosition;
        SetGameActive(true);
        level.ResetLevel();
        playerAnimator.SetBool("isDead", false);
        CanvasManager.GetInstance().SwitchCanvas(CanvasType.GameUI);
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

    private IEnumerator ApplySlowMotionEffect()
    {
        playerAnimator.SetBool("isDead", true);
        Time.timeScale = slowdownFactor;
        currentAudioSource = audioManager.GetAudioSource("SFX", "Death");
        currentAudioSource.pitch = slowdownFactor;
        currentAudioSource.Play();
        yield return new WaitForSecondsRealtime(slowdownTime);
        Time.timeScale = 1f;
    }

    private IEnumerator Stop()
    {
        Time.timeScale = 0;
        currentAudioSource = audioManager.GetAudioSource("Ambience", "Abyss");
        currentAudioSource.Stop();
        FadeAudioSource.StartFade(currentAudioSource, 0.3f, 0f);
        yield return new WaitForSecondsRealtime(stopTime);
        Time.timeScale = 1f;
    }
}