using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private Text goldText;
    [SerializeField] private Text levelText;
    [SerializeField] private GameObject playingPanel;
    [SerializeField] private GameObject wonPanel;

    private void Awake()
    {
        instance = this;
    }
    public void ActivePlayingPanel()
    {
        playingPanel.SetActive(true);
    }

    public void ActiveWonPanel()
    {
        wonPanel.SetActive(true);
    }

    public void DisableWonPanel()
    {
        wonPanel.SetActive(false);
    }
    public void UpdateGoldText()
    {
        goldText.text = DataManager.instance.Gold.ToString();
    }

    public void UpdateLevelText()
    {
        levelText.text = "Level " + DataManager.instance.Level.ToString();
    }
}
