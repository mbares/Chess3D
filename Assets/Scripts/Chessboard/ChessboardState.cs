using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu]
public class ChessboardState : ScriptableObject
{
    private const int NUM_OF_COLUMNS = 8;
    private const int NUM_OF_ROWS = 8;

    [SerializeField]
    private ChessPiecesSet activePiecesSet;
    [SerializeField]
    private ChessboardPiecesLayout unfinishedGameLayout;

    private GameState unfinishedGameState;
    private ChessPiece[,] piecesOnChessboardData = new ChessPiece[NUM_OF_COLUMNS, NUM_OF_ROWS];

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
            ChessboardPosition chessboardPosition = ChessboardPositionConverter.Vector3ToChessboardPosition(activePieces[i].transform.localPosition);
            piecesOnChessboardData[chessboardPosition.column, chessboardPosition.row] = activePieces[i].GetComponent<ChessPiece>(); 
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

    public ChessboardSquaresInfo[] GetUnfinishedChessboardSquaresInfo()
    {
        ChessboardSquaresInfo[] chessboardSquaresInfo = new ChessboardSquaresInfo[NUM_OF_COLUMNS];

        for (int i = 0; i < NUM_OF_COLUMNS; i++) {
            for (int j = 0; j < NUM_OF_ROWS; j++) {
                chessboardSquaresInfo[i] = new ChessboardSquaresInfo();
            }
        }

        for (int i = 0; i < activePiecesSet.Items.Count; i++) {
            ChessboardPosition chessboardPosition = ChessboardPositionConverter.Vector3ToChessboardPosition(activePiecesSet.Items[i].transform.localPosition);
            PieceLayoutLabel label = activePiecesSet.Items[i].GetComponent<ChessPiece>().chessPieceInfo.label;
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
}