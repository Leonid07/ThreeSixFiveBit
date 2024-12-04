using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Load : MonoBehaviour
{
    [SerializeField] private TMP_Text loadingText;
    public GameObject firstPanelEducation;

    private void Start()
    {
        StartCoroutine(LoadSimulation());
    }

    private IEnumerator LoadSimulation()
    {
        float progress = 0f;

        while (progress < 1f)
        {
            // Simulate loading progress
            progress += Time.deltaTime * 0.1f; // Adjust multiplier for speed
            loadingText.text = (progress * 100f).ToString("F0") + "%";

            yield return null;
        }

        loadingText.text = "100%";
        yield return new WaitForSeconds(0.5f); // Optional delay before hiding
        gameObject.SetActive(false);
        firstPanelEducation.SetActive(true);
    }
}
