using System.Collections.Generic;

public class KingMovement : ChessPieceMovement
{
    public override List<ChessboardPosition> GetAvailablePositions()
    {
        availablePositions.Clear();

        GetAvailableKingPositionsInDirection(chessPiece.controllingPlayer.forward);
        GetAvailableKingPositionsInDirection(chessPiece.controllingPlayer.forwardLeft);
        GetAvailableKingPositionsInDirection(chessPiece.controllingPlayer.forwardRight);
        GetAvailableKingPositionsInDirection(chessPiece.controllingPlayer.backward);
        GetAvailableKingPositionsInDirection(chessPiece.controllingPlayer.backwardLeft);
        GetAvailableKingPositionsInDirection(chessPiece.controllingPlayer.backwardRight);
        GetAvailableKingPositionsInDirection(chessPiece.controllingPlayer.left);
        GetAvailableKingPositionsInDirection(chessPiece.controllingPlayer.right);

        return base.GetAvailablePositions();
    }

    public override List<ChessboardPosition> GetAvailableCapturePositions()
    {
        GetCapturableKingPositionsInDirection(chessPiece.controllingPlayer.forward);
        GetCapturableKingPositionsInDirection(chessPiece.controllingPlayer.forwardLeft);
        GetCapturableKingPositionsInDirection(chessPiece.controllingPlayer.forwardRight);
        GetCapturableKingPositionsInDirection(chessPiece.controllingPlayer.backward);
        GetCapturableKingPositionsInDirection(chessPiece.controllingPlayer.backwardLeft);
        GetCapturableKingPositionsInDirection(chessPiece.controllingPlayer.backwardRight);
        GetCapturableKingPositionsInDirection(chessPiece.controllingPlayer.left);
        GetCapturableKingPositionsInDirection(chessPiece.controllingPlayer.right);

        return capturablePositions;
    }

    private void GetAvailableKingPositionsInDirection(ChessboardPosition direction)
    {
        ChessboardPosition newPosition = currentPosition + direction;
        if (IsPositionValid(newPosition)) {
            availablePositions.Add(newPosition);
        }
    }

    private void GetCapturableKingPositionsInDirection(ChessboardPosition direction)
    {
        ChessboardPosition newPosition = currentPosition + direction;
        if (IsPositionValid(newPosition)) {
            capturablePositions.Add(newPosition);
        }
    }

}
