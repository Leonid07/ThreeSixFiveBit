using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Education : MonoBehaviour
{
    public int countCheckLevel = 0;
    public string idIsCurrentLevelqwe = "idIsCurrentLevelqwe";

    public Button panel_1;
    public Button panel_2;
    public Button panel_3;

    int count = 0;

    private void Start()
    {
        LoadEducation();

        panel_1.onClick.AddListener(NextPanel);
        panel_2.onClick.AddListener(NextPanel);
        panel_3.onClick.AddListener(NextPanel);
    }
    public void LoadEducation()
    {
        if (PlayerPrefs.HasKey(idIsCurrentLevelqwe))
        {
            countCheckLevel = PlayerPrefs.GetInt(idIsCurrentLevelqwe);
        }
    }
    public void SaveEducation()
    {
        PlayerPrefs.SetInt(idIsCurrentLevelqwe, countCheckLevel);
        PlayerPrefs.Save();
    }
    public void NextPanel()
    {
        switch (count)
        {
            case 0:
                panel_1.gameObject.SetActive(false);
                panel_2.gameObject.SetActive(true);
                break;
            case 1:
                panel_2.gameObject.SetActive(false);
                panel_3.gameObject.SetActive(true);
                break;
            case 2:
                panel_3.gameObject.SetActive(false);

                DataManager.InstanceData.mapNextLevel = DataManager.InstanceData.levels[0];
                PanelManager.InstancePanel.StartLevelGame();
                PanelManager.InstancePanel.player.StartMovetment();
                GameManager.InstanceGame.RestartEnemy();
                GameManager.InstanceGame.StartGame();
                countCheckLevel++;
                SaveEducation();
                break;
        }
        count++;
    }
}
