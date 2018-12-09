using UnityEngine;

public class PawnPromotionPanel : MonoBehaviour
{
    [SerializeField]
    private PawnPromotionManager pawnPromotionManager;

    public void ShowPawnPromotionPanel()
    {
        gameObject.SetActive(true);
    }

    public void HidePawnPromotionPanel()
    {
        gameObject.SetActive(false);
    }

    public void PickQueen()
    {
        pawnPromotionManager.PromotePawn(PieceType.Queen);
    }

    public void PickKnight()
    {
        pawnPromotionManager.PromotePawn(PieceType.Knight);
    }

    public void PickBishop()
    {
        pawnPromotionManager.PromotePawn(PieceType.Bishop);
    }

    public void PickRook()
    {
        pawnPromotionManager.PromotePawn(PieceType.Rook);
    }
}
