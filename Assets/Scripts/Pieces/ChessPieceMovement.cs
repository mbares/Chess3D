﻿using UnityEngine;
using System.Collections.Generic;

public abstract class ChessPieceMovement : MonoBehaviour
{
    [HideInInspector]
    public ChessboardPosition simulatedPosition;
    [HideInInspector]
    public ChessboardPosition currentPosition;
    [HideInInspector]
    public bool hasMoved;

    [SerializeField]
    protected ChessPiece chessPiece;
    [SerializeField]
    protected GameManager gameManager;
    [SerializeField]
    protected ChessboardStateManager chessboardState;

    protected List<ChessboardPosition> availablePositions = new List<ChessboardPosition>();
    protected List<ChessboardPosition> capturablePositions = new List<ChessboardPosition>();
 
    public abstract List<ChessboardPosition> GetAvailableCapturePositions();

    public virtual List<ChessboardPosition> GetAvailablePositions()
    {
        for (int i = availablePositions.Count - 1; i >= 0; --i) {
            if (IsPositionCausingOwnCheck(availablePositions[i])) {
                availablePositions.RemoveAt(i);
            }
        }

        return availablePositions;
    }

    private void OnDisable()
    {
        hasMoved = false;
    }

    protected virtual void OnEnable()
    {
        currentPosition = ChessboardPositionConverter.Vector3ToChessboardPosition(transform.localPosition);
        simulatedPosition = currentPosition;
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

    protected bool IsPositionCausingOwnCheck(ChessboardPosition position)
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

        bool isInCheck = chessboardState.IsPlayerInCheck(chessPiece.controllingPlayer);

        chessboardState.RemovePieceDataFromPosition(position);

        if (temp != null) {
            chessboardState.AddPieceDataToPosition(temp, position);
            chessboardState.activePiecesSet.Add(temp.gameObject);
        }

        chessboardState.AddPieceDataToPosition(chessPiece, currentPosition);

        simulatedPosition = currentPosition;
        return isInCheck;
    }

    public virtual void Move(ChessboardPosition position)
    {
        transform.localPosition = ChessboardPositionConverter.ChessboardPositionToVector3(position);
        currentPosition = position;
        simulatedPosition = position;
    }
}