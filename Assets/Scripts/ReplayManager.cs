using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayManager : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private ChessboardStateManager chessboardStateManager;
    [SerializeField]
    private float delayBetweenMoves;
    [SerializeField]
    private GameEvent newGameRequest;
    [SerializeField]
    private ReplayData replayData;

    public void PlayReplay()
    {
        gameManager.SetPlayerInteractionAllowed(false);
        newGameRequest.Raise();

        StartCoroutine(PlayMovesWithDelay(replayData));
    }

    private IEnumerator PlayMovesWithDelay(ReplayData replayData)
    {
        List<ChessMove> chessMoves = replayData.playerMoves.moves;

        for (int i = 0; i < chessMoves.Count; ++i) {
            PieceMover mover = chessboardStateManager.GetChessPieceAtPosition(chessMoves[i].from).GetComponent<PieceMover>();
            mover.MovePiece(chessMoves[i].to);
            yield return new WaitForSeconds(delayBetweenMoves);
        }
        yield return new WaitForSeconds(delayBetweenMoves * 5);

        newGameRequest.Raise();
        gameManager.SetPlayerInteractionAllowed(true);
    }
}
