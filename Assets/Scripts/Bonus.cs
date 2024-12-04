using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Bonus : MonoBehaviour
{
    public TMP_Text hourlyBonusText; // ����� ��� �������� ������

    public Button hourlyBonusButton; // ������ ��� �������� ������

    public Color standartColor;
    public Color newColor;

    private const string HourlyBonusTimeKey = "hourly_bonus_time"; // ���� ��� ���������� ������� �������� ������

    public int HourlyBonusCooldownInSeconds = 3600; // 1 ���


    public int countHourly = 100; // ���������� ������� �� ������� �����

    private void Start()
    {
        hourlyBonusButton.onClick.AddListener(ClaimHourlyBonus); // ���������� ��������� ��� �������� ������
        StartCoroutine(UpdateBonusTextsRoutine());
    }

    private IEnumerator UpdateBonusTextsRoutine()
    {
        while (true)
        {
            UpdateBonusTexts();
            yield return new WaitForSeconds(0.5f); // ���������� ������ ������ 0.5 �������
        }
    }

    private void UpdateBonusTexts()
    {
        string hourlyBonusTimeStr = PlayerPrefs.GetString(HourlyBonusTimeKey, "0"); // ��������� ������� �������� ������

        long hourlyBonusTime = long.Parse(hourlyBonusTimeStr); // �������������� ������� �������� ������

        long currentTimestamp = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;

        long hourlyCooldown = hourlyBonusTime + HourlyBonusCooldownInSeconds - currentTimestamp; // ���������� ����������� ������� ��� �������� ������

        hourlyBonusText.text = FormatTimeHourly(hourlyCooldown); // ���������� ������ �������� ������

        hourlyBonusButton.interactable = hourlyCooldown <= 0; // ���������� ������ �������� ������
    }
    private string FormatTimeHourly(long seconds) // �������������� ������ ��� �������� ������
    {
        if (seconds <= 0)
        {
            hourlyBonusButton.GetComponent<Image>().color = standartColor;
            return "Take";
        }
        hourlyBonusButton.GetComponent<Image>().color = newColor;
        TimeSpan timeSpan = TimeSpan.FromSeconds(seconds);
        return string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
    }

    private void ClaimHourlyBonus() // ����� ��� ��������� �������� ������
    {
        long currentTimestamp = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
        //GameManager.InstanceGame.gold += countHourly;
        //DataManager.InstanceData.SaveGold();
        PlayerPrefs.SetString(HourlyBonusTimeKey, currentTimestamp.ToString());
        PlayerPrefs.Save();

        Debug.Log("Hourly Bonus Claimed!");
        Debug.Log($"New Hourly Bonus Time: {currentTimestamp}");
    }
}
