using UnityEngine;
using UnityEngine.UI;

public class EndGamePanel : MonoBehaviour
{
    [SerializeField]
    GameManager gameManager;
    [SerializeField]
    private ChessMoveRecorder chessMoveRecorder;
    [SerializeField]
    private GameEvent startNewGameEvent;
    [SerializeField]
    private GameEvent guiShown;
    [SerializeField]
    private GameEvent guiHidden;
    [SerializeField]
    private Text winLabel;

    public void ShowEndGamePanel(string gameOutcome)
    {
        if(gameManager.IsPlayerInteractionAllowed()) {
            SetUp(gameOutcome);
            gameObject.SetActive(true);
            guiShown.Raise();
        } else {
            startNewGameEvent.Raise();
        }
    }

    public void HideEndGamePanel()
    {
        gameObject.SetActive(false);
        startNewGameEvent.Raise();
        guiHidden.Raise();
    }

    private void SetUp(string gameOutcome)
    {
        switch (gameOutcome) {
            case "WhiteWins":
                winLabel.text = "WHITE WINS";
                break;
            case "BlackWins":
                winLabel.text = "BLACK WINS";
                break;
            case "Draw":
                winLabel.text = "DRAW";
                break;
            default:
                winLabel.text = "GAME OVER";
                break;
        }
    }

    public void SaveReplay()
    {
        chessMoveRecorder.SavePlayerMovesForReplay();
    }
}