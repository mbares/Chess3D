using UnityEngine;

public class QuitButton : MonoBehaviour
{
    [SerializeField]
    private GameObject quitGamePanel;
    [SerializeField]
    private GameEvent guiShown;
    [SerializeField]
    private GameEvent guiHidden;

    public void ShowQuitGamePanel()
    {
        quitGamePanel.SetActive(true);
        guiShown.Raise();

    }

    public void HideQuitGamePanel()
    {
        quitGamePanel.SetActive(false);
        guiHidden.Raise();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
