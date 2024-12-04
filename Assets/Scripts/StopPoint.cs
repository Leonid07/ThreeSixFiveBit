using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StopPoint : MonoBehaviour
{
    public bool requiresPause = true; // Определяет, нужно ли останавливать персонажа на этой точке
    public bool lastEnemy = false;
    public Enemy enemy;

    public float fadeDuration = 1.0f;

    [Header("Параметры игрока")]
    public GameObject spawnPoint_1;

    public int countTabs = 5;
    public float timeGameOver = 3f;

    public Coroutine deadTimer;

    public void OnPointerClick()
    {
        if (countTabs == 0)
        {
            // dead MOB
            OnReached();
            StartCoroutine(FadeAndPlaySound());
            StopCoroutine(deadTimer);
            PanelManager.InstancePanel.player.OnContinueButtonPressed();
        }
        else
        {
            int cou = countTabs;
            cou--;
            PanelManager.InstancePanel.player.health[cou].gameObject.SetActive(false);
            countTabs--;
        }
    }

    public IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(timeGameOver);

        PanelManager.InstancePanel.panelLose.SetActive(true);

        SoundManager.InstanceSound.musicLevelMainMenu.Play();
        SoundManager.InstanceSound.musicFonGame.Stop();

    }

    // Метод для обработки достижения точки (можно добавить другие параметры и действия)
    public void OnReached()
    {
        if (requiresPause)
        {
            if (enemy != null)
            {
                if (lastEnemy == true)
                {
                    if (DataManager.InstanceData.mapNextLevel.mapNextLevel.isLoad == 0)
                    {
                        DataManager.InstanceData.mapNextLevel.OpenLevel();
                    }
                    else
                    {
                        Debug.Log("прохождение одного и тогоже уровня");
                    }

                    PanelManager.InstancePanel.panelWin.SetActive(true);
                    DataManager.InstanceData.coin += 50;
                    DataManager.InstanceData.SaveCoin();
                    DataManager.InstanceData.ApplyCoinToText();

                    PanelManager.InstancePanel.panelWin.SetActive(true);

                    SoundManager.InstanceSound.musicLevelMainMenu.Play();
                    SoundManager.InstanceSound.musicFonGame.Stop();

                    Debug.Log("End Game");
                }
            }
        }
    }
    private IEnumerator FadeAndPlaySound()
    {

        if (SoundManager.InstanceSound.soundEnemy != null)
        {
            SoundManager.InstanceSound.soundEnemy.Play();
        }

        float elapsedTime = 0f;
        Color originalColor = enemy.spriteEnemy.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / fadeDuration);

            Color newColor = originalColor;

            newColor.a = Mathf.Lerp(originalColor.a, 0, t);

            enemy.spriteEnemy.color = newColor;

            yield return null;
        }

        Color finalColor = enemy.spriteEnemy.color;

        finalColor.a = 0;

        enemy.spriteEnemy.color = finalColor;
    }
}