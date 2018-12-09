using UnityEngine;

[CreateAssetMenu(menuName = "System/GameManager")]
public class GameManager : ScriptableObject
{
    public ChessboardStateManager chessboardState;
    public Player currentPlayer;

    [SerializeField]
    private Player blackPlayer;
    [SerializeField]
    private Player whitePlayer;
    [SerializeField]
    private ChessMoveRecorder chessMoveRecorder;
    [SerializeField]
    private GameEvent newGameStartedEvent;
    [SerializeField]
    private GameEvent continuedGameStartedEvent;
    [SerializeField]
    private GameEvent newTurnEvent;
    [SerializeField]
    private EndOfGameDetector endOfGameDetector;

    public void StartNewGame()
    {
        chessboardState.SetUp();
        chessMoveRecorder.ClearMoves();
        currentPlayer = whitePlayer;
        chessboardState.UpdatePiecesOnChessboardData();
        newGameStartedEvent.Raise();
    }

    public void ContinueGame()
    {
        chessboardState.SetUp();
        chessMoveRecorder.ClearMoves();
        currentPlayer = chessboardState.GetUnfinishedGameCurrentPlayerColor() == PieceColor.White ? whitePlayer : blackPlayer;
        continuedGameStartedEvent.Raise();
    }

    public void SetPlayerPieces()
    {
        chessboardState.UpdatePlayerPiecesSet(whitePlayer);
        chessboardState.UpdatePlayerPiecesSet(blackPlayer);
    }

    public void StartNextTurn()
    {
        chessboardState.GetAllAvailableCapturePositionsOfPlayer(currentPlayer);
        currentPlayer = currentPlayer == whitePlayer ? blackPlayer : whitePlayer;

        if (!endOfGameDetector.CheckForEndOfGame(currentPlayer)) {
            currentPlayer.SetIsInCheck(false);
            newTurnEvent.Raise();
        }
    }

    public void SetOtherPlayerInCheck(Player currentPlayer)
    {
        if (currentPlayer == whitePlayer) {
            blackPlayer.SetIsInCheck(true);
        } else {
            whitePlayer.SetIsInCheck(true);
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
        if (chessMoveRecorder.playerMoves.moves.Count > 0) {
            GameState unfinishedGameState = new GameState {
                currentPlayerColor = currentPlayer.piecesColor,
                chessboardSquaresInfo = chessboardState.GetUnfinishedChessboardSquaresInfo()
            };

            GameStateSerializer.SaveGameState(unfinishedGameState);
        }
    }

    public void SaveUnfinishedGameMoves()
    {
        chessMoveRecorder.SavePlayerMovesForContinue();
    }
}