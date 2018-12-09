using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class PlayerMovesSerializer : MonoBehaviour
{
    public static void SavePlayerMovesForContinue(PlayerMoves moves)
    {
        SavePlayerMoves(moves, "unfinished_game");
    }

    public static void SavePlayerMovesForReplay(PlayerMoves moves)
    {
        SavePlayerMoves(moves, "replay_" + System.DateTime.Now.ToString("dd-MM-yy_H-mm-ss"));
    }

    public static void SavePlayerMoves(PlayerMoves moves, string name)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/" + name + "_moves.dat");

        string data = PlayerMovesToString(moves);
        binaryFormatter.Serialize(file, data);
        file.Close();
    }

    public static PlayerMoves LoadPlayerMovesForContinue()
    {
        return LoadPlayerMoves("unfinished_game");
    }

    public static PlayerMoves LoadPlayerMoves(string name)
    {
        if (File.Exists(Application.persistentDataPath + "/" + name + "_moves.dat")) {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + name + "_moves.dat", FileMode.Open);
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
        for (int i = 0; i < playerMoves.notatedMoves.Count; ++i) {
            sb.Append(playerMoves.notatedMoves[i]);
            if (i != playerMoves.notatedMoves.Count - 1) {
                sb.Append(",");
            }
        }

        return sb.ToString();
    }

    private static PlayerMoves StringToPlayerMoves(string data)
    {
        PlayerMoves playerMoves = new PlayerMoves();
        string[] parts = data.Split('|');
        string[] chessMoves = parts[0].Split(',');
        string[] notatedMoves = parts[1].Split(',');

        for (int i = 0; i < chessMoves.Length; ++i) {
            playerMoves.moves.Add(ChessMove.FromString(chessMoves[i]));
        }

        for (int i = 0; i < notatedMoves.Length; ++i) {
            playerMoves.notatedMoves.Add(notatedMoves[i]);
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
            fullName = fullName.Remove(fullName.IndexOf("_moves.dat"));
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
