
using UnityEngine;
using UnityEngine.UI;

public class HelpBook : MonoBehaviour
{
    public Sprite[] LeftPages;
    public Sprite[] RightPages;

    public Image RightOne;
    public Image LeftOne;


    public Button Back;
    public Button Next;
    public Button Close;


    public int index;
    

    public void Open()
    {
        gameObject.SetActive(true);
        

        index = 0;
        UpdatePages();
    }

    public void NextPg()
    {
        if (RightPages == null || index >= RightPages.Length - 1)
            return;

        index+=1;
        UpdatePages();
    }

    public void PrevPg()
    {
        if (index <= 0)
            return;

        index-=1;
        UpdatePages();
    }

    public void CloseBook()
    {
        
        gameObject.SetActive(false);
    }

    private void UpdatePages()
    {
        if (LeftPages == null || RightPages == null)
            return;

        if (index < 0 || index >= LeftPages.Length || index >= RightPages.Length)
            return;

        if (LeftOne != null)
            LeftOne.sprite = LeftPages[index];

        if (RightOne != null)
            RightOne.sprite = RightPages[index];
    }
}
