using UnityEngine;

public class OnTurnDisplay : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private GameObject whitePlayerLabel;
    [SerializeField]
    private GameObject blackPlayerLabel;

    public void ToggleOnTurnLabel()
    {
        if (gameManager.currentPlayer.piecesColor == PieceColor.White) {
            whitePlayerLabel.SetActive(true);
            blackPlayerLabel.SetActive(false);
        } else {
            blackPlayerLabel.SetActive(true);
            whitePlayerLabel.SetActive(false);
        }
    }
}
