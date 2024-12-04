using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Enemy : MonoBehaviour, IPointerClickHandler
{
    [Header("��������� ���������������")]
    public float inhaleSpeed = 1f; // �������� �����
    public float exhaleSpeed = 1f; // �������� ������

    [Header("������� ���������������")]
    public float maxScaleY = 1.2f; // ������������ ������� �� ��� Y
    public float minScaleY = 0.8f; // ����������� ������� �� ��� Y

    private bool isInhaling = true;

    [Header("���������� �������")]
    public SpriteRenderer spriteEnemy;

    public StopPoint stopPoint;

    private void Start()
    {
        spriteEnemy = GetComponent<SpriteRenderer>();
        StartCoroutine(BreathingCoroutine());
    }

    private IEnumerator BreathingCoroutine()
    {
        while (true)
        {
            Vector3 scale = transform.localScale;
            Vector3 position = transform.localPosition;

            if (isInhaling)
            {
                while (scale.y < maxScaleY)
                {
                    scale.y += inhaleSpeed * Time.deltaTime;
                    position.y += exhaleSpeed * Time.deltaTime;
                    transform.localScale = new Vector3(scale.x, scale.y, scale.z);
                    transform.position = new Vector3(position.x, position.y, position.z);
                    yield return null;
                }
                isInhaling = false;
            }
            else
            {
                while (scale.y > minScaleY)
                {
                    scale.y -= exhaleSpeed * Time.deltaTime;
                    position.y -= exhaleSpeed * Time.deltaTime;
                    transform.localScale = new Vector3(scale.x, scale.y, scale.z);
                    transform.position = new Vector3(position.x, position.y, position.z);
                    yield return null;
                }
                isInhaling = true;
            }

            yield return null;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        stopPoint.OnPointerClick();
    }
}
