using UnityEngine;

[CreateAssetMenu]
public class Player : ScriptableObject
{
    public string playerName;
    public PieceColor piecesColor;
    public ChessboardPosition forward;
    public ChessboardPosition forwardLeft;
    public ChessboardPosition forwardRight;
    public ChessboardPosition left;
    public ChessboardPosition right;
    public ChessboardPosition backward;
    public ChessboardPosition backwardLeft;
    public ChessboardPosition backwardRight;
}