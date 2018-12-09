using UnityEngine;

[CreateAssetMenu(menuName = "System/PawnPromotionManager")]
public class PawnPromotionManager : ScriptableObject
{
    [SerializeField]
    private ChessMoveRecorder chessMoveRecorder;
    [SerializeField]
    private ChessboardStateManager chessboardStateManager;
    [SerializeField]
    private ChessPiecesSet activePiecesSet;
    [SerializeField]
    private ChessPiecesSet inactivePiecesSet;

    public void PromotePawn(PieceType type)
    {
        ChessPiece pawnToPromote = null;
        for (int i = 0; i < activePiecesSet.Items.Count; ++i) {
            if (activePiecesSet.Items[i].GetComponent<PawnMovement>() != null && activePiecesSet.Items[i].GetComponent<PawnMovement>().awaitingPromotion) {
                pawnToPromote = activePiecesSet.Items[i].GetComponent<ChessPiece>();
                break;
            }
        }

        if (pawnToPromote != null) {
            ChessPiece newPiece = FindInactiveChessPieceOfColorAndType(pawnToPromote.chessPieceInfo.color, type);
            if (newPiece != null) {
                chessboardStateManager.RemovePieceDataFromPosition(pawnToPromote.GetChessboardPosition());
                newPiece.transform.localPosition = pawnToPromote.transform.localPosition;
                newPiece.Activate();
                chessboardStateManager.AddPieceDataToPosition(newPiece, pawnToPromote.GetChessboardPosition());
                activePiecesSet.Add(newPiece.gameObject);
                activePiecesSet.Remove(pawnToPromote.gameObject);

                chessMoveRecorder.RecordPawnPromotion(newPiece.chessPieceInfo.label);
                pawnToPromote.GetComponent<PawnMovement>().awaitingPromotion = false;

                pawnToPromote.Deactivate();
            }
        }
    }

    private ChessPiece FindInactiveChessPieceOfColorAndType(PieceColor color, PieceType type)
    {
        for (int i = inactivePiecesSet.Items.Count - 1; i >= 0; --i) {
            ChessPiece piece = inactivePiecesSet.Items[i].GetComponent<ChessPiece>();
            if (piece.chessPieceInfo.color == color && piece.chessPieceInfo.type == type) {
                return piece;
            }
        }
        return null;
    }
}
