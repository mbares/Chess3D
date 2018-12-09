using UnityEngine;

[CreateAssetMenu(menuName = "Data/ChessboardLayout")]
public class ChessboardPiecesLayout : ScriptableObject
{
    private const int NUM_OF_COLUMNS = 8;

    public ChessboardSquaresInfo[] chessboardSquaresInfo = new ChessboardSquaresInfo[NUM_OF_COLUMNS];
}

[System.Serializable]
public class ChessboardSquaresInfo
{
    private const int NUM_OF_ROWS = 8;

    public PieceLabel[] row = new PieceLabel[NUM_OF_ROWS];

    public ChessboardSquaresInfo ()
    {
        for (int i = 0; i < row.Length; i++) {
            row[i] = PieceLabel.Empty;
        }
    }
}