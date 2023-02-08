using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    public int Gold { get; private set; }
    public int Level { get; private set; }

    private void Awake()
    {
        Instance = this;
        LoadData(true);
    }

    void LoadData(bool isContinue)
    {
        if (isContinue)
        {
            Gold = PlayerPrefs.GetInt(GameData.Keys.GOLD, GameData.DEFAULT_ASSET);
            Level = PlayerPrefs.GetInt(GameData.Keys.LEVEL, GameData.DEFAULT_LEVEL);
        }
        else
        {
            Gold = GameData.DEFAULT_ASSET;
            Level = GameData.DEFAULT_LEVEL;
        }
    }
    public void SetData(int level, int gold)
    {
        Level = level;
        Gold = gold;
        UIManager.Instance.UpdateLevelText();
        UIManager.Instance.UpdateGoldText();
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt(GameData.Keys.GOLD, Gold);
        PlayerPrefs.SetInt(GameData.Keys.LEVEL, Level);
    }
}
