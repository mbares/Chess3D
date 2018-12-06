using System.Collections.Generic;
using System;

[Serializable]
public class PlayerMoves
{
    public List<ChessMove> moves = new List<ChessMove>();

    public void Clear()
    {
        moves.Clear();
    }

    public void Add(ChessboardPosition from, ChessboardPosition to)
    {
        moves.Add(new ChessMove(from, to));
    }

    public void SaveMoves(string name)
    {
        PlayerMovesSerializer.SavePlayerMoves(this, name);
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
}
