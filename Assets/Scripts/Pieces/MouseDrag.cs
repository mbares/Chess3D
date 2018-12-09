using System.Collections.Generic;
using UnityEngine;

public class MouseDrag : MonoBehaviour
{
    [SerializeField]
    protected ChessPiece chessPiece;
    [SerializeField]
    private ChessPieceMovement chessPieceMovement;
    [SerializeField]
    protected PositionHighlightManager positionHighlightManager;
    [SerializeField]
    protected GameManager gameManager;
    [SerializeField]
    protected ChessboardStateManager chessboardState;
    [SerializeField]
    protected ChessMoveRecorder chessMoveRecorder;
    [SerializeField]
    protected CheckDetector checkDetector;
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
                if (ExistsCapturablePieceAtAvailablePosition()) {
                    capturedPiece = chessboardState.GetChessPieceAtPosition(positionUnderDraggedPiece);
                    chessboardState.RemoveCapturedPieceAtPositionAndDeactivate(positionUnderDraggedPiece);
                }

                ChessboardPosition lastPosition = chessPieceMovement.currentPosition;
                UpdatePieceData();
                availablePositions.Clear();
                chessPieceMovement.Move(positionUnderDraggedPiece);
                chessPieceMovement.SetHasMovedOnce();

                if (checkDetector.IsMoveCausingCheck(chessPiece)) {
                    gameManager.SetOtherPlayerInCheck(chessPiece.controllingPlayer);
                    chessMoveRecorder.RecordCheck(chessPiece, lastPosition, positionUnderDraggedPiece, capturedPiece);
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