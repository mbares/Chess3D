using UnityEngine;

[CreateAssetMenu]
public class GameManager : ScriptableObject
{
    public ChessboardState chessboardState;
    public Player currentPlayer;

    [SerializeField]
    private Player blackPlayer;
    [SerializeField]
    private Player whitePlayer;

    public void StartNewGame()
    {
        currentPlayer = whitePlayer;
        chessboardState.UpdateChessboardSquaresInfo();
    }

    public void NextTurn()
    {
        currentPlayer = currentPlayer == whitePlayer ? blackPlayer : whitePlayer;
    }
}