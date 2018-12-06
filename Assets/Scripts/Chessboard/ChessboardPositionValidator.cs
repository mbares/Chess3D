public static class ChessboardPositionValidator
{
    private const int MIN_POSITION = 0;
    private const int MAX_POSITION = 7;

    public static bool IsPositionEmpty(ChessboardPosition chessboardPosition, ChessboardState chessboardState)
    {
        return chessboardState.GetChessPieceInfoAtPosition(chessboardPosition) == null;
    }

    public static bool IsPositionInBounds(ChessboardPosition chessboardPosition)
    {
        int column = chessboardPosition.column;
        int row = chessboardPosition.row;

        if (column < MIN_POSITION || column > MAX_POSITION || row < MIN_POSITION || row > MAX_POSITION) {
            return false;
        } else {
            return true;
        }
    }
}
