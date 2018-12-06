using UnityEngine;

[CreateAssetMenu]
public class ChessMoveRecorder : ScriptableObject
{
    private PlayerMoves playerMoves;

    public void ClearMoves()
    {
        playerMoves.Clear();
    }

    public void RecordMove(ChessboardPosition from, ChessboardPosition to)
    {
        AddPlayerMove(from, to);
    }

    private void AddPlayerMove(ChessboardPosition from, ChessboardPosition to)
    {
        playerMoves.Add(from, to);
    }

    public void SavePlayerMovesForReplay()
    {
        PlayerMovesSerializer.SavePlayerMoves(playerMoves, System.DateTime.Now.ToString("dd-MM-yy_H-mm-ss"));
    }

    public void SavePlayerMovesForContinue()
    {
        PlayerMovesSerializer.SavePlayerMoves(playerMoves, "unfinished_game");
    }
}
