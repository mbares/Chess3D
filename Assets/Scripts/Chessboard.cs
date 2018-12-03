using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Chessboard : ScriptableObject
{
    [System.Serializable]
    public struct PiecePosition
    {
        public IChessPiece chessPiece;
        public Vector2 position;
    }

    [HideInInspector]
    public List<PiecePosition> piecePositions = new List<PiecePosition>();
}
