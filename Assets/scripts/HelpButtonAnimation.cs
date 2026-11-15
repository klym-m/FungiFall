
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HelpButtonAnim : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image image;
    public Sprite[] frames;
    public float frameDelay = 0.03f;
    public HelpBook helpBook;

    bool hovering;
    bool helpOpen;
    float timer;
    int frame;


    public void OnPointerEnter(PointerEventData e)
    {
        hovering = true;
    }

    public void OnPointerExit(PointerEventData e)
    {
        hovering = false;
    }

    public void OpenHelp()
    {
        if (helpBook == null) return;

        helpBook.Open();
        helpOpen = true;
    }

    public void CloseHelp()
    {
        if (helpBook == null) return;

        helpBook.CloseBook();
        helpOpen = false;
        
    }

    void Update()
    {
        if (image == null || frames == null || frames.Length == 0)
            return;

        timer += Time.unscaledDeltaTime;

        if (timer < frameDelay) return;
        timer = 0f;

        bool wantOpen = hovering || helpOpen;

        if (wantOpen && frame < frames.Length - 1)
            frame+=1;
        else if (!wantOpen && frame > 0)
            frame-=1;

        image.sprite = frames[frame];
    }
}
