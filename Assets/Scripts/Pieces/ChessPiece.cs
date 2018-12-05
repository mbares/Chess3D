using UnityEngine;

public class ChessPiece : MonoBehaviour
{
    public ChessPieceInfo chessPieceInfo;

    [SerializeField]
    private ChessPiecesSet inactivePiecesSet;

    private void Awake()
    {
        inactivePiecesSet.Add(gameObject);
        gameObject.SetActive(false);
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
    Empty, WPwn, WRk, WKn, WBsh, WQn, WKng,
    BPwn, BRk, BKn, BBsh, BQn, BKng
}