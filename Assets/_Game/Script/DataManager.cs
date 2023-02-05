using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    private const int DEFAULT_ASSET = 0;
    private const int DEFAULT_LEVEL = 1;
    private const string GOLD_DATAKEY = "gold";
    private const string LEVEL_DATAKEY = "level";

    public static DataManager instance;

    private int level;
    private int gold;

    public int Gold { get => gold; set => gold = value; }
    public int Level { get => level; set => level = value; }

    private void Awake()
    {
        instance = this;
        LoadData();
    }

    void LoadData()
    {
        Gold = 0;//PlayerPrefs.GetInt(GOLD_DATAKEY, DEFAULT_ASSET);
        Level = 1;//PlayerPrefs.GetInt(LEVEL_DATAKEY, DEFAULT_LEVEL);
    }
    public void SetData(int level, int gold)
    {
        Level = level;
        Gold = gold;
        UIManager.instance.UpdateLevelText();
        UIManager.instance.UpdateGoldText();
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("gold", Gold);
        PlayerPrefs.SetInt("level", Level);
    }
}
