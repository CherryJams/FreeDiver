using System;
using Unity.VisualScripting;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private int level = 1;
    [SerializeField] private int experience = 0;
    [SerializeField] private ExperienceBar experienceBar;
    [SerializeField] private GameObject upgradesMenu;


    int TO_LEVEL_UP
    {
        get { return level * 1000; }
    }

    private void Start()
    {
        experienceBar.UpdateExperienceSlider(experience, TO_LEVEL_UP);
        experienceBar.SetLevelText(level);
    }

    public void AddExperience(int amount)
    {
        experience += amount;
        CheckLevelUp();
        experienceBar.UpdateExperienceSlider(experience, TO_LEVEL_UP);
    }

    private void CheckLevelUp()
    {
        if (experience >= TO_LEVEL_UP)
        {
            experience -= TO_LEVEL_UP;
            level++;
            experienceBar.SetLevelText(level);
            ShowUpdragesMenu();
        }
    }

    private void ShowUpdragesMenu()
    {
        Time.timeScale = 0;
        AudioManager.GetInstance().GetAudioSource("UISounds", "LevelUp").Play();
        upgradesMenu.SetActive(true);
    }

    public void HideUpgradesMenu()
    {
        Time.timeScale = 1;
        upgradesMenu.SetActive(false);
    }

    public void ResetLevel()
    {
        level = 1;
        experience = 0;
        experienceBar.UpdateExperienceSlider(experience, TO_LEVEL_UP);
        experienceBar.SetLevelText(level);
    }
}