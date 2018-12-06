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
    [SerializeField]
    private ChessMoveRecorder chessMoveRecorder;

    public void StartNewGame()
    {
        chessMoveRecorder.ClearMoves();
        currentPlayer = whitePlayer;
        chessboardState.UpdatePiecesOnChessboardData();
    }

    public void ContinueGame()
    {
        chessMoveRecorder.ClearMoves();
        currentPlayer = chessboardState.GetUnfinishedGameCurrentPlayerColor() == PieceColor.White ? whitePlayer : blackPlayer;
    }

    public void NextTurn()
    {
        currentPlayer = currentPlayer == whitePlayer ? blackPlayer : whitePlayer;
    }

    public void SaveUnfinishedGameState()
    {
        GameState unfinishedGameState = new GameState {
            currentPlayerColor = currentPlayer.piecesColor,
            chessboardSquaresInfo = chessboardState.GetUnfinishedChessboardSquaresInfo()
        };

        GameStateSerializer.SaveGameState(unfinishedGameState);
    }
}