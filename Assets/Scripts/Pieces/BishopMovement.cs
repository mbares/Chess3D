public class BishopMovement : ChessPieceMovement
{
    protected override void GetAvailablePositions()
    {
        availablePositions.Clear();
        GetAvailablePositionsInDirection(gameManager.currentPlayer.forwardLeft);
        GetAvailablePositionsInDirection(gameManager.currentPlayer.forwardRight);
        GetAvailablePositionsInDirection(gameManager.currentPlayer.backwardLeft);
        GetAvailablePositionsInDirection(gameManager.currentPlayer.backwardRight);
    }
}
