using System.Collections.Generic;

public class QueenMovement : ChessPieceMovement
{
    public override List<ChessboardPosition> GetAvailablePositions()
    {
        availablePositions.Clear();

        GetAvailablePositionsInDirection(chessPiece.controllingPlayer.forward);
        GetAvailablePositionsInDirection(chessPiece.controllingPlayer.backward);
        GetAvailablePositionsInDirection(chessPiece.controllingPlayer.left);
        GetAvailablePositionsInDirection(chessPiece.controllingPlayer.right);
        GetAvailablePositionsInDirection(chessPiece.controllingPlayer.forwardLeft);
        GetAvailablePositionsInDirection(chessPiece.controllingPlayer.forwardRight);
        GetAvailablePositionsInDirection(chessPiece.controllingPlayer.backwardLeft);
        GetAvailablePositionsInDirection(chessPiece.controllingPlayer.backwardRight);

        return base.GetAvailablePositions();
    }

    public override List<ChessboardPosition> GetAvailableCapturePositions()
    {
        capturablePositions.Clear();

        GetCapturablePositionsInDirection(chessPiece.controllingPlayer.forward);
        GetCapturablePositionsInDirection(chessPiece.controllingPlayer.backward);
        GetCapturablePositionsInDirection(chessPiece.controllingPlayer.left);
        GetCapturablePositionsInDirection(chessPiece.controllingPlayer.right);
        GetCapturablePositionsInDirection(chessPiece.controllingPlayer.forwardLeft);
        GetCapturablePositionsInDirection(chessPiece.controllingPlayer.forwardRight);
        GetCapturablePositionsInDirection(chessPiece.controllingPlayer.backwardLeft);
        GetCapturablePositionsInDirection(chessPiece.controllingPlayer.backwardRight);

        return capturablePositions;
    }
}
