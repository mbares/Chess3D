using UnityEngine;
using System.Collections.Generic;

public abstract class ChessPieceMovement : MonoBehaviour
{
    [HideInInspector]
    public ChessboardPosition simulatedPosition;

    [SerializeField]
    protected ChessPiece chessPiece;
    [SerializeField]
    protected GameManager gameManager;
    [SerializeField]
    protected ChessboardState chessboardState;

    protected List<ChessboardPosition> availablePositions = new List<ChessboardPosition>();
    protected List<ChessboardPosition> capturablePositions = new List<ChessboardPosition>();

    protected ChessboardPosition currentPosition;
    protected bool hasMovedOnce;

    public abstract List<ChessboardPosition> GetAvailableCapturePositions();

    public virtual List<ChessboardPosition> GetAvailablePositions()
    {
        for (int i = availablePositions.Count - 1; i >= 0; --i) {
            if (WillPositionCauseOwnCheck(availablePositions[i])) {
                availablePositions.RemoveAt(i);
            }
        }

        return availablePositions;
    }

    private void Start()
    {
        currentPosition = ChessboardPositionConverter.Vector3ToChessboardPosition(transform.localPosition);
        simulatedPosition = currentPosition;
    }

    public void SetHasMoved()
    {
        hasMovedOnce = true;
    }

    public void SetCurrentPosition(ChessboardPosition chessboardPosition)
    {
        currentPosition = chessboardPosition;
        simulatedPosition = chessboardPosition;
    }

    public bool IsPieceAtPositionOpponent(ChessboardPosition chessboardPosition)
    {
        return chessPiece.chessPieceInfo.color != chessboardState.GetChessPieceInfoAtPosition(chessboardPosition).color;
    }

    protected bool IsPositionOccupiedWithOpponent(ChessboardPosition chessboardPosition)
    {
        if (!ChessboardPositionValidator.IsPositionEmpty(chessboardPosition, chessboardState)) {
            return IsPieceAtPositionOpponent(chessboardPosition);
        }
        return false;
    }

    protected void GetAvailablePositionsInDirection(ChessboardPosition direction)
    {
        ChessboardPosition newPosition = currentPosition;

        newPosition += direction;
        while (ChessboardPositionValidator.IsPositionInBounds(newPosition)) {
            if (ChessboardPositionValidator.IsPositionEmpty(newPosition, chessboardState)) {
                availablePositions.Add(newPosition);
            } else if (IsPieceAtPositionOpponent(newPosition)) {
                availablePositions.Add(newPosition);
                break;
            } else {
                break;
            }

            newPosition += direction;
        }
    }

    protected void GetCapturablePositionsInDirection(ChessboardPosition direction)
    {
        ChessboardPosition newPosition = currentPosition;

        newPosition += direction;
        while (ChessboardPositionValidator.IsPositionInBounds(newPosition)) {
            if (ChessboardPositionValidator.IsPositionEmpty(newPosition, chessboardState)) {
                capturablePositions.Add(newPosition);
            } else {
                capturablePositions.Add(newPosition);
                break;
            }

            newPosition += direction;
        }
    }

    protected bool IsPositionValid(ChessboardPosition position)
    {
        if (ChessboardPositionValidator.IsPositionInBounds(position)) {
            if (ChessboardPositionValidator.IsPositionEmpty(position, chessboardState)) {
                return true;
            } else if (!ChessboardPositionValidator.IsPositionEmpty(position, chessboardState) && IsPieceAtPositionOpponent(position)) {
                return true;
            }
        }
        return false;
    }

    private bool WillPositionCauseOwnCheck(ChessboardPosition position)
    {
        simulatedPosition = position;

        chessboardState.RemovePieceDataFromPosition(currentPosition);

        ChessPiece temp = null;
        if (!ChessboardPositionValidator.IsPositionEmpty(position, chessboardState)) {
            temp = chessboardState.GetChessPieceAtPosition(position);
            chessboardState.RemovePieceDataFromPosition(position);
            chessboardState.activePiecesSet.Remove(temp.gameObject);
        }

        chessboardState.AddPieceDataToPosition(chessPiece, position);

        if (chessPiece.controllingPlayer.piecesColor == PieceColor.White) {
            gameManager.GetAllAvailableCapturePositionsOfBlackPlayer();
        } else {
            gameManager.GetAllAvailableCapturePositionsOfWhitePlayer();
        }

        bool isInCheck = chessboardState.DetectIfPlayerInCheck(chessPiece.controllingPlayer);

        chessboardState.RemovePieceDataFromPosition(position);

        if (temp != null) {
            chessboardState.AddPieceDataToPosition(temp, position);
            chessboardState.activePiecesSet.Add(temp.gameObject);
        }

        chessboardState.AddPieceDataToPosition(chessPiece, currentPosition);

        simulatedPosition = currentPosition;
        return isInCheck;
    }
}