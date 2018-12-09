using UnityEngine;

public class NewGameButton : MonoBehaviour
{
    [SerializeField]
    private GameObject newGamePanel;
    [SerializeField]
    private GameEvent newGameRequestEvent;

    public void ShowNewGamePanel()
    {
        newGamePanel.SetActive(true);
    }

    public void HideNewGamePanel()
    {
        newGamePanel.SetActive(false);
    }

    public void StartNewGame()
    {
        newGameRequestEvent.Raise();
    }
}
