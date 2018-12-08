﻿using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CheckDetector : ScriptableObject
{
    private ChessPieceMovement whiteKing;
    private ChessPieceMovement blackKing;

    public void SetKings(List<GameObject> activePieces)
    {
        for (int i = 0; i < activePieces.Count; ++i) {
            if (activePieces[i].GetComponent<ChessPiece>().chessPieceInfo.label == PieceLayoutLabel.WKng) {
                whiteKing = activePieces[i].GetComponent<ChessPieceMovement>();
            } else if (activePieces[i].GetComponent<ChessPiece>().chessPieceInfo.label == PieceLayoutLabel.BKng) {
                blackKing = activePieces[i].GetComponent<ChessPieceMovement>();
            }

            if (whiteKing != null && blackKing != null) {
                return;
            }
        }
    }

    public bool DetectIfPlayerInCheck(Player player)
    {
        if (player.piecesColor == PieceColor.White) {
            if (player.opponentAvailablePositionsSet.Items.Contains(whiteKing.simulatedPosition)) {
                Debug.Log("White in check");
                return true;
            }
        } else if (player.piecesColor == PieceColor.Black) {
            if (player.opponentAvailablePositionsSet.Items.Contains(blackKing.simulatedPosition)) {
                Debug.Log("Black in check");
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