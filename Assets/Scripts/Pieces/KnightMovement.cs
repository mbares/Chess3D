using System.Collections.Generic;

public class KnightMovement : ChessPieceMovement
{
    public override List<ChessboardPosition> GetAvailablePositions()
    {
        availablePositions.Clear();

        AddAvailablePositionIfValid(GetKnightPositionsVertical(chessPiece.controllingPlayer.forward));
        AddAvailablePositionIfValid(GetKnightPositionsVertical(chessPiece.controllingPlayer.backward));
        AddAvailablePositionIfValid(GetKnightPositionsHorizontal(chessPiece.controllingPlayer.left));
        AddAvailablePositionIfValid(GetKnightPositionsHorizontal(chessPiece.controllingPlayer.right));

        return base.GetAvailablePositions();
    }

    public override List<ChessboardPosition> GetAvailableCapturePositions()
    {
        capturablePositions.Clear();

        AddCapturablePositionIfValid(GetKnightPositionsVertical(chessPiece.controllingPlayer.forward));
        AddCapturablePositionIfValid(GetKnightPositionsVertical(chessPiece.controllingPlayer.backward));
        AddCapturablePositionIfValid(GetKnightPositionsHorizontal(chessPiece.controllingPlayer.left));
        AddCapturablePositionIfValid(GetKnightPositionsHorizontal(chessPiece.controllingPlayer.right));

        return capturablePositions;
    }

    private ChessboardPosition[] GetKnightPositionsVertical(ChessboardPosition direction)
    {
        ChessboardPosition[] positions = {
            currentPosition + direction * 2 + chessPiece.controllingPlayer.left,
            currentPosition + direction * 2 + chessPiece.controllingPlayer.right
        };

        return positions;
    }

    private ChessboardPosition[] GetKnightPositionsHorizontal(ChessboardPosition direction)
    {
        ChessboardPosition[] positions = {
            currentPosition + direction * 2 + chessPiece.controllingPlayer.forward,
            currentPosition + direction * 2 + chessPiece.controllingPlayer.backward
        };

        return positions;
    }

    private void AddAvailablePositionIfValid(ChessboardPosition[] positions)
    {
        for (int i = 0; i < positions.Length; ++i) {
            if (IsPositionValid(positions[i])) {
                availablePositions.Add(positions[i]);
            }
        }
    }

    private void AddCapturablePositionIfValid(ChessboardPosition[] positions)
    {
        for (int i = 0; i < positions.Length; ++i) {
            if (ChessboardPositionValidator.IsPositionInBounds(positions[i])) {
                availablePositions.Add(positions[i]);
            }
        }
    }
}
