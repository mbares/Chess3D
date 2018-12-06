using System.Collections.Generic;

public class RookMovement : ChessPieceMovement
{
    public override List<ChessboardPosition> GetAvailablePositions()
    {
        availablePositions.Clear();

        GetAvailablePositionsInDirection(gameManager.currentPlayer.forward);
        GetAvailablePositionsInDirection(gameManager.currentPlayer.backward);
        GetAvailablePositionsInDirection(gameManager.currentPlayer.left);
        GetAvailablePositionsInDirection(gameManager.currentPlayer.right);

        return availablePositions;
    }
}
