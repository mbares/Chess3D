using System.Collections.Generic;
using UnityEngine;

public class PieceMover : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private ChessboardStateManager chessboardStateManager;
    [SerializeField]
    private EnPassantManager enPassantManager;
    [SerializeField]
    private ChessMoveRecorder chessMoveRecorder;
    [SerializeField]
    private CheckDetector checkDetector;
    [SerializeField]
    private PositionHighlightManager positionHighlightManager;
    [SerializeField]
    private ChessPieceMovement chessPieceMovement;

    public void MovePiece(ChessboardPosition newPosition)
    {
        List<ChessboardPosition> availablePositions = chessPieceMovement.GetAvailablePositions();
        MovePiece(availablePositions, newPosition);
    }

    public void MovePiece(List<ChessboardPosition> availablePositions, ChessboardPosition newPosition)
    {
        ChessPiece chessPiece = chessPieceMovement.GetComponent<ChessPiece>();
        if (availablePositions.Contains(newPosition)) {
            ChessPiece capturedPiece = null;
            bool enPassantMove = false;

            if (ExistsCapturablePieceAtAvailablePosition(newPosition)) {
                capturedPiece = chessboardStateManager.GetChessPieceAtPosition(newPosition);
                chessboardStateManager.RemoveCapturedPieceAtPositionAndDeactivate(newPosition);
            } else if (chessPiece.chessPieceInfo.type == PieceType.Pawn && enPassantManager.enPassantPosition != null && enPassantManager.enPassantPosition.Equals(newPosition)) {
                capturedPiece = enPassantManager.pieceCausingEnPassant;
                chessboardStateManager.RemoveCapturedPieceAtPositionAndDeactivate(capturedPiece.GetChessboardPosition());
                enPassantMove = true;
            }

            ChessboardPosition lastPosition = chessPieceMovement.currentPosition;
            UpdatePieceData(chessPiece, newPosition);
            availablePositions.Clear();

            chessPieceMovement.Move(newPosition);
            chessPieceMovement.hasMoved = true;

            KingMovement kingMovement = chessPiece.GetComponent<KingMovement>();

            if (checkDetector.IsMoveCausingCheck(chessPiece)) {
                gameManager.SetOtherPlayerInCheck(chessPiece.controllingPlayer);
                chessMoveRecorder.RecordCheck(chessPiece, lastPosition, newPosition, capturedPiece);
            } else if (enPassantMove) {
                chessMoveRecorder.RecordEnPassant(lastPosition, newPosition);
            } else if (kingMovement != null && newPosition.Equals(kingMovement.GetKingsideCastlingKingPosition())) {
                DoKingsideCastlingMove(newPosition, kingMovement, lastPosition);
            } else if (kingMovement != null && newPosition.Equals(kingMovement.GetQueensideCastlingKingPosition())) {
                DoQueensideCastlingMove(newPosition, kingMovement, lastPosition);
            } else {
                chessMoveRecorder.RecordNormalMove(chessPiece, lastPosition, newPosition, capturedPiece);
            }

            gameManager.StartNextTurn();
        } else {
            transform.localPosition = ChessboardPositionConverter.ChessboardPositionToVector3(chessPieceMovement.currentPosition);
        }

        positionHighlightManager.DeactivateAllHighlights();
    }


    private void DoKingsideCastlingMove(ChessboardPosition newPosition, KingMovement kingMovement, ChessboardPosition lastPosition)
    {
        ChessPieceMovement kingsideRook = kingMovement.GetKingsideRook();
        ChessboardPosition lastRookPosition = kingsideRook.currentPosition;
        kingsideRook.Move(kingMovement.GetKingsideCastlingRookPosition());
        kingsideRook.hasMoved = true;
        chessMoveRecorder.RecordKingsideCastling(new ChessMove(lastPosition, newPosition), new ChessMove(lastRookPosition, kingsideRook.currentPosition));
    }

    private void DoQueensideCastlingMove(ChessboardPosition newPosition, KingMovement kingMovement, ChessboardPosition lastPosition)
    {
        ChessPieceMovement queensideRook = kingMovement.GetQueensideRook();
        ChessboardPosition lastRookPosition = queensideRook.currentPosition;
        queensideRook.Move(kingMovement.GetQueensideCastlingRookPosition());
        queensideRook.hasMoved = true;
        chessMoveRecorder.RecordQueensideCastling(new ChessMove(lastPosition, newPosition), new ChessMove(lastRookPosition, queensideRook.currentPosition));
    }

    private bool ExistsCapturablePieceAtAvailablePosition(ChessboardPosition newPosition)
    {
        if (chessboardStateManager.GetChessPieceAtPosition(newPosition) != null && chessPieceMovement.IsPieceAtPositionOpponent(newPosition)) {
            return true;
        }
        return false;
    }

    private void UpdatePieceData(ChessPiece chessPiece, ChessboardPosition newPosition)
    {
        chessboardStateManager.RemovePieceDataFromPosition(chessPieceMovement.currentPosition);
        chessboardStateManager.AddPieceDataToPosition(chessPiece, newPosition);
    }
}