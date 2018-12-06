using System.Collections.Generic;

public class PawnMovement : ChessPieceMovement
{
    public override List<ChessboardPosition> GetAvailablePositions()
    {
        availablePositions.Clear();

        ChessboardPosition newPosition;
        if (!hasMovedOnce) {
            newPosition = currentPosition + gameManager.currentPlayer.forward * 2;
            if (ChessboardPositionValidator.IsPositionEmpty(newPosition, gameManager.chessboardState)) {
                availablePositions.Add(newPosition);
            }
        }

        newPosition = currentPosition + gameManager.currentPlayer.forward;
        if (ChessboardPositionValidator.IsPositionInBounds(newPosition) && ChessboardPositionValidator.IsPositionEmpty(newPosition, gameManager.chessboardState)) {
            availablePositions.Add(newPosition);
        }

        newPosition = currentPosition + gameManager.currentPlayer.forwardLeft;
        if (ChessboardPositionValidator.IsPositionInBounds(newPosition) && !ChessboardPositionValidator.IsPositionEmpty(newPosition, gameManager.chessboardState)) {
            if (IsPieceAtPositionOpponent(newPosition)) {
                availablePositions.Add(newPosition);
            }
        }

        newPosition = currentPosition + gameManager.currentPlayer.forwardRight;
        if (ChessboardPositionValidator.IsPositionInBounds(newPosition) && !ChessboardPositionValidator.IsPositionEmpty(newPosition, gameManager.chessboardState)) {
            if (IsPieceAtPositionOpponent(newPosition)) {
                availablePositions.Add(newPosition);
            }
        }

        return availablePositions;
    }
}