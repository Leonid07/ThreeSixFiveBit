using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Swipe : MonoBehaviour, IEndDragHandler
{
    [SerializeField] int maxPage;
    public int currentPage;
    Vector3 targetPos;
    [SerializeField] Vector3 pageStep;
    [SerializeField] RectTransform levelPagesRect;

    [SerializeField] float tweenTime;

    float dragThreshould;

    public Button buttonLeft;
    public Button buttonRight;

    private void Awake()
    {
        currentPage = 1;
        targetPos = levelPagesRect.localPosition;
        dragThreshould = Screen.width / 15;
    }

    private void Start()
    {
        //buttonLeft.onClick.AddListener(Previous);
        buttonLeft.onClick.AddListener(() => 
        {
            Previous();
            StartCoroutine(MovePage());
        });
        buttonRight.onClick.AddListener(() => 
        { 
            Next();
            StartCoroutine(MovePage());
        });
        //buttonRight.onClick.AddListener(Next);
        buttonLeft.gameObject.SetActive(false);
    }

    public void Next()
    {
        if (currentPage < maxPage)
        {
            currentPage++;
            targetPos += pageStep;
            StartCoroutine(MovePage());
            buttonLeft.gameObject.SetActive(true);
            buttonRight.gameObject.SetActive(false);
        }
    }

    public void Previous()
    {
        if (currentPage > 1)
        {
            currentPage--;
            targetPos -= pageStep;
            StartCoroutine(MovePage());
            buttonRight.gameObject.SetActive(true);
            buttonLeft.gameObject.SetActive(false);
        }
    }

    IEnumerator MovePage()
    {
        Vector3 startPos = levelPagesRect.localPosition;
        float elapsedTime = 0f;

        while (elapsedTime < tweenTime)
        {
            levelPagesRect.localPosition = Vector3.Lerp(startPos, targetPos, elapsedTime / tweenTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        levelPagesRect.localPosition = targetPos;
        CheckIsBuyRecord();
    }

    public void CheckIsBuyRecord()
    {
        int count = currentPage;
        count--;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (Mathf.Abs(eventData.position.x - eventData.pressPosition.x) > dragThreshould)
        {
            if (eventData.position.x > eventData.pressPosition.x) Previous();
            else Next();
        }
        else
        {
            StartCoroutine(MovePage());
        }
    }
}
