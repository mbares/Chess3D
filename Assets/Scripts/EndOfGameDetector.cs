using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "System/EndOfGameDetector")]
public class EndOfGameDetector : ScriptableObject
{
    [SerializeField]
    private ChessboardStateManager chessboardStateManager;
    [SerializeField]
    private ChessPiecesSet blackPlayerPieces;
    [SerializeField]
    private ChessPiecesSet whitePlayerPieces;
    [SerializeField]
    private GameEvent whiteWinsEvent;
    [SerializeField]
    private GameEvent blackWinsEvent;
    [SerializeField]
    private GameEvent drawEvent;

    public bool CheckForEndOfGame(Player currentPlayer)
    {
        List<ChessboardPosition> currentPlayerAvailablePositions = chessboardStateManager.GetAllAvailablePositionsOfPlayer(currentPlayer);

        if (IsCheckmate(currentPlayer, currentPlayerAvailablePositions)) {
            return true;
        } else if (IsDraw(currentPlayer, currentPlayerAvailablePositions)) {
            return true;
        }
        return false;
    }

    private bool IsCheckmate(Player currentPlayer, List<ChessboardPosition> currentPlayerAvailablePositions)
    {
        if (currentPlayer.IsInCheck() && currentPlayerAvailablePositions.Count == 0) {
            if (currentPlayer.piecesColor == PieceColor.White) {
                blackWinsEvent.Raise();
            } else {
                whiteWinsEvent.Raise();
            }
            return true;
        }
        return false;
    }

    private bool IsDraw(Player currentPlayer, List<ChessboardPosition> currentPlayerAvailablePositions)
    {
        if (currentPlayerAvailablePositions.Count == 0) {
            drawEvent.Raise();
            return true;
        } if (PlayersHaveInsufficientMaterial()) {
            drawEvent.Raise();
            return true;
        }
        return false;
    }

    private bool PlayersHaveInsufficientMaterial()
    {
        if (whitePlayerPieces.Items.Count == 1 && blackPlayerPieces.Items.Count == 1) {
            return true;
        } else if (whitePlayerPieces.Items.Count == 2 && PlayerPiecesSetContains(PieceType.Bishop, whitePlayerPieces) && blackPlayerPieces.Items.Count == 1) {
            return true;
        } else if (whitePlayerPieces.Items.Count == 2 && PlayerPiecesSetContains(PieceType.Knight, whitePlayerPieces) && blackPlayerPieces.Items.Count == 1) {
            return true;
        } else if (blackPlayerPieces.Items.Count == 2 && PlayerPiecesSetContains(PieceType.Bishop, blackPlayerPieces) && whitePlayerPieces.Items.Count == 1) {
            return true;
        } else if (blackPlayerPieces.Items.Count == 2 && PlayerPiecesSetContains(PieceType.Knight, blackPlayerPieces) && whitePlayerPieces.Items.Count == 1) {
            return true;
        } else if (whitePlayerPieces.Items.Count == 2 && blackPlayerPieces.Items.Count == 2 && SameColoredBishopsInsufficientMaterial()) {
            return true;
        }
        return false;
    }

    private bool PlayerPiecesSetContains(PieceType pieceType, ChessPiecesSet set)
    {
        for (int i = 0; i < set.Items.Count; ++i) {
            if (set.Items[i].GetComponent<ChessPiece>().chessPieceInfo.type == pieceType) {
                return true;
            }
        }
        return false;
    }

    private bool SameColoredBishopsInsufficientMaterial()
    {
        BishopMovement whiteBishop = null;
        BishopMovement blackBishop = null;
        for (int i = 0; i < whitePlayerPieces.Items.Count; ++i) {
            if (whitePlayerPieces.Items[i].GetComponent<ChessPiece>().chessPieceInfo.type == PieceType.Bishop) {
                whiteBishop = whitePlayerPieces.Items[i].GetComponent<BishopMovement>();
            }
        }
        if (whiteBishop == null) {
            return false;
        }

        for (int i = 0; i < blackPlayerPieces.Items.Count; ++i) {
            if (blackPlayerPieces.Items[i].GetComponent<ChessPiece>().chessPieceInfo.type == PieceType.Bishop) {
                blackBishop = blackPlayerPieces.Items[i].GetComponent<BishopMovement>();
            }
        }
        if (blackBishop == null) {
            return false;
        }

        return whiteBishop.availableSquaresColor == blackBishop.availableSquaresColor;
    }
}
