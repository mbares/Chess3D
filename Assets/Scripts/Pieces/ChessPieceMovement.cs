using UnityEngine;
using System.Collections.Generic;

public abstract class ChessPieceMovement : MonoBehaviour
{
    [SerializeField]
    protected ChessPiece chessPiece;
    [SerializeField]
    protected GameManager gameManager;
    [SerializeField]
    protected ChessboardState chessboardState;

    protected List<ChessboardPosition> availablePositions = new List<ChessboardPosition>();
    protected ChessboardPosition currentPosition;
    protected bool hasMovedOnce;

    public abstract List<ChessboardPosition> GetAvailablePositions();

    private void Start()
    {
        currentPosition = ChessboardPositionConverter.Vector3ToChessboardPosition(transform.localPosition);
    }

    public void SetHasMoved()
    {
        hasMovedOnce = true;
    }

    public void SetCurrentPosition(ChessboardPosition chessboardPosition)
    {
        currentPosition = chessboardPosition;
    }

    public bool IsPieceAtPositionOpponent(ChessboardPosition chessboardPosition)
    {
        return chessPiece.chessPieceInfo.color != chessboardState.GetChessPieceInfoAtPosition(chessboardPosition).color;
    }

    protected void GetAvailablePositionsInDirection(ChessboardPosition direction)
    {
        ChessboardPosition newPosition = currentPosition;

        newPosition += direction;
        while (ChessboardPositionValidator.IsPositionInBounds(newPosition)) {
            if (ChessboardPositionValidator.IsPositionEmpty(newPosition, chessboardState)) {
                availablePositions.Add(newPosition);
            } else if (!ChessboardPositionValidator.IsPositionEmpty(newPosition, chessboardState) && IsPieceAtPositionOpponent(newPosition)) {
                availablePositions.Add(newPosition);
                break;
            } else if (!ChessboardPositionValidator.IsPositionEmpty(newPosition, chessboardState)) {
                break;
            }

            newPosition += direction;
        }
    }

    protected void AddAvailablePositionIfValid(ChessboardPosition position)
    {
        if (ChessboardPositionValidator.IsPositionInBounds(position)) {
            if (ChessboardPositionValidator.IsPositionEmpty(position, chessboardState)) {
                availablePositions.Add(position);
            } else if (!ChessboardPositionValidator.IsPositionEmpty(position, chessboardState) && IsPieceAtPositionOpponent(position)) {
                availablePositions.Add(position);
            }
        }
    }
}