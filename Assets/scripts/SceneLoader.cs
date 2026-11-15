using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string sceneName = "Game";   
    public CanvasGroup group;           
    public Image barFill;               
    public RectTransform barTrack;      
    public RectTransform mushroom;     

    public float minTime = 2.5f;        
             
    public float turns = 2f;            

    bool loading;

    public void StartLoading()
    {
        if (loading) return;            
        loading = true;

        DontDestroyOnLoad(gameObject);
        gameObject.SetActive(true);
        StartCoroutine(Load());
    }

    IEnumerator Load()
    {
        group.alpha = 0f;

       
        while (group.alpha < 1f)
        {
            group.alpha += Time.unscaledDeltaTime / 0.3f;
            yield return null;
        }

        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
        op.allowSceneActivation = false;

        float timer = 0f;
        float shown = 0f;
        float width = barTrack.rect.width;

        
        while (shown < 1f)
        {
            timer += Time.unscaledDeltaTime;

            float real = Mathf.Clamp01(op.progress / 0.9f);
            float wanted = Mathf.Min(real, timer / minTime);
            shown = Mathf.MoveTowards(shown, wanted, Time.unscaledDeltaTime * 0.6f);

            barFill.fillAmount = shown;
            mushroom.anchoredPosition = new Vector2(width * shown, 0f);
            mushroom.localRotation = Quaternion.Euler(0f, 0f, -shown * 360f * turns);

            yield return null;
        }

        yield return new WaitForSecondsRealtime(0.3f);

        
        op.allowSceneActivation = true;
        while (!op.isDone) yield return null;

       
        while (group.alpha > 0f)
        {
            group.alpha -= Time.unscaledDeltaTime / 1f;
            yield return null;
        }

        Destroy(gameObject);
    }
}