using UnityEngine;

[CreateAssetMenu]
public class ChessboardPiecesLayout : ScriptableObject
{
    [System.Serializable]
    public class RowData
    {
        public PieceLayoutLabel[] row = new PieceLayoutLabel[8];
    }

    public RowData[] columns = new RowData[8];
}
