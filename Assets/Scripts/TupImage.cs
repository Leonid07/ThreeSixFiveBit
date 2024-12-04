using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TupImage : MonoBehaviour, IPointerClickHandler
{
    public Character character;
    public float rotationAngle = 45f; // ���� ��������
    public Vector3 scaleIncrease = new Vector3(1f, 1f, 1f); // �� ������� ����������� ������
    public float scaleDuration = 0.5f; // ������������ ���������� � �������� �������
    public int maxTaps = 5; // ���������� ����� ��� ���������� �������
    public int currentTaps = 0; // ������� �����
    public bool rotateClockwise = true; // ����������� ��������

    [Header("��� ��������")]
    public GameObject panelBG_Block;
    public GameObject punch;
    public GameObject[] tabsLabel;
    public Swipe swipePanel;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (character.isCharacterActive)
        {
            character.textButton.gameObject.SetActive(false);

            panelBG_Block.SetActive(true);
            punch.SetActive(true);

            if (currentTaps < maxTaps)
            {
                currentTaps++;
                RotateObject();
            }

            // ���������, ���������� �� ���������� �����
            if (currentTaps == maxTaps)
            {
                if (rotateClockwise == false)
                {
                    StartCoroutine(ScaleObject());
                    rotateClockwise = true;
                }
            }
        }
    }
    public void ChangeBG()
    {
        character.textButton.gameObject.SetActive(false);
        tabsLabel[0].SetActive(true);
        panelBG_Block.SetActive(true);
        punch.SetActive(true);
        swipePanel.buttonLeft.gameObject.SetActive(false);
        swipePanel.buttonRight.gameObject.SetActive(false);
    }

    private void RotateObject()
    {

        switch (currentTaps)
        {
            case 0:
                tabsLabel[0].SetActive(true);
                BlockCountLabel(0);
                break;
            case 1:
                tabsLabel[1].SetActive(true);
                BlockCountLabel(1);
                break;
            case 2:
                tabsLabel[2].SetActive(true);
                BlockCountLabel(2);
                break;
            case 3:
                tabsLabel[3].SetActive(true);
                BlockCountLabel(3);
                break;
            case 4:
                tabsLabel[4].SetActive(true);
                BlockCountLabel(4);
                break;
            case 5:
                tabsLabel[5].SetActive(true);
                BlockCountLabel(5);
                break;
        }

        // �������� ���� ��������
        float angle = rotateClockwise ? rotationAngle : -rotationAngle;

        // ������������ ������
        transform.rotation = Quaternion.Euler(0,0,angle);

        // ������ ����������� ��������
        rotateClockwise = !rotateClockwise;
    }
    void BlockCountLabel(int count)
    {
        for (int i = 0; i < tabsLabel.Length; i++)
        {
            if (i == count)
            {
                tabsLabel[i].SetActive(true);
            }
            else
            {
                tabsLabel[i].SetActive(false);
            }
        }
    }

    private IEnumerator ScaleObject()
    {
        // ��������� ����������� ������
        Vector3 originalScale = transform.localScale;

        transform.rotation = Quaternion.Euler(0,0,0);

        // ����������� ������
        Vector3 targetScale = originalScale + scaleIncrease;
        float elapsedTime = 0f;

        // �������� ������������ �� ������������ �������
        while (elapsedTime < scaleDuration)
        {
            transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsedTime / scaleDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localScale = targetScale;

        // ����� �� ������������ �������
        yield return new WaitForSeconds(0.5f);

        // ���������� ������ � ������������ �������
        elapsedTime = 0f;
        while (elapsedTime < scaleDuration)
        {
            transform.localScale = Vector3.Lerp(targetScale, originalScale, elapsedTime / scaleDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localScale = originalScale;
        panelBG_Block.SetActive(false);
        punch.SetActive(false);
        character.textButton.gameObject.SetActive(true);
        character.isCharacterActive = false;

        swipePanel.buttonLeft.gameObject.SetActive(true);
        swipePanel.buttonRight.gameObject.SetActive(true);
    }
}
