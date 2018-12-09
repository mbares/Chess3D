using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "System/CheckDetector")]
public class CheckDetector : ScriptableObject
{
    private ChessPieceMovement whiteKing;
    private ChessPieceMovement blackKing;

    public void SetKings(List<GameObject> activePieces)
    {
        for (int i = 0; i < activePieces.Count; ++i) {
            if (activePieces[i].GetComponent<ChessPiece>().chessPieceInfo.label == PieceLabel.WK) {
                whiteKing = activePieces[i].GetComponent<ChessPieceMovement>();
            } else if (activePieces[i].GetComponent<ChessPiece>().chessPieceInfo.label == PieceLabel.BK) {
                blackKing = activePieces[i].GetComponent<ChessPieceMovement>();
            }

            if (whiteKing != null && blackKing != null) {
                return;
            }
        }
    }

    public bool IsMoveCausingCheck(ChessPiece movedPiece)
    {
        if (movedPiece.chessPieceInfo.color == PieceColor.White) {
            if (movedPiece.GetComponent<ChessPieceMovement>().GetAvailableCapturePositions().Contains(blackKing.simulatedPosition)) {
                return true;
            }
        } else if (movedPiece.chessPieceInfo.color == PieceColor.Black) {
            if (movedPiece.GetComponent<ChessPieceMovement>().GetAvailableCapturePositions().Contains(whiteKing.simulatedPosition)) {
                return true;
            }
        }

        return false;
    }

    public bool IsPlayerInCheck(Player player)
    {
        if (player.piecesColor == PieceColor.White) {
            if (player.opponentAvailablePositionsSet.Items.Contains(whiteKing.simulatedPosition)) {
                return true;
            }
        } else if (player.piecesColor == PieceColor.Black) {
            if (player.opponentAvailablePositionsSet.Items.Contains(blackKing.simulatedPosition)) {
                return true;
            }
        }

        return false;
    }

    public ChessPiece GetPieceThatIsCausingCheck(List<GameObject> activePieces, Player checkedPlayer)
    {
        ChessboardPosition kingPosition;
        if (checkedPlayer.piecesColor == PieceColor.White) {
            kingPosition = whiteKing.simulatedPosition;
        } else {
            kingPosition = blackKing.simulatedPosition;
        }

        for (int i = 0; i < activePieces.Count; ++i) {
            ChessPiece piece = activePieces[i].GetComponent<ChessPiece>();
            if (piece.controllingPlayer != checkedPlayer) {
                List<ChessboardPosition> availablePositionsForPiece = piece.GetComponent<ChessPieceMovement>().GetAvailableCapturePositions();
                for (int j = 0; j < availablePositionsForPiece.Count; ++j) {
                    if (availablePositionsForPiece[j].Equals(kingPosition)) {
                        return piece;
                    }
                }
            }
        }
        return null;
    }
}
