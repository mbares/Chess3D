using System.Collections.Generic;

public class RookMovement : ChessPieceMovement
{
    public override List<ChessboardPosition> GetAvailablePositions()
    {
        availablePositions.Clear();

        GetAvailablePositionsInDirection(chessPiece.controllingPlayer.forward);
        GetAvailablePositionsInDirection(chessPiece.controllingPlayer.backward);
        GetAvailablePositionsInDirection(chessPiece.controllingPlayer.left);
        GetAvailablePositionsInDirection(chessPiece.controllingPlayer.right);

        return base.GetAvailablePositions();
    }

    public override List<ChessboardPosition> GetAvailableCapturePositions()
    {
        capturablePositions.Clear();

        GetCapturablePositionsInDirection(chessPiece.controllingPlayer.forward);
        GetCapturablePositionsInDirection(chessPiece.controllingPlayer.backward);
        GetCapturablePositionsInDirection(chessPiece.controllingPlayer.left);
        GetCapturablePositionsInDirection(chessPiece.controllingPlayer.right);

        return capturablePositions;
    }
}
