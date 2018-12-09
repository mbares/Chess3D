using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class MoveDisplayManager : MonoBehaviour
{
    [SerializeField]
    private ChessMoveRecorder chessMoveRecorder;
    [SerializeField]
    private Text moveDisplayText;

    private int turnNumber = 1;
    private int turnCounter = 0;

    public void Reset()
    {
        turnNumber = 1;
        moveDisplayText.text = turnNumber + ".";
    }

    public void IncreaseTurnNumber()
    {
        turnCounter++; 

        if (turnCounter == 2) {
            turnNumber++;
            moveDisplayText.text += "\n" + turnNumber + ".";
            turnCounter = 0;
        }
    }

    public void DisplayMove()
    {
        moveDisplayText.text += "  " + chessMoveRecorder.GetLastMoveMade();
    }

    public void DisplayUnfinishedGameMoves()
    {
        Reset();
        try {
            PlayerMoves moves = PlayerMovesSerializer.LoadPlayerMovesForContinue();
            for (int i = 0; i < moves.notatedMoves.Count; ++i) {
                moveDisplayText.text += "  " + moves.notatedMoves[i];
                IncreaseTurnNumber();
            }
            chessMoveRecorder.playerMoves = moves;
        } catch (FileNotFoundException) {
            Debug.LogError("Unfinished game moves file not found");
        }
    }
}
