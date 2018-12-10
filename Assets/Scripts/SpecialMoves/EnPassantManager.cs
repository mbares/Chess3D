using UnityEngine;

[CreateAssetMenu(menuName = "System/EnPassantManager")]
public class EnPassantManager : ScriptableObject
{
    [HideInInspector]
    public ChessboardPosition enPassantPosition = null;
    [HideInInspector]
    public ChessPiece pieceCausingEnPassant = null;

    private int counter = 0;

    private void OnEnable()
    {
        Reset();
    }

    public void ClearEnPassantPosition()
    {
        if (enPassantPosition != null) {
            counter++;

            if (counter == 2) {
                Reset();
            }
        }
    }

    public void SetEnPassantPosition(ChessboardPosition enPassantPosition, ChessPiece pieceCausingEnPassant)
    {
        counter = 0;
        this.enPassantPosition = enPassantPosition;
        this.pieceCausingEnPassant = pieceCausingEnPassant;
    }

    public void Reset()
    {
        pieceCausingEnPassant = null;
        enPassantPosition = null;
        counter = 0;
    }
}
