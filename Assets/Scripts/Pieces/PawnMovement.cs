public class PawnMovement : ChessPieceMovement
{
    protected override void GetAvailablePositions()
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
        if (ChessboardPositionValidator.IsPositionEmpty(newPosition, gameManager.chessboardState) && ChessboardPositionValidator.IsPositionInBounds(newPosition)) {
            availablePositions.Add(newPosition);
        }

        newPosition = currentPosition + gameManager.currentPlayer.forwardLeft;
        if (!ChessboardPositionValidator.IsPositionEmpty(newPosition, gameManager.chessboardState) && ChessboardPositionValidator.IsPositionInBounds(newPosition)) {
            if (IsPieceAtPositionOpponent(newPosition)) {
                availablePositions.Add(newPosition);
            }
        }

        newPosition = currentPosition + gameManager.currentPlayer.forwardRight;
        if (!ChessboardPositionValidator.IsPositionEmpty(newPosition, gameManager.chessboardState) && ChessboardPositionValidator.IsPositionInBounds(newPosition)) {
            if (IsPieceAtPositionOpponent(newPosition)) {
                availablePositions.Add(newPosition);
            }
        }
    }
}