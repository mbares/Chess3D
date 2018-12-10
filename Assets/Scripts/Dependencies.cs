using UnityEngine;

public class Dependencies : MonoBehaviour
{
    [SerializeField]
    private PlayerAvailablePositionsSet whiteAvailablePositions;
    [SerializeField]
    private PlayerAvailablePositionsSet blackAvailablePositions;
    [SerializeField]
    private ChessPiecesSet whitePieces;
    [SerializeField]
    private ChessPiecesSet blackPieces;
    [SerializeField]
    private EndOfGameDetector endOfGameDetector;
}
