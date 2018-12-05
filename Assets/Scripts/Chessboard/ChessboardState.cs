using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu]
public class ChessboardState : ScriptableObject
{
    [SerializeField]
    private ChessPiecesSet activePiecesSet;
    private ChessPiece[,] chessboardSquares = new ChessPiece[8, 8];

    public ChessPieceInfo GetChessboardSquareInfo(ChessboardPosition chessboardPosition)
    {
        int columnPosition = chessboardPosition.column;
        int rowPosition = chessboardPosition.row;
        if (chessboardSquares[columnPosition, rowPosition] != null) {
            return chessboardSquares[columnPosition, rowPosition].chessPieceInfo;
        } else {
            return null;
        }
    }

    public ChessPiece GetChessPieceAtPosition(ChessboardPosition chessboardPosition)
    {
        int columnPosition = chessboardPosition.column;
        int rowPosition = chessboardPosition.row;
        return chessboardSquares[columnPosition, rowPosition];
    }

    public void UpdateChessboardSquaresInfo()
    {
        Array.Clear(chessboardSquares, 0, chessboardSquares.Length);

        List<GameObject> activePieces = activePiecesSet.Items;
        for (int i = 0; i < activePieces.Count; i++) {
            int column = Mathf.FloorToInt(activePieces[i].transform.localPosition.x);
            int row = Mathf.FloorToInt(activePieces[i].transform.localPosition.z);
            chessboardSquares[column, row] = activePieces[i].GetComponent<ChessPiece>(); 
        }
    }
}