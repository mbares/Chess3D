using UnityEngine;
using System.Collections.Generic;

public abstract class ChessPieceMovement : MonoBehaviour
{
    private const int MIN_POSITION = 0;
    private const int MAX_POSITION = 7;

    [SerializeField]
    protected ChessPiece chessPiece;
    [SerializeField]
    protected PositionHighlightManager positionHighlightManager;
    [SerializeField]
    protected GameManager gameManager;
    [SerializeField]
    private float dragFloatingHeight = 1;

    protected List<ChessboardPosition> availablePositions = new List<ChessboardPosition>();
    protected ChessboardPosition currentPosition;
    protected bool hasMovedOnce = false;

    private ChessboardPosition positionUnderDraggedPiece;
    private ChessboardPosition lastPositionUnderDraggedPiece;
    private Vector3 normalisedVector3PositionUnderDraggedPiece;
    private Vector3 screenPoint;
    private Vector3 offset;
    private float distanceToCamera;

    protected abstract void GetAvailablePositions();

    private void Start()
    {
        currentPosition = ChessboardPositionConverter.Vector3ToChessboardPosition(transform.localPosition);
    }

    private void OnMouseDown()
    {
        if (gameManager.currentPlayer.piecesColor == GetComponent<ChessPiece>().chessPieceInfo.color) {
            GetAvailablePositions();
            distanceToCamera = Vector3.Distance(transform.position, Camera.main.transform.position);
            positionHighlightManager.ActivateCurrentPositionHighlight(transform.localPosition);
        }
    }

    private void OnMouseDrag()
    {
        if (gameManager.currentPlayer.piecesColor == GetComponent<ChessPiece>().chessPieceInfo.color) {
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
        } else if (ChessboardPositionValidator.IsPositionInBounds(positionUnderDraggedPiece) && !positionUnderDraggedPiece.Equals(currentPosition)) {
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
        if (gameManager.currentPlayer.piecesColor == GetComponent<ChessPiece>().chessPieceInfo.color) {
            if (availablePositions.Contains(positionUnderDraggedPiece)) {
                availablePositions.Clear();
                currentPosition = positionUnderDraggedPiece;
                transform.localPosition = normalisedVector3PositionUnderDraggedPiece;
                hasMovedOnce = true;
                //TODO update chessboard state
                gameManager.NextTurn();
            } else {
                transform.localPosition = ChessboardPositionConverter.ChessboardPositionToVector3(currentPosition);
            }

            positionHighlightManager.DeactivateAllHighlights();
        }
    }

    public bool IsPieceAtPositionOpponent(ChessboardPosition chessboardPosition)
    {
        return chessPiece.chessPieceInfo.color != gameManager.chessboardState.GetChessboardSquareInfo(chessboardPosition).color;
    }

    protected void GetAvailablePositionsInDirection(ChessboardPosition direction)
    {
        ChessboardPosition newPosition = currentPosition;

        newPosition += direction;
        while (ChessboardPositionValidator.IsPositionInBounds(newPosition)) {
            if (ChessboardPositionValidator.IsPositionEmpty(newPosition, gameManager.chessboardState)) {
                availablePositions.Add(newPosition);
            } else if (!ChessboardPositionValidator.IsPositionEmpty(newPosition, gameManager.chessboardState) && IsPieceAtPositionOpponent(newPosition)) {
                availablePositions.Add(newPosition);
                break;
            } else if (!ChessboardPositionValidator.IsPositionEmpty(newPosition, gameManager.chessboardState)) {
                break;
            }

            newPosition += direction;
        }
    }

    protected void AddAvailablePositionIfValid(ChessboardPosition position)
    {
        if (ChessboardPositionValidator.IsPositionInBounds(position)) {
            if (ChessboardPositionValidator.IsPositionEmpty(position, gameManager.chessboardState)) {
                availablePositions.Add(position);
            } else if (!ChessboardPositionValidator.IsPositionEmpty(position, gameManager.chessboardState) && IsPieceAtPositionOpponent(position)) {
                availablePositions.Add(position);
            }
        }
    }
}