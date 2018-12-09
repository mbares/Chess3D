using UnityEngine;

public class Chessboard : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private EnPassantManager enPassantManager;
    [SerializeField]
    private ChessboardPiecesLayout startingLayout;
    [SerializeField]
    private ChessboardPiecesLayout unfinishedGameLayout;
    [SerializeField]
    private ChessPiecesSet inactivePiecesSet;
    [SerializeField]
    private ChessPiecesSet activePiecesSet;
    [SerializeField]
    private Transform piecesContainer;

    private void OnApplicationQuit()
    {
        gameManager.SaveUnfinishedGameState();
        gameManager.SaveUnfinishedGameMoves();
        activePiecesSet.Clear();
        inactivePiecesSet.Clear();
    }

    private void Start()
    {
        if (GameStateSerializer.LoadGameState() != null) {
            gameManager.chessboardState.SetUnfinishedGameLayout();
            SetUpChessboardLayout(unfinishedGameLayout);
            gameManager.ContinueGame();
        } else {
            StartNewGame();
        }
    }

    public void StartNewGame()
    {
        enPassantManager.Reset();
        SetUpChessboardLayout(startingLayout);
        gameManager.StartNewGame();
    }

    private void SetUpChessboardLayout(ChessboardPiecesLayout layout)
    {
        DeactivateAllActivePieces();
        activePiecesSet.Clear();
        for (int i = 0; i < layout.chessboardSquaresInfo.Length; i++) {
            for (int j = 0; j < layout.chessboardSquaresInfo[i].row.Length; j++) {
                GameObject chessPiece = FindInactiveChessPieceWithLabel(layout.chessboardSquaresInfo[i].row[j]);
                if (chessPiece != null) {
                    activePiecesSet.Add(chessPiece);
                    chessPiece.transform.parent = piecesContainer;
                    chessPiece.GetComponent<ChessPieceMovement>().Move(new ChessboardPosition(i, j));
                    chessPiece.SetActive(true);
                }
            }
        }
        gameManager.SetPlayerPieces();
    }

    private void DeactivateAllActivePieces()
    {
        for (int i = 0; i < activePiecesSet.Items.Count; ++i) {
            activePiecesSet.Items[i].GetComponent<ChessPiece>().Deactivate();
        }
    }

    private GameObject FindInactiveChessPieceWithLabel(PieceLabel label)
    {
        for (int i = inactivePiecesSet.Items.Count - 1; i >= 0; --i) {
            if (inactivePiecesSet.Items[i].GetComponent<ChessPiece>().chessPieceInfo.label == label) {
                GameObject chessPiece = inactivePiecesSet.Items[i];
                inactivePiecesSet.Remove(inactivePiecesSet.Items[i]);

                return chessPiece;
            }
        }
        return null;
    }

    public void ClearEnPassantPosition()
    {
        enPassantManager.ClearEnPassantPosition();
    }
}