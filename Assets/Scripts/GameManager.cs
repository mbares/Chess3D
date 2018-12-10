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

    private bool playerInteractionAllowed;

    private void Awake()
    {
        playerInteractionAllowed = true;
    }

    public void SetPlayerInteractionAllowed(bool value)
    {
        playerInteractionAllowed = value;
    }

    public bool IsPlayerInteractionAllowed()
    {
        return playerInteractionAllowed;
    }

    public void StartNewGame()
    {
        chessboardState.SetUp();
        chessMoveRecorder.ClearMoves();
        currentPlayer = whitePlayer;
        chessboardState.UpdatePiecesOnChessboardData();
        newGameStartedEvent.Raise();
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
}