using System.Collections.Generic;
using UnityEngine;

public class MouseDrag : MonoBehaviour
{
    [SerializeField]
    private ChessPiece chessPiece;
    [SerializeField]
    private ChessPieceMovement chessPieceMovement;
    [SerializeField]
    private PositionHighlightManager positionHighlightManager;
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private ChessboardStateManager chessboardState;
    [SerializeField]
    private ChessMoveRecorder chessMoveRecorder;
    [SerializeField]
    private CheckDetector checkDetector;
    [SerializeField]
    private EnPassantManager enPassantManager;
    [SerializeField]
    private float dragFloatingHeight = 1;

    private List<ChessboardPosition> availablePositions = new List<ChessboardPosition>();
    private ChessboardPosition positionUnderDraggedPiece;
    private ChessboardPosition lastPositionUnderDraggedPiece;
    private Vector3 normalisedVector3PositionUnderDraggedPiece;
    private Vector3 screenPoint;
    private Vector3 offset;
    private float distanceToCamera;

    private void OnMouseDown()
    {
        if (gameManager.currentPlayer == GetComponent<ChessPiece>().controllingPlayer) {
            availablePositions = chessPieceMovement.GetAvailablePositions();
            distanceToCamera = Vector3.Distance(transform.position, Camera.main.transform.position);
            positionHighlightManager.ActivateCurrentPositionHighlight(transform.localPosition);
        }
    }

    private void OnMouseDrag()
    {
        if (gameManager.currentPlayer == GetComponent<ChessPiece>().controllingPlayer) {
            DragChessPiece();

            positionUnderDraggedPiece = ChessboardPositionConverter.Vector3ToChessboardPosition(transform.localPosition);

            if (lastPositionUnderDraggedPiece != positionUnderDraggedPiece) {
                lastPositionUnderDraggedPiece = positionUnderDraggedPiece;
                normalisedVector3PositionUnderDraggedPiece = ChessboardPositionConverter.ChessboardPositionToVector3(positionUnderDraggedPiece);

                TogglePositionHighlights();
            }
        }
    }

    private void DragChessPiece()
    {
        Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distanceToCamera);
        Vector3 cursorPosition = GetWorldPositionOnPlane(cursorPoint);
        cursorPosition.y = dragFloatingHeight;
        transform.localPosition = cursorPosition;
    }

    private void TogglePositionHighlights()
    {
        positionHighlightManager.DeactivateInvalidPositionHighlight();
        positionHighlightManager.DeactivateValidPositionHighlight();

        if (availablePositions.Contains(positionUnderDraggedPiece)) {
            positionHighlightManager.ActivateValidPositionHighlight(normalisedVector3PositionUnderDraggedPiece);
        } else if (ChessboardPositionValidator.IsPositionInBounds(positionUnderDraggedPiece) && !positionUnderDraggedPiece.Equals(chessPieceMovement.currentPosition)) {
            positionHighlightManager.ActivateInvalidPositionHighlight(normalisedVector3PositionUnderDraggedPiece);
        }
    }

    private Vector3 GetWorldPositionOnPlane(Vector3 screenPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        Plane xz = new Plane(Vector3.up, new Vector3(0, dragFloatingHeight, 0));
        float distance;
        xz.Raycast(ray, out distance);
        return ray.GetPoint(distance);
    }

    private void OnMouseUp()
    {
        if (gameManager.currentPlayer == chessPiece.controllingPlayer) {
            if (availablePositions.Contains(positionUnderDraggedPiece)) {
                ChessPiece capturedPiece = null;
                bool enPassantMove = false;

                if (ExistsCapturablePieceAtAvailablePosition()) {
                    capturedPiece = chessboardState.GetChessPieceAtPosition(positionUnderDraggedPiece);
                    chessboardState.RemoveCapturedPieceAtPositionAndDeactivate(positionUnderDraggedPiece);
                } else if (chessPiece.chessPieceInfo.type == PieceType.Pawn && enPassantManager.enPassantPosition != null && enPassantManager.enPassantPosition.Equals(positionUnderDraggedPiece)) {
                    capturedPiece = enPassantManager.pieceCausingEnPassant;
                    chessboardState.RemoveCapturedPieceAtPositionAndDeactivate(capturedPiece.GetChessboardPosition());
                    enPassantMove = true;
                }

                KingMovement kingMovement = chessPiece.GetComponent<KingMovement>();

                ChessboardPosition lastPosition = chessPieceMovement.currentPosition;
                UpdatePieceData();
                availablePositions.Clear();
                chessPieceMovement.Move(positionUnderDraggedPiece);
                chessPieceMovement.hasMoved = true;

                if (checkDetector.IsMoveCausingCheck(chessPiece)) {
                    gameManager.SetOtherPlayerInCheck(chessPiece.controllingPlayer);
                    chessMoveRecorder.RecordCheck(chessPiece, lastPosition, positionUnderDraggedPiece, capturedPiece);
                } else if (enPassantMove) {
                    chessMoveRecorder.RecordEnPassant(lastPosition, positionUnderDraggedPiece);
                } else if (kingMovement != null && positionUnderDraggedPiece.Equals(kingMovement.GetKingsideCastlingKingPosition())) {
                    ChessPieceMovement kingsideRook = kingMovement.GetKingsideRook();
                    ChessboardPosition lastRookPosition = kingsideRook.currentPosition;
                    kingsideRook.Move(kingMovement.GetKingsideCastlingRookPosition());
                    kingsideRook.hasMoved = true;
                    chessMoveRecorder.RecordKingsideCastling(new ChessMove(lastPosition, positionUnderDraggedPiece), new ChessMove(lastRookPosition, kingsideRook.currentPosition));
                } else if (kingMovement != null && positionUnderDraggedPiece.Equals(kingMovement.GetQueensideCastlingKingPosition())) {
                    ChessPieceMovement queensideRook = kingMovement.GetQueensideRook();
                    ChessboardPosition lastRookPosition = queensideRook.currentPosition;
                    queensideRook.Move(kingMovement.GetQueensideCastlingRookPosition());
                    queensideRook.hasMoved = true;
                    chessMoveRecorder.RecordQueensideCastling(new ChessMove(lastPosition, positionUnderDraggedPiece), new ChessMove(lastRookPosition, queensideRook.currentPosition));
                } else {
                    chessMoveRecorder.RecordNormalMove(chessPiece, lastPosition, positionUnderDraggedPiece, capturedPiece);
                }

                gameManager.StartNextTurn();
            } else {
                transform.localPosition = ChessboardPositionConverter.ChessboardPositionToVector3(chessPieceMovement.currentPosition);
            }

            positionHighlightManager.DeactivateAllHighlights();
        }
    }

    private void UpdatePieceData()
    {
        chessboardState.RemovePieceDataFromPosition(chessPieceMovement.currentPosition);
        chessboardState.AddPieceDataToPosition(chessPiece, positionUnderDraggedPiece);
    }

    private bool ExistsCapturablePieceAtAvailablePosition()
    {
        if (chessboardState.GetChessPieceAtPosition(positionUnderDraggedPiece) != null && chessPieceMovement.IsPieceAtPositionOpponent(positionUnderDraggedPiece)) {
            return true;
        }
        return false;
    }
}