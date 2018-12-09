using System.Collections.Generic;
using UnityEngine;

public class BishopMovement : ChessPieceMovement
{
    [HideInInspector]
    public PieceColor availableSquaresColor;

    protected override void OnEnable()
    {
        base.OnEnable();
        SetAvailableSquaresColor();
    }

    public override List<ChessboardPosition> GetAvailablePositions()
    {
        availablePositions.Clear();

        GetAvailablePositionsInDirection(chessPiece.controllingPlayer.forwardLeft);
        GetAvailablePositionsInDirection(chessPiece.controllingPlayer.forwardRight);
        GetAvailablePositionsInDirection(chessPiece.controllingPlayer.backwardLeft);
        GetAvailablePositionsInDirection(chessPiece.controllingPlayer.backwardRight);

        return base.GetAvailablePositions();
    }

    public override List<ChessboardPosition> GetAvailableCapturePositions()
    {
        capturablePositions.Clear();

        GetCapturablePositionsInDirection(chessPiece.controllingPlayer.forwardLeft);
        GetCapturablePositionsInDirection(chessPiece.controllingPlayer.forwardRight);
        GetCapturablePositionsInDirection(chessPiece.controllingPlayer.backwardLeft);
        GetCapturablePositionsInDirection(chessPiece.controllingPlayer.backwardRight);

        return capturablePositions;
    }

    private void SetAvailableSquaresColor()
    {
        if (currentPosition.column % 2 == 0) {
            if (currentPosition.row % 2 == 0) {
                availableSquaresColor = PieceColor.Black;
            } else {
                availableSquaresColor = PieceColor.White;
            }
        } else {
            if (currentPosition.row % 2 == 0) {
                availableSquaresColor = PieceColor.White;
            } else {
                availableSquaresColor = PieceColor.Black;
            }
        }
    }
}
