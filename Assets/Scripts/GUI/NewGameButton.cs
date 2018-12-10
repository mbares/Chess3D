using UnityEngine;

public class NewGameButton : MonoBehaviour
{
    [SerializeField]
    private GameObject newGamePanel;
    [SerializeField]
    private GameEvent newGameRequestEvent;
    [SerializeField]
    private GameEvent guiShown;
    [SerializeField]
    private GameEvent guiHidden;

    public void ShowNewGamePanel()
    {
        newGamePanel.SetActive(true);
        guiShown.Raise();
    }

    public void HideNewGamePanel()
    {
        newGamePanel.SetActive(false);
        guiHidden.Raise();
    }

    public void StartNewGame()
    {
        newGameRequestEvent.Raise();
    }
}
