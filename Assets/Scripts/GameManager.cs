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
        chessboardState.SetUp();
        chessMoveRecorder.ClearMoves();
        currentPlayer = whitePlayer;
        chessboardState.UpdatePiecesOnChessboardData();
    }

    public void ContinueGame()
    {
        chessboardState.SetUp();
        chessMoveRecorder.ClearMoves();
        currentPlayer = chessboardState.GetUnfinishedGameCurrentPlayerColor() == PieceColor.White ? whitePlayer : blackPlayer;
    }

    public void StartNextTurn()
    {
        chessboardState.GetAllAvailableCapturePositionsOfPlayer(currentPlayer);
        currentPlayer = currentPlayer == whitePlayer ? blackPlayer : whitePlayer;
        currentPlayer.SetIsInCheck(chessboardState.DetectIfPlayerInCheck(currentPlayer));
        if(currentPlayer.IsInCheck()) {
            currentPlayer.pieceCausingCheck = chessboardState.GetPieceThatIsCausingCheck(currentPlayer);
        }
    }

    public void GetAllAvailableCapturePositionsOfWhitePlayer()
    {
        chessboardState.GetAllAvailableCapturePositionsOfPlayer(whitePlayer);
    }

    public void GetAllAvailableCapturePositionsOfBlackPlayer()
    {
        chessboardState.GetAllAvailableCapturePositionsOfPlayer(blackPlayer);
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