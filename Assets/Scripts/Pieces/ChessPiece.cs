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

    public void Activate()
    {
        inactivePiecesSet.Remove(gameObject);
        gameObject.SetActive(true);
        controllingPlayer.playerPieces.Add(gameObject);
    }

    public void Deactivate()
    {
        inactivePiecesSet.Add(gameObject);
        gameObject.SetActive(false);
        controllingPlayer.playerPieces.Remove(gameObject);
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
    public PieceLabel label;
}

public enum PieceType
{
    Pawn, Rook, Knight, Bishop, Queen, King
}

public enum PieceColor
{
    White, Black
}

public enum PieceLabel
{
    Empty, WP, WR, WN, WB, WQ, WK,
    BP, BR, BN, BB, BQ, BK
}