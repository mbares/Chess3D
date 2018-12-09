using System.Collections.Generic;
using System;

[Serializable]
public class PlayerMoves
{
    public List<ChessMove> moves = new List<ChessMove>();
    public List<string> notatedMoves = new List<string>();

    public void Clear()
    {
        moves.Clear();
        notatedMoves.Clear();
    }

    public void Add(ChessMove move, string notatedMove)
    {
        moves.Add(move);
        notatedMoves.Add(notatedMove);
    }
}

[Serializable]
public class ChessMove
{
    public ChessboardPosition from;
    public ChessboardPosition to;

    public ChessMove(ChessboardPosition from, ChessboardPosition to)
    {
        this.from = from;
        this.to = to;
    }

    public string ToAlgebraicNotationString(MoveType moveType, ChessPiece movedPiece, ChessPiece capturedPiece = null)
    {
        string algebraicNotation = "";

        switch (moveType) {            
            case MoveType.Check:
                algebraicNotation = "+";
                break;
            case MoveType.KingsideCastling:
                return "0-0";
            case MoveType.QueenSideCastling:
                return "0-0-0";            
            default:
                break;
        }

        if (movedPiece.chessPieceInfo.type != PieceType.Pawn) {
            algebraicNotation = movedPiece.chessPieceInfo.label.ToString().Substring(1);
        }

        if (capturedPiece != null) {
            algebraicNotation += "x";
        }

        algebraicNotation += to.ToString();

        return algebraicNotation;
    }

    public override string ToString()
    {
        return from.ToString() + ":" + to.ToString();
    }

    public static ChessMove FromString(string data)
    {
        string[] parts = data.Split(':');
        return new ChessMove(ChessboardPositionConverter.StringToChessboardPosition(parts[0]), ChessboardPositionConverter.StringToChessboardPosition(parts[1]));
    }
}

public enum MoveType
{
    Normal, Check, KingsideCastling, QueenSideCastling
}