using System;
using System.IO;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GameStateSerializer : MonoBehaviour
{
    public static void SaveGameState(GameState gameState)
    {
        File.Delete(Application.persistentDataPath + "/unfinished_game_state.dat");

        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/unfinished_game_state.dat");

        string data = GameStateToString(gameState);

        binaryFormatter.Serialize(file, data);
        file.Close();
    }

    public static GameState LoadGameState()
    {
        if (File.Exists(Application.persistentDataPath + "/unfinished_game_state.dat")) {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/unfinished_game_state.dat", FileMode.Open);
            string data = (string)binaryFormatter.Deserialize(file);

            file.Close();

            return StringToGameState(data);
        }
        return null;
    }

    private static string GameStateToString(GameState gameState)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(gameState.currentPlayerColor.ToString() + "|");
        for (int i = 0; i < gameState.chessboardSquaresInfo.Length; ++i) {
            for (int j = 0; j < gameState.chessboardSquaresInfo[i].row.Length; ++j) {
                sb.Append(gameState.chessboardSquaresInfo[i].row[j] + ",");
            }
        }

        return sb.ToString();
    }

    private static GameState StringToGameState(string data)
    {
        GameState gameState = new GameState();
        string[] parts = data.Split('|');
        gameState.currentPlayerColor = (PieceColor)Enum.Parse(typeof(PieceColor), parts[0]);
        string[] squareInfo = parts[1].Split(',');
        int counter = 0;
        for (int i = 0; i < gameState.chessboardSquaresInfo.Length; ++i) {
            for (int j = 0; j < gameState.chessboardSquaresInfo[i].row.Length; ++j) {
                gameState.chessboardSquaresInfo[i].row[j] = (PieceLabel)Enum.Parse(typeof(PieceLabel), squareInfo[counter]);
                counter++;
            }
        }
        return gameState;
    }
}

public class GameState
{
    public PieceColor currentPlayerColor;
    public ChessboardSquaresInfo[] chessboardSquaresInfo = new ChessboardSquaresInfo[8];

    public GameState()
    {
        for (int i = 0; i < 8; i++) {
            for (int j = 0; j < 8; j++) {
                chessboardSquaresInfo[i] = new ChessboardSquaresInfo();
            }
        }
    }
}