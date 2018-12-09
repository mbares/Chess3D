using UnityEngine;

[CreateAssetMenu(menuName = "System/ChessMoveRecorder")]
public class ChessMoveRecorder : ScriptableObject
{
    [HideInInspector]
    public PlayerMoves playerMoves;

    [SerializeField]
    private GameEvent moveMade;

    private string lastMoveMade;

    public void SavePlayerMovesForReplay()
    {
        PlayerMovesSerializer.SavePlayerMovesForReplay(playerMoves);
    }

    public void SavePlayerMovesForContinue()
    {
        PlayerMovesSerializer.SavePlayerMovesForContinue(playerMoves);
    }

    public void ClearMoves()
    {
        playerMoves.Clear();
    }

    public string GetLastMoveMade()
    {
        return lastMoveMade;    
    }

    public void RecordNormalMove(ChessPiece movedPiece, ChessboardPosition from, ChessboardPosition to, ChessPiece capturedPiece = null)
    {
        ChessMove move = new ChessMove(from, to);
        lastMoveMade = move.ToAlgebraicNotationString(MoveType.Normal, movedPiece, capturedPiece);

        RecordMove(move);
    }

    public void RecordPawnPromotion()
    {

    }

    public void RecordCastling()
    {

    }

    public void RecordEnPassant()
    {

    }

    public void RecordCheck(ChessPiece movedPiece, ChessboardPosition from, ChessboardPosition to, ChessPiece capturedPiece = null)
    {
        ChessMove move = new ChessMove(from, to);
        lastMoveMade = move.ToAlgebraicNotationString(MoveType.Check, movedPiece, capturedPiece);

        RecordMove(move);
    }

    private void RecordMove(ChessMove move)
    {
        playerMoves.Add(move, lastMoveMade);
        moveMade.Raise();
    }
}
