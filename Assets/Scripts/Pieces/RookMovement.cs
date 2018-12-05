public class RookMovement : ChessPieceMovement
{
    protected override void GetAvailablePositions()
    {
        availablePositions.Clear();
        GetAvailablePositionsInDirection(gameManager.currentPlayer.forward);
        GetAvailablePositionsInDirection(gameManager.currentPlayer.backward);
        GetAvailablePositionsInDirection(gameManager.currentPlayer.left);
        GetAvailablePositionsInDirection(gameManager.currentPlayer.right);
    }
}
