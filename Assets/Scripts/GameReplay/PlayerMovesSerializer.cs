using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class PlayerMovesSerializer : MonoBehaviour
{
    public static void SavePlayerMovesForReplay(PlayerMoves moves)
    {
        SavePlayerMoves(moves, "replay_" + System.DateTime.Now.ToString("dd-MM-yy_H-mm-ss"));
    }

    public static void SavePlayerMoves(PlayerMoves moves, string name)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/" + name + ".dat");

        string data = PlayerMovesToString(moves);
        binaryFormatter.Serialize(file, data);
        file.Close();
    }

    public static PlayerMoves LoadPlayerMoves(string name)
    {
        if (File.Exists(Application.persistentDataPath + "/" + name + ".dat")) {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + name + ".dat", FileMode.Open);
            string data = (string)binaryFormatter.Deserialize(file);

            PlayerMoves playerMoves = StringToPlayerMoves(data);

            file.Close();
            return playerMoves;
        }

        throw new FileNotFoundException();
    }

    private static string PlayerMovesToString(PlayerMoves playerMoves)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < playerMoves.moves.Count; ++i) {
            sb.Append(playerMoves.moves[i].ToString());
            if (i != playerMoves.moves.Count - 1) {
                sb.Append(",");
            }
        }
        sb.Append("|");
        foreach (KeyValuePair<ChessboardPosition, PieceType> entry in playerMoves.pawnPromotions) {
            sb.Append(entry.Key + ":" + entry.Value + ",");
        }
        if (sb.ToString().LastIndexOf(',') == sb.ToString().Length - 1) {
            sb.Remove(sb.Length - 1, 1);
        }

        return sb.ToString();
    }

    private static PlayerMoves StringToPlayerMoves(string data)
    {
        PlayerMoves playerMoves = new PlayerMoves();
        string[] parts = data.Split('|');
        string[] chessMoves = parts[0].Split(',');


        for (int i = 0; i < chessMoves.Length; ++i) {
            playerMoves.moves.Add(ChessMove.FromString(chessMoves[i]));
        }

        if (!string.IsNullOrEmpty(parts[1])) {
            string[] pawnPromotions = parts[1].Split(',');
            for (int i = 0; i < pawnPromotions.Length; ++i) {
                string[] keyValueString = pawnPromotions[i].Split(':');
                playerMoves.pawnPromotions.Add(ChessboardPositionConverter.StringToChessboardPosition(keyValueString[0]), (PieceType)Enum.Parse(typeof(PieceType), keyValueString[1]));
            }
        }

        return playerMoves;
    }

    public static Dictionary<string, PlayerMoves> GetAllReplays()
    {
        string partialName = "replay";
        Dictionary<string, PlayerMoves> allReplays = new Dictionary<string, PlayerMoves>();

        DirectoryInfo replaysDirectory = new DirectoryInfo(Application.persistentDataPath);
        FileInfo[] filesInDir = replaysDirectory.GetFiles(partialName + "*");
        foreach (FileInfo foundFile in filesInDir) {
            string fullName = foundFile.Name;
            fullName = fullName.Remove(fullName.IndexOf(".dat"));
            try {
                allReplays.Add(fullName, LoadPlayerMoves(fullName));
            } catch (FileNotFoundException) {
                Debug.LogError("Replay file not found");
                continue;
            }
        }

        return allReplays;
    }
}
