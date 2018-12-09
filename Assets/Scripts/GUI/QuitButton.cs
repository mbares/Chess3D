using UnityEngine;

public class QuitButton : MonoBehaviour
{
    [SerializeField]
    private GameObject quitGamePanel;

    public void ShowQuitGamePanel()
    {
        quitGamePanel.SetActive(true);
    }

    public void HideQuitGamePanel()
    {
        quitGamePanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
