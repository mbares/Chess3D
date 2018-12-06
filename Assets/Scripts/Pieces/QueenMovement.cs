using System.Collections.Generic;

public class QueenMovement : ChessPieceMovement
{
    public override List<ChessboardPosition> GetAvailablePositions()
    {
        availablePositions.Clear();

        GetAvailablePositionsInDirection(gameManager.currentPlayer.forward);
        GetAvailablePositionsInDirection(gameManager.currentPlayer.backward);
        GetAvailablePositionsInDirection(gameManager.currentPlayer.left);
        GetAvailablePositionsInDirection(gameManager.currentPlayer.right);
        GetAvailablePositionsInDirection(gameManager.currentPlayer.forwardLeft);
        GetAvailablePositionsInDirection(gameManager.currentPlayer.forwardRight);
        GetAvailablePositionsInDirection(gameManager.currentPlayer.backwardLeft);
        GetAvailablePositionsInDirection(gameManager.currentPlayer.backwardRight);

        return availablePositions;
    }
}
