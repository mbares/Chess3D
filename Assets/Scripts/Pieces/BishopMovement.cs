using System.Collections.Generic;

public class BishopMovement : ChessPieceMovement
{
    public override List<ChessboardPosition> GetAvailablePositions()
    {
        availablePositions.Clear();

        GetAvailablePositionsInDirection(gameManager.currentPlayer.forwardLeft);
        GetAvailablePositionsInDirection(gameManager.currentPlayer.forwardRight);
        GetAvailablePositionsInDirection(gameManager.currentPlayer.backwardLeft);
        GetAvailablePositionsInDirection(gameManager.currentPlayer.backwardRight);

        return availablePositions;
    }
}
