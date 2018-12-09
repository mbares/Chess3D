using UnityEngine;
using System;

public static class ChessboardPositionConverter
{
    private const float SQUARE_SIZE = 1f;

    public static Vector3 ChessboardPositionToVector3(ChessboardPosition chessboardPosition)
    {
        return new Vector3(chessboardPosition.column * SQUARE_SIZE, 0f, chessboardPosition.row * SQUARE_SIZE);
    }

    public static ChessboardPosition StringToChessboardPosition(string data)
    {
        string columnData = data.Substring(0, 1);
        string rowData = data.Substring(1, 1);
        int row;
        int.TryParse(rowData, out row);
        int column = Array.IndexOf(ChessboardPosition.columnLabels, columnData);
        return new ChessboardPosition(column, row);
    }

    public static ChessboardPosition Vector3ToChessboardPosition(Vector3 position)
    {
        int column = Mathf.RoundToInt(position.x / SQUARE_SIZE);
        int row = Mathf.RoundToInt(position.z / SQUARE_SIZE);
        return new ChessboardPosition(column, row);
    }
}