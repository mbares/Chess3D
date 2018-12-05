public class KnightMovement : ChessPieceMovement
{
    protected override void GetAvailablePositions()
    {
        GetAvailableKnightPositionsVertical(gameManager.currentPlayer.forward);
        GetAvailableKnightPositionsVertical(gameManager.currentPlayer.backward);
        GetAvailableKnightPositionsHorizontal(gameManager.currentPlayer.left);
        GetAvailableKnightPositionsHorizontal(gameManager.currentPlayer.right);
    }

    private void GetAvailableKnightPositionsVertical(ChessboardPosition direction)
    {
        ChessboardPosition newPosition1 = currentPosition + direction * 2 + gameManager.currentPlayer.left;
        ChessboardPosition newPosition2 = currentPosition + direction * 2 + gameManager.currentPlayer.right;

        AddAvailablePositionIfValid(newPosition1);
        AddAvailablePositionIfValid(newPosition2);
    }

    private void GetAvailableKnightPositionsHorizontal(ChessboardPosition direction)
    {
        ChessboardPosition newPosition1 = currentPosition + direction * 2 + gameManager.currentPlayer.forward;
        ChessboardPosition newPosition2 = currentPosition + direction * 2 + gameManager.currentPlayer.backward;

        AddAvailablePositionIfValid(newPosition1);
        AddAvailablePositionIfValid(newPosition2);
    }
}
