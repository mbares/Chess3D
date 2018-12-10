using UnityEngine;

public class PawnPromotionPanel : MonoBehaviour
{
    [SerializeField]
    private PawnPromotionManager pawnPromotionManager;
    [SerializeField]
    private GameEvent guiShown;
    [SerializeField]
    private GameEvent guiHidden;

    public void ShowPawnPromotionPanel()
    {
        gameObject.SetActive(true);
        guiShown.Raise();
    }

    public void HidePawnPromotionPanel()
    {
        gameObject.SetActive(false);
        guiHidden.Raise();
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
