using System.Collections.Generic;
using UnityEngine;

public class PawnMovement : ChessPieceMovement
{
    [HideInInspector]
    public bool awaitingPromotion = false;

    [SerializeField]
    private GameEvent pawnPromotionEvent;
    [SerializeField]
    private EnPassantManager enPassantManager;
    [SerializeField]
    private ReplayData replayData;
    [SerializeField]
    private PawnPromotionManager pawnPromotionManager;

    public override List<ChessboardPosition> GetAvailablePositions()
    {
        availablePositions.Clear();

        ChessboardPosition newPosition;
        if (!hasMoved) {
            newPosition = currentPosition + chessPiece.controllingPlayer.forward * 2;
            if (ChessboardPositionValidator.IsPositionEmpty(newPosition, gameManager.chessboardState)) {
                availablePositions.Add(newPosition);
            }
        }

        newPosition = currentPosition + chessPiece.controllingPlayer.forward;
        if (ChessboardPositionValidator.IsPositionInBounds(newPosition) && ChessboardPositionValidator.IsPositionEmpty(newPosition, gameManager.chessboardState)) {
            availablePositions.Add(newPosition);
        }

        newPosition = currentPosition + chessPiece.controllingPlayer.forwardLeft;
        if (ChessboardPositionValidator.IsPositionInBounds(newPosition) && (IsPositionOccupiedWithOpponent(newPosition) || newPosition.Equals(enPassantManager.enPassantPosition))) {
            availablePositions.Add(newPosition);
        }

        newPosition = currentPosition + chessPiece.controllingPlayer.forwardRight;
        if (ChessboardPositionValidator.IsPositionInBounds(newPosition) && (IsPositionOccupiedWithOpponent(newPosition) || newPosition.Equals(enPassantManager.enPassantPosition))) {
            availablePositions.Add(newPosition);
        }

        return base.GetAvailablePositions();
    }

    public override List<ChessboardPosition> GetAvailableCapturePositions()
    {
        List<ChessboardPosition> capturePositions = new List<ChessboardPosition>();

        ChessboardPosition newPosition = currentPosition + chessPiece.controllingPlayer.forwardLeft;
        if (ChessboardPositionValidator.IsPositionInBounds(newPosition)) {
            capturePositions.Add(newPosition);
        }

        newPosition = currentPosition + chessPiece.controllingPlayer.forwardRight;
        if (ChessboardPositionValidator.IsPositionInBounds(newPosition)) {
            capturePositions.Add(newPosition);
        }

        return capturePositions;
    }

    public override void Move(ChessboardPosition position)
    {
        if ((position.row - currentPosition.row) == 2) {
            ActivateEnPassant(position);
        }

        base.Move(position);
        if (chessPiece.chessPieceInfo.color == PieceColor.White && position.row == ChessboardPositionValidator.MAX_POSITION) {
            awaitingPromotion = true;
            if (gameManager.IsPlayerInteractionAllowed()) {
                pawnPromotionEvent.Raise();
            } else {
                ReplayPromote(position);
            }
        } else if (chessPiece.chessPieceInfo.color == PieceColor.Black && position.row == ChessboardPositionValidator.MIN_POSITION) {
            awaitingPromotion = true;
            if (gameManager.IsPlayerInteractionAllowed()) {
                pawnPromotionEvent.Raise();
            } else {
                ReplayPromote(position);
            }
        }
    }

    private void ReplayPromote(ChessboardPosition position)
    {
        foreach (KeyValuePair<ChessboardPosition, PieceType> entry in replayData.playerMoves.pawnPromotions) {
            if (entry.Key.Equals(position)) {
                pawnPromotionManager.PromotePawn(entry.Value);
            }
        }
    }

    private void ActivateEnPassant(ChessboardPosition position)
    {
        if (position.column - 1 >= ChessboardPositionValidator.MIN_POSITION) {
            ChessboardPosition adjacentLeftPosition = new ChessboardPosition(position.column - 1, position.row);
            ChessPiece adjacentChessPiece = chessboardState.GetChessPieceAtPosition(adjacentLeftPosition);
            if (adjacentChessPiece != null && adjacentChessPiece.chessPieceInfo.type == PieceType.Pawn && adjacentChessPiece.controllingPlayer != chessPiece.controllingPlayer) {
                enPassantManager.enPassantPosition = new ChessboardPosition(position.column, position.row - 1);
                enPassantManager.pieceCausingEnPassant = chessPiece;
                return;
            }
        }

        if (position.column + 1 <= ChessboardPositionValidator.MAX_POSITION) {
            ChessboardPosition adjacentRightPosition = new ChessboardPosition(position.column + 1, position.row);
            ChessPiece adjacentChessPiece = chessboardState.GetChessPieceAtPosition(adjacentRightPosition);
            if (adjacentChessPiece != null && adjacentChessPiece.chessPieceInfo.type == PieceType.Pawn && adjacentChessPiece.controllingPlayer != chessPiece.controllingPlayer) {
                enPassantManager.enPassantPosition = new ChessboardPosition(position.column, position.row - 1);
                enPassantManager.pieceCausingEnPassant = chessPiece;
            }
        }
    }
}