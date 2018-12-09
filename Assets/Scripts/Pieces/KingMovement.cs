using System.Collections.Generic;

public class KingMovement : ChessPieceMovement
{
    private ChessPieceMovement kingsideRook;
    private ChessPieceMovement queensideRook;
    private ChessboardPosition kingsideCastlingRookPosition;
    private ChessboardPosition queensideCastlingRookPosition;
    private ChessboardPosition kingsideCastlingKingPosition;
    private ChessboardPosition queensideCastlingKingPosition;

    public ChessPieceMovement GetKingsideRook()
    {
        return kingsideRook;
    }

    public ChessPieceMovement GetQueensideRook()
    {
        return queensideRook;
    }

    public ChessboardPosition GetKingsideCastlingRookPosition()
    {
        return kingsideCastlingRookPosition;
    }

    public ChessboardPosition GetQueensideCastlingRookPosition()
    {
        return queensideCastlingRookPosition;
    }

    public ChessboardPosition GetKingsideCastlingKingPosition()
    {
        return kingsideCastlingKingPosition;
    }

    public ChessboardPosition GetQueensideCastlingKingPosition()
    {
        return queensideCastlingKingPosition;
    }

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
        GetKingsideCastlingIfValid();
        GetQueensideastlingIfValid();

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

    private void GetKingsideCastlingIfValid()
    {
        ChessboardPosition direction = chessPiece.controllingPlayer.right;
        ChessPiece rook = chessboardState.GetChessPieceAtPosition(new ChessboardPosition(ChessboardPositionValidator.MAX_POSITION, currentPosition.row));
        bool isPassingPositionValid = availablePositions.Contains(currentPosition + direction) && !IsPositionCausingOwnCheck(currentPosition + direction);
        if (rook != null && rook.chessPieceInfo.type == PieceType.Rook && !rook.GetComponent<ChessPieceMovement>().hasMoved && !hasMoved && isPassingPositionValid) {
            ChessboardPosition newPosition = currentPosition + direction * 2;
            if (ChessboardPositionValidator.IsPositionEmpty(newPosition, chessboardState) && !IsPositionCausingOwnCheck(newPosition)) {
                availablePositions.Add(newPosition);
                kingsideCastlingKingPosition = newPosition;
                kingsideRook = rook.GetComponent<ChessPieceMovement>();
                kingsideCastlingRookPosition = new ChessboardPosition(kingsideCastlingKingPosition.column - 1, kingsideCastlingKingPosition.row);
            }
        } else {
            kingsideCastlingKingPosition = null;
            kingsideRook = null;
            kingsideCastlingRookPosition = null;
        }
    }

    private void GetQueensideastlingIfValid()
    {
        ChessboardPosition direction = chessPiece.controllingPlayer.left;
        ChessPiece rook = chessboardState.GetChessPieceAtPosition(new ChessboardPosition(ChessboardPositionValidator.MIN_POSITION, currentPosition.row));
        bool isPassingPositionValid = availablePositions.Contains(currentPosition + direction) && !IsPositionCausingOwnCheck(currentPosition + direction);
        if (rook != null && rook.chessPieceInfo.type == PieceType.Rook && !rook.GetComponent<ChessPieceMovement>().hasMoved && !hasMoved && isPassingPositionValid) {
            ChessboardPosition newPosition = currentPosition + direction * 2;
            if (ChessboardPositionValidator.IsPositionEmpty(newPosition, chessboardState) && !IsPositionCausingOwnCheck(newPosition)) {
                ChessboardPosition rookPassingPosition = currentPosition + direction * 3;
                if (ChessboardPositionValidator.IsPositionEmpty(rookPassingPosition, chessboardState)) {
                    availablePositions.Add(newPosition);
                    queensideCastlingKingPosition = newPosition;
                    queensideRook = rook.GetComponent<ChessPieceMovement>();
                    queensideCastlingRookPosition = new ChessboardPosition(queensideCastlingKingPosition.column + 1, queensideCastlingKingPosition.row);
                }
            }
        } else {
            queensideCastlingKingPosition = null;
            queensideRook = null;
            queensideCastlingRookPosition = null;
        }
    }
}