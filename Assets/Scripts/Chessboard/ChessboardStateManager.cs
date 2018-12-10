using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "System/ChessboardStateManager")]
public class ChessboardStateManager : ScriptableObject
{
    private const int NUM_OF_COLUMNS = 8;
    private const int NUM_OF_ROWS = 8;

    public ChessPiecesSet activePiecesSet;

    [SerializeField]
    private CheckDetector checkDetector;
    [SerializeField]
    private ChessboardPiecesLayout unfinishedGameLayout;

    private ChessPiece[,] piecesOnChessboardData = new ChessPiece[NUM_OF_COLUMNS, NUM_OF_ROWS];

    public void SetUp()
    {
        checkDetector.SetKings(activePiecesSet.Items);
    }

    public bool IsPlayerInCheck(Player player)
    {
        return checkDetector.IsPlayerInCheck(player);
    }

    public ChessPieceInfo GetChessPieceInfoAtPosition(ChessboardPosition chessboardPosition)
    {
        int columnPosition = chessboardPosition.column;
        int rowPosition = chessboardPosition.row;
        if (piecesOnChessboardData[columnPosition, rowPosition] != null) {
            return piecesOnChessboardData[columnPosition, rowPosition].chessPieceInfo;
        } else {
            return null;
        }
    }

    public ChessPiece GetChessPieceAtPosition(ChessboardPosition chessboardPosition)
    {
        return piecesOnChessboardData[chessboardPosition.column, chessboardPosition.row];
    }

    public void UpdatePiecesOnChessboardData()
    {
        Array.Clear(piecesOnChessboardData, 0, piecesOnChessboardData.Length);

        List<GameObject> activePieces = activePiecesSet.Items;
        for (int i = 0; i < activePieces.Count; i++) {
            ChessPiece piece = activePieces[i].GetComponent<ChessPiece>();
            ChessboardPosition chessboardPosition = piece.GetChessboardPosition();
            piecesOnChessboardData[chessboardPosition.column, chessboardPosition.row] = piece;
        }
    }

    public void AddPieceDataToPosition(ChessPiece piece, ChessboardPosition chessboardPosition)
    {
        piecesOnChessboardData[chessboardPosition.column, chessboardPosition.row] = piece;
    }

    public void RemovePieceDataFromPosition(ChessboardPosition chessboardPosition)
    {
        piecesOnChessboardData[chessboardPosition.column, chessboardPosition.row] = null;
    }

    public void ClearPiecesOnChessboardData()
    {
        Array.Clear(piecesOnChessboardData, 0, piecesOnChessboardData.Length);
    }

    public void RemoveCapturedPieceAtPositionAndDeactivate(ChessboardPosition chessboardPosition)
    {
        ChessPiece capturedChessPiece = GetChessPieceAtPosition(chessboardPosition);
        capturedChessPiece.controllingPlayer.playerPieces.Remove(capturedChessPiece.gameObject);
        activePiecesSet.Remove(capturedChessPiece.gameObject);
        capturedChessPiece.Deactivate();
    }

    public ChessboardSquaresInfo[] GetUnfinishedChessboardSquaresInfo()
    {
        ChessboardSquaresInfo[] chessboardSquaresInfo = new ChessboardSquaresInfo[NUM_OF_COLUMNS];

        for (int i = 0; i < NUM_OF_COLUMNS; i++) {
            for (int j = 0; j < NUM_OF_ROWS; j++) {
                chessboardSquaresInfo[i] = new ChessboardSquaresInfo();
            }
        }

        for (int i = 0; i < activePiecesSet.Items.Count; i++) {
            ChessPiece piece = activePiecesSet.Items[i].GetComponent<ChessPiece>();
            ChessboardPosition chessboardPosition = piece.GetChessboardPosition();
            PieceLabel label = piece.chessPieceInfo.label;
            chessboardSquaresInfo[chessboardPosition.column].row[chessboardPosition.row] = label;
        }

        return chessboardSquaresInfo;
    }

    public void GetAllAvailableCapturePositionsOfPlayer(Player player)
    {
        player.availableCapturePositionsSet.Clear();
        for (int i = 0; i < activePiecesSet.Items.Count; ++i) {
            ChessPiece piece = activePiecesSet.Items[i].GetComponent<ChessPiece>();
            if (piece.controllingPlayer == player) {
                List<ChessboardPosition> availableCapturePositionsForPiece = piece.GetComponent<ChessPieceMovement>().GetAvailableCapturePositions();
                for (int j = 0; j < availableCapturePositionsForPiece.Count; ++j) {
                    player.availableCapturePositionsSet.Add(availableCapturePositionsForPiece[j]);
                }
            }
        }
    }

    public List<ChessboardPosition> GetAllAvailablePositionsOfPlayer(Player player)
    {
        List<ChessboardPosition> availablePositions = new List<ChessboardPosition>();

        for (int i = 0; i < activePiecesSet.Items.Count; ++i) {
            ChessPiece piece = activePiecesSet.Items[i].GetComponent<ChessPiece>();
            if (piece.controllingPlayer == player) {
                List<ChessboardPosition> availablePositionsForPiece = piece.GetComponent<ChessPieceMovement>().GetAvailablePositions();
                for (int j = 0; j < availablePositionsForPiece.Count; ++j) {
                    availablePositions.Add(availablePositionsForPiece[j]);
                }
            }
        }

        return availablePositions;
    }

    public void UpdatePlayerPiecesSet(Player player)
    {
        player.playerPieces.Clear();
        for (int i = 0; i < activePiecesSet.Items.Count; ++i) {
            if (activePiecesSet.Items[i].GetComponent<ChessPiece>().controllingPlayer == player) {
                player.playerPieces.Add(activePiecesSet.Items[i]);
            }
        }
    }
}