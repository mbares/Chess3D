using UnityEngine;

[CreateAssetMenu(menuName = "System/ChessMoveRecorder")]
public class ChessMoveRecorder : ScriptableObject
{
    [HideInInspector]
    public PlayerMoves playerMoves;

    [SerializeField]
    private GameEvent moveMade;
    [SerializeField]
    private GameEvent pawnPromotionRewrite;

    private string lastMoveMade;

    public void SavePlayerMovesForReplay()
    {
        if (playerMoves.moves.Count > 0) {
            PlayerMovesSerializer.SavePlayerMovesForReplay(playerMoves);
        }
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

    public void RecordPawnPromotion(PieceType type, PieceLabel promotedTo)
    {
        ChessMove lastMove = playerMoves.moves[playerMoves.moves.Count - 1];
        lastMoveMade = lastMove.PawnPromotionToAlgebraicNotation(promotedTo);

        playerMoves.pawnPromotions.Add(lastMove.to, type);

        pawnPromotionRewrite.Raise();
    }

    public void RecordKingsideCastling(ChessMove kingMove, ChessMove rookMove)
    {
        lastMoveMade = "0-0";
        RecordCastling(kingMove, rookMove);
    }

    public void RecordQueensideCastling(ChessMove kingMove, ChessMove rookMove)
    {
        lastMoveMade = "0-0-0";
        RecordCastling(kingMove, rookMove);
    }

    private void RecordCastling(ChessMove kingMove, ChessMove rookMove)
    {
        playerMoves.Add(kingMove);
        playerMoves.Add(rookMove);

        moveMade.Raise();
    }

    public void RecordEnPassant(ChessboardPosition from, ChessboardPosition to)
    {
        ChessMove move = new ChessMove(from, to);
        lastMoveMade = move.EnPassantToAlgebraicNotation();

        RecordMove(move);
    }

    public void RecordCheck(ChessPiece movedPiece, ChessboardPosition from, ChessboardPosition to, ChessPiece capturedPiece = null)
    {
        ChessMove move = new ChessMove(from, to);
        lastMoveMade = move.ToAlgebraicNotationString(MoveType.Check, movedPiece, capturedPiece);

        RecordMove(move);
    }

    private void RecordMove(ChessMove move)
    {
        playerMoves.Add(move);
        moveMade.Raise();
    }
}
