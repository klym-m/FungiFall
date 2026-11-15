using System.Collections;
using TMPro;
using UnityEngine;

public class Busket : MonoBehaviour
{
    public int counter = 0;
    public GameObject LosePanel;
    public TMP_Text score;       
    public TMP_Text loseScore;   
    public float fadeTime = 0.5f;

    void Start()
    {
        UpdateScore();
    }

    void OnTriggerEnter(Collider collider)
    {
        switch (collider.gameObject.tag)
        {
            case "Fatal":
                loseScore.text = counter.ToString();
                Time.timeScale = 0f;
                StartCoroutine(FadeIn());
                break;
            case "Poison":
                counter = Mathf.Max(0, counter - 1);
                break;
            case "Safe":
                counter++;
                break;
            default:
                return;
        }

        UpdateScore();
        Destroy(collider.gameObject);
    }

    void UpdateScore()
    {
        score.text = counter.ToString();
    }

    IEnumerator FadeIn()
    {
        CanvasGroup group = LosePanel.GetComponent<CanvasGroup>();
        group.alpha = 0f;
        LosePanel.SetActive(true);

        float t = 0f;
        while (t < fadeTime)
        {
            t += Time.unscaledDeltaTime;
            group.alpha = Mathf.Clamp01(t / fadeTime);
            yield return null;
        }
        group.alpha = 1f;
    }
}