public class KingMovement : ChessPieceMovement
{
    protected override void GetAvailablePositions()
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
    }

    private void GetAvailableKingPositionsInDirection(ChessboardPosition direction)
    {
        ChessboardPosition newPosition = currentPosition + direction;
        AddAvailablePositionIfValid(newPosition);
    }
}
