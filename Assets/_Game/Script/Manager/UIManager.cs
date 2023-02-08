using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text goldText;
    [SerializeField] private Text levelText;
    [SerializeField] private GameObject playingPanel;
    [SerializeField] private GameObject wonPanel;

    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
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
        goldText.text = DataManager.Instance.Gold.ToString();
    }

    public void UpdateLevelText()
    {
        levelText.text = "Level " + DataManager.Instance.Level.ToString();
    }
}
