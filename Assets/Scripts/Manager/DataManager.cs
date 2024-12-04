using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager InstanceData { get; private set; }

    private void Awake()
    {
        if (InstanceData != null && InstanceData != this)
        {
            Destroy(gameObject);
        }
        else
        {
            InstanceData = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public string idEducation = "education";
    public int isSaveEducation = 0;
    public GameObject panelEducation;

    [Header("Обучения боя")]
    public GameObject panelEducationGame;
    public string idEducationGame = "idEducationGame";
    public int isSaveEducationGame = 0;

    public int coin;
    public string idCoin = "coin";
    public TMP_Text[] textCountCoin;

    public Map[] levels;

    public Map mapNextLevel;

    private void Start()
    {
        SetIndexLevel();
        LoadLevel();
        ApplyCoinToText();
        LoadEducation();
        CheckEducation();
        LoadEducationGame();
        LoadCoin();
        ApplyCoinToText();
    }
    public void SaveCoin()
    {
        PlayerPrefs.SetInt(idCoin, coin);
        PlayerPrefs.Save();
    }
    public void LoadCoin()
    {
        if (PlayerPrefs.HasKey(idCoin))
        {
            coin = PlayerPrefs.GetInt(idCoin);
        }
    }
    public void ApplyCoinToText()
    {
        foreach (TMP_Text text in textCountCoin)
        {
            text.text = coin.ToString();
        }
    }
    public void SetIndexLevel()
    {
        int count = 1;
        for (int i = 0; i < levels.Length; i++)
        {
            levels[i].indexLevel = count;
            levels[i].SetTextIndexMap();
            count++;
        }
    }
    public void SaveLevel()
    {
        for (int i = 0; i < levels.Length; i++)
        {
            PlayerPrefs.SetInt(levels[i].idLevel, levels[i].isLoad);
            PlayerPrefs.Save();
        }
    }
    public void LoadLevel()
    {
        for (int i = 0; i < levels.Length; i++)
        {
            if (PlayerPrefs.HasKey(levels[i].idLevel))
            {
                levels[i].isLoad = PlayerPrefs.GetInt(levels[i].idLevel);
                levels[i].CheckLevel();
            }
        }
    }

    public void SaveEducationGame()
    {
        PlayerPrefs.SetInt(idEducationGame, isSaveEducationGame);
        PlayerPrefs.Save();
    }
    public void LoadEducationGame()
    {
        if (PlayerPrefs.HasKey(idEducationGame))
        {
            isSaveEducationGame = PlayerPrefs.GetInt(idEducationGame);
        }
    }

    public void SaveEducation()
    {
        PlayerPrefs.SetInt(idEducation, isSaveEducation);
        PlayerPrefs.Save();
    }
    public void LoadEducation()
    {
        if (PlayerPrefs.HasKey(idEducation))
        {
            isSaveEducation = PlayerPrefs.GetInt(idEducation);
        }
    }
    public void CheckEducation()
    {
        if (isSaveEducation == 0)
        {
            panelEducation.SetActive(true);
        }
        else
        {
            panelEducation.SetActive(false);
        }
    }
}
