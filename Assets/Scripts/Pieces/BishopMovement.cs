using System.Collections.Generic;

public class BishopMovement : ChessPieceMovement
{
    public override List<ChessboardPosition> GetAvailablePositions()
    {
        availablePositions.Clear();

        GetAvailablePositionsInDirection(chessPiece.controllingPlayer.forwardLeft);
        GetAvailablePositionsInDirection(chessPiece.controllingPlayer.forwardRight);
        GetAvailablePositionsInDirection(chessPiece.controllingPlayer.backwardLeft);
        GetAvailablePositionsInDirection(chessPiece.controllingPlayer.backwardRight);

        return base.GetAvailablePositions();
    }

    public override List<ChessboardPosition> GetAvailableCapturePositions()
    {
        capturablePositions.Clear();

        GetCapturablePositionsInDirection(chessPiece.controllingPlayer.forwardLeft);
        GetCapturablePositionsInDirection(chessPiece.controllingPlayer.forwardRight);
        GetCapturablePositionsInDirection(chessPiece.controllingPlayer.backwardLeft);
        GetCapturablePositionsInDirection(chessPiece.controllingPlayer.backwardRight);

        return capturablePositions;
    }
}
