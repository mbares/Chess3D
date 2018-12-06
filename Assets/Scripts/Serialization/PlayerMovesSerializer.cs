using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class PlayerMovesSerializer : MonoBehaviour
{
    public static void SavePlayerMoves(PlayerMoves moves, string name)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/" + name + ".dat");

        binaryFormatter.Serialize(file, moves);
        file.Close();
    }

    public static PlayerMoves LoadPlayerMoves(string name)
    {
        if (File.Exists(Application.persistentDataPath + "/" + name + ".dat")) {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + name + ".dat", FileMode.Open);
            PlayerMoves playerMoves = (PlayerMoves)binaryFormatter.Deserialize(file);

            file.Close();
            return playerMoves;
        }

        throw new FileNotFoundException();
    }
}
