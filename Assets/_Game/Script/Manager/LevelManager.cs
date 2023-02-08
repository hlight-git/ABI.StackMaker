using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public static LevelManager Instance { get; private set; }

    [SerializeField] private GameObject startSceneTransition;
    [SerializeField] private GameObject endSceneTransition;

    private bool isTransitioning;
    public bool IsTransitioning => isTransitioning;

    private void Awake()
    {
        Instance = this;
        isTransitioning = false;
    }
    public void StartGame()
    {
        StartCoroutine(LoadScene(LoadCurrentPlayerLevelScene));
    }
    public void RestartPlayingLevel()
    {
        StartCoroutine(LoadScene(ReloadCurrentLevelScene));
        UIManager.Instance.DisableWonPanel();
    }
    public void NextLevel()
    {
        UIManager.Instance.DisableWonPanel();
        if (DataManager.Instance.Level + 1 == SceneManager.sceneCountInBuildSettings)
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
        UIManager.Instance.ActivePlayingPanel();
        SceneManager.LoadScene(DataManager.Instance.Level);
        UIManager.Instance.UpdateLevelText();
        UIManager.Instance.UpdateGoldText();
    }
    private void ReloadCurrentLevelScene()
    {
        SceneManager.LoadScene(DataManager.Instance.Level);
    }
    private void LoadNextLevelScene()
    {
        int nextLevel = DataManager.Instance.Level + 1;
        int newGold = DataManager.Instance.Gold + GameConstant.Level.Reward.GOLD;

        DataManager.Instance.SetData(nextLevel, newGold);
        DataManager.Instance.SaveData();

        SceneManager.LoadScene(nextLevel);
    }
    IEnumerator LoadScene(Action loadSceneAction)
    {
        SetSceneTransitionActive(endSceneTransition, true);
        yield return new WaitForSeconds(GameAnim.Duration.SceneTransition.END_SCENE);

        loadSceneAction();
        yield return new WaitForEndOfFrame();
        ChangeSceneTransition();

        yield return new WaitForSeconds(GameAnim.Duration.SceneTransition.START_SCENE);
        SetSceneTransitionActive(startSceneTransition, false);
    }
    void ChangeSceneTransition()
    {
        SetSceneTransitionActive(startSceneTransition, true);
        SetSceneTransitionActive(endSceneTransition, false);
        isTransitioning = true;
    }

    private void SetSceneTransitionActive(GameObject sceneTransition, bool isActive)
    {
        sceneTransition.SetActive(isActive);
        isTransitioning = isActive;
    }
}
