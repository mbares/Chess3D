using UnityEngine;

public class ChessboardSetUp : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private ChessboardPiecesLayout startingLayout;
    [SerializeField]
    private ChessPiecesSet inactivePiecesSet;
    [SerializeField]
    private ChessPiecesSet activePiecesSet;
    [SerializeField]
    private Transform piecesContainer;

    private void Start()
    {
        SetUpStartingLayout();
        gameManager.StartNewGame();
    }

    private void SetUpStartingLayout()
    {
        activePiecesSet.Clear();
        for (int i = 0; i < startingLayout.columns.Length; i++) {
            for (int j = 0; j < startingLayout.columns[i].row.Length; j++) {
                GameObject chessPiece = FindInactiveChessPieceWithLabel(startingLayout.columns[i].row[j]);
                if (chessPiece != null) {
                    activePiecesSet.Add(chessPiece);
                    chessPiece.transform.parent = piecesContainer;
                    chessPiece.transform.localPosition = ChessboardPositionConverter.ChessboardPositionToVector3(new ChessboardPosition(i, j));
                    chessPiece.SetActive(true);
                }
            }
        }
    }

    private GameObject FindInactiveChessPieceWithLabel(PieceLayoutLabel label)
    {
        for (int i = inactivePiecesSet.Items.Count - 1; i >= 0; i--) {
            if (inactivePiecesSet.Items[i].GetComponent<ChessPiece>().chessPieceInfo.label == label) {
                GameObject chessPiece = inactivePiecesSet.Items[i];
                inactivePiecesSet.Remove(inactivePiecesSet.Items[i]);
                
                return chessPiece;
            }
        }
        return null;
    }
}
