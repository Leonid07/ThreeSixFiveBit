using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Map : MonoBehaviour
{
    [Header("Индекс уровня")]
    public int indexLevel = 0;

    Button thisButton;
    public Image imageButton;
    public Sprite standartSprite;
    public Sprite blockSprite;

    public TMP_Text textIndexLevel;

    public int isLoad = 0; // 0 не пройдено 1 пройдено
    public string idLevel;

    public Map mapNextLevel;

    public Map thisLevel;

    public Education education;

    private void Awake()
    {
        imageButton = GetComponent<Image>();
        thisLevel = GetComponent<Map>();
        textIndexLevel = GetComponentInChildren<TMP_Text>();
        thisButton = GetComponent<Button>();

        idLevel = gameObject.name;
        thisButton.onClick.AddListener(OnPointerClick);
        CheckLevel();
    }
    public void OnPointerClick()
    {
        LoadLevel();
    }
    public void LoadLevel()
    {
        if (isLoad == 0)
            return;



        if (education.countCheckLevel == 0)
        {
            education.panel_1.gameObject.SetActive(true);
            return;
        }
        else
        {
            DataManager.InstanceData.mapNextLevel = thisLevel;
            SoundManager.InstanceSound.musicFonGame.Play();
            SoundManager.InstanceSound.musicLevelMainMenu.Stop();

            PanelManager.InstancePanel.StartLevelGame();
            PanelManager.InstancePanel.player.StartMovetment();
            GameManager.InstanceGame.RestartEnemy();
            GameManager.InstanceGame.StartGame();
            PanelManager.InstancePanel.player.RestartMovement();
        }
    }
    public void OpenLevel()
    {
        mapNextLevel.isLoad = 1;
        mapNextLevel.CheckLevel();
        DataManager.InstanceData.SaveLevel();
    }

    public void CheckLevel()
    {
        if (isLoad == 0)
        {
            imageButton.sprite = blockSprite;
        }
        if (isLoad == 1)
        {
            imageButton.sprite = standartSprite;
        }
    }
    public void SetTextIndexMap()
    {
        textIndexLevel.text = indexLevel.ToString();
    }
}