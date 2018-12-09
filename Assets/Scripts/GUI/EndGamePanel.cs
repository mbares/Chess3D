using UnityEngine;
using UnityEngine.UI;

public class EndGamePanel : MonoBehaviour
{
    [SerializeField]
    private ChessMoveRecorder chessMoveRecorder;
    [SerializeField]
    private Text winLabel;
    [SerializeField]
    private Button saveReplayButton;

    public void ShowEndGamePanel(string gameOutcome)
    {
        SetUp(gameOutcome);
        gameObject.SetActive(true);
    }

    public void HideEndGamePanel()
    {
        gameObject.SetActive(false);
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
        saveReplayButton.onClick.AddListener(() => chessMoveRecorder.SavePlayerMovesForReplay());
    }
}