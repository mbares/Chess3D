using UnityEngine;

public class ChessPiece : MonoBehaviour
{
    public ChessPieceInfo chessPieceInfo;
    public Player controllingPlayer;

    [SerializeField]
    private ChessPiecesSet inactivePiecesSet;

    private void Awake()
    {
        Deactivate();
    }

    public void Deactivate()
    {
        inactivePiecesSet.Add(gameObject);
        gameObject.SetActive(false);
    }

    public ChessboardPosition GetChessboardPosition()
    {
        return ChessboardPositionConverter.Vector3ToChessboardPosition(transform.localPosition);
    }
}

[System.Serializable]
public class ChessPieceInfo
{
    public PieceColor color;
    public PieceType type;
    public PieceLayoutLabel label;
}

public enum PieceType
{
    Pawn, Rook, Knight, Bishop, Queen, King
}

public enum PieceColor
{
    White, Black
}

public enum PieceLayoutLabel
{
    Empty, WPwn, WRk, Wknt, WBsh, WQn, WKng,
    BPwn, BRk, BKnt, BBsh, BQn, BKng
}