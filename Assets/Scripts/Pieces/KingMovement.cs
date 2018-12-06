using System.Collections.Generic;

public class KingMovement : ChessPieceMovement
{
    public override List<ChessboardPosition> GetAvailablePositions()
    {
        availablePositions.Clear();

        GetAvailableKingPositionsInDirection(gameManager.currentPlayer.forward);
        GetAvailableKingPositionsInDirection(gameManager.currentPlayer.forwardLeft);
        GetAvailableKingPositionsInDirection(gameManager.currentPlayer.forwardRight);
        GetAvailableKingPositionsInDirection(gameManager.currentPlayer.backward);
        GetAvailableKingPositionsInDirection(gameManager.currentPlayer.backwardLeft);
        GetAvailableKingPositionsInDirection(gameManager.currentPlayer.backwardRight);
        GetAvailableKingPositionsInDirection(gameManager.currentPlayer.left);
        GetAvailableKingPositionsInDirection(gameManager.currentPlayer.right);

        return availablePositions;
    }

    private void GetAvailableKingPositionsInDirection(ChessboardPosition direction)
    {
        ChessboardPosition newPosition = currentPosition + direction;
        AddAvailablePositionIfValid(newPosition);
    }
}
