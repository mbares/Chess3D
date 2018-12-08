using System.Collections.Generic;

public class PawnMovement : ChessPieceMovement
{
    public override List<ChessboardPosition> GetAvailablePositions()
    {
        availablePositions.Clear();

        ChessboardPosition newPosition;
        if (!hasMovedOnce) {
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
        if (ChessboardPositionValidator.IsPositionInBounds(newPosition) && IsPositionOccupiedWithOpponent(newPosition)) {
            availablePositions.Add(newPosition);
        }

        newPosition = currentPosition + chessPiece.controllingPlayer.forwardRight;
        if (ChessboardPositionValidator.IsPositionInBounds(newPosition) && IsPositionOccupiedWithOpponent(newPosition)) {
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
}