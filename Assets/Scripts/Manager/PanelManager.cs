using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    public static PanelManager InstancePanel { get; private set; }
    private void Awake()
    {
        if (InstancePanel != null && InstancePanel != this)
        {
            Destroy(gameObject);
        }
        else
        {
            InstancePanel = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public Button buttonFinishEducatiob;

    [Header("Игрок")]
    public player player;
    public Character[] characters;

    public GameObject mainCanvas;
    public GameObject gameCanvas;

    [Header("Панели проигрыша и выйгрыша")]
    public GameObject panelWin;
    public GameObject panelLose;

    public Button buttonMainMenuWin;
    public Button buttonMainMenuLose;

    private void Start()
    {
        buttonFinishEducatiob.onClick.AddListener(EducationPassed);

        buttonMainMenuWin.onClick.AddListener(GoToMainMenu);
        buttonMainMenuLose.onClick.AddListener(GoToMainMenu);
    }

    public void StartLevelGame()
    {
        mainCanvas.SetActive(false);
        gameCanvas.SetActive(true);
    }

    public void EducationPassed()
    {
        DataManager.InstanceData.isSaveEducation++;
        DataManager.InstanceData.SaveEducation();
        DataManager.InstanceData.panelEducation.SetActive(false);
    }

    public void GoToMainMenu()
    {
        panelWin.SetActive(false);
        panelLose.SetActive(false);
        mainCanvas.SetActive(true);
        gameCanvas.SetActive(false);
    }
}
