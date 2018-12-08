using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu]
public class ChessboardState : ScriptableObject
{
    private const int NUM_OF_COLUMNS = 8;
    private const int NUM_OF_ROWS = 8;

    public ChessPiecesSet activePiecesSet;

    [SerializeField]
    private CheckDetector checkDetector;
    [SerializeField]
    private ChessboardPiecesLayout unfinishedGameLayout;

    private GameState unfinishedGameState;
    private ChessPiece[,] piecesOnChessboardData = new ChessPiece[NUM_OF_COLUMNS, NUM_OF_ROWS];

    public void SetUp()
    {
        checkDetector.SetKings(activePiecesSet.Items);
    }

    public bool DetectIfPlayerInCheck(Player player)
    {
        return checkDetector.DetectIfPlayerInCheck(player);
    }

    public ChessPiece GetPieceThatIsCausingCheck(Player checkedPlayer)
    {
        return checkDetector.GetPieceThatIsCausingCheck(activePiecesSet.Items, checkedPlayer);
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
            PieceLayoutLabel label = piece.chessPieceInfo.label;
            chessboardSquaresInfo[chessboardPosition.column].row[chessboardPosition.row] = label;
        }

        return chessboardSquaresInfo;
    }

    public void SetUnfinishedGameLayout()
    {
        if (unfinishedGameState == null) {
            LoadUnfinishedGameState();
        }

        unfinishedGameLayout.chessboardSquaresInfo = unfinishedGameState.chessboardSquaresInfo;
    }

    public PieceColor GetUnfinishedGameCurrentPlayerColor()
    {
        if (unfinishedGameState == null) {
            LoadUnfinishedGameState();
        }
        return unfinishedGameState.currentPlayerColor;
    }

    private void LoadUnfinishedGameState()
    {
        unfinishedGameState = GameStateSerializer.LoadGameState();
    }

    public void GetAllAvailableCapturePositionsOfPlayer(Player player)
    {
        player.availablePositionsSet.Clear();
        for (int i = 0; i < activePiecesSet.Items.Count; ++i) {
            ChessPiece piece = activePiecesSet.Items[i].GetComponent<ChessPiece>();
            if (piece.controllingPlayer == player) {
                List<ChessboardPosition> availablePositionsForPiece = piece.GetComponent<ChessPieceMovement>().GetAvailableCapturePositions();
                for (int j = 0; j < availablePositionsForPiece.Count; ++j) {
                    player.availablePositionsSet.Add(availablePositionsForPiece[j]);
                }
            }
        }
    }
}