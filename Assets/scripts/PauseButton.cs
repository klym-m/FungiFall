using Newtonsoft.Json.Bson;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButton : MonoBehaviour
{
    public HelpBook Book;         

    public void Open()
    {
        Time.timeScale = 0f;
        gameObject.SetActive(true);
    }

    public void Close()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }

    public void OpenBook()
    {
        Book.Open();
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}