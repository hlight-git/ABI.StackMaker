using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private const int FINISH_LEVEL_REWARD = 50;
    private const float START_SCENE_TRANSITION_ANIM_DURATION = 1.5f;
    private const float END_SCENE_TRANSITION_ANIM_DURATION = 1.5f;

    public static LevelManager instance;

    [SerializeField] private GameObject startSceneTransition;
    [SerializeField] private GameObject endSceneTransition;

    private bool isTransitioning;
    public bool IsTransitioning => isTransitioning;

    private void Awake()
    {
        instance = this;
    }
    public void StartGame()
    {
        StartCoroutine(LoadScene(LoadCurrentPlayerLevelScene));
    }
    public void RestartPlayingLevel()
    {
        StartCoroutine(LoadScene(ReloadCurrentLevelScene));
    }
    public void NextLevel()
    {
        UIManager.instance.DisableWonPanel();
        if (DataManager.instance.Level + 1 == SceneManager.sceneCountInBuildSettings)
        {
            RestartPlayingLevel();
        }
        else
        {
            StartCoroutine(LoadScene(LoadNextLevelScene));
        }
    }
    public void LoadCurrentPlayerLevelScene()
    {
        UIManager.instance.ActivePlayingPanel();
        SceneManager.LoadScene(DataManager.instance.Level);
        UIManager.instance.UpdateLevelText();
        UIManager.instance.UpdateGoldText();
    }
    private void ReloadCurrentLevelScene()
    {
        SceneManager.LoadScene(DataManager.instance.Level);
    }
    private void LoadNextLevelScene()
    {
        int nextLevel = DataManager.instance.Level + 1;
        int newGold = DataManager.instance.Gold + FINISH_LEVEL_REWARD;

        DataManager.instance.SetData(nextLevel, newGold);
        DataManager.instance.SaveData();

        SceneManager.LoadScene(nextLevel);
    }
    IEnumerator LoadScene(Action loadSceneAction)
    {
        EndSceneTransition();
        yield return new WaitForSeconds(END_SCENE_TRANSITION_ANIM_DURATION);

        loadSceneAction();
        yield return new WaitForSeconds(0.2f);
        ChangeSceneTransition();

        yield return new WaitForSeconds(START_SCENE_TRANSITION_ANIM_DURATION);
        DisableStartSceneTransition();
    }
    void ChangeSceneTransition()
    {
        StartSceneTransition();
        DisableEndSceneTransition();
        isTransitioning = true;
    }
    private void StartSceneTransition()
    {
        isTransitioning = true;
        startSceneTransition.SetActive(true);
    }
    private void EndSceneTransition()
    {
        isTransitioning = true;
        endSceneTransition.SetActive(true);
    }
    private void DisableStartSceneTransition()
    {
        startSceneTransition.SetActive(false);
        isTransitioning = false;
    }
    private void DisableEndSceneTransition()
    {
        endSceneTransition.SetActive(false);
        isTransitioning = false;
    }
}
