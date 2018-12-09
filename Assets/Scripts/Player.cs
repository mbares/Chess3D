using UnityEngine;

[CreateAssetMenu(menuName = "Data/Player")]
public class Player : ScriptableObject
{
    public string playerName;
    public PieceColor piecesColor;
    public PlayerAvailablePositionsSet availablePositionsSet;
    public PlayerAvailablePositionsSet opponentAvailablePositionsSet;
    public ChessboardPosition forward;
    public ChessboardPosition forwardLeft;
    public ChessboardPosition forwardRight;
    public ChessboardPosition left;
    public ChessboardPosition right;
    public ChessboardPosition backward;
    public ChessboardPosition backwardLeft;
    public ChessboardPosition backwardRight;

    private bool inCheck = false;

    public bool IsInCheck()
    {
        return inCheck;
    }

    public void SetIsInCheck(bool value)
    {
        inCheck = value;
    }
}