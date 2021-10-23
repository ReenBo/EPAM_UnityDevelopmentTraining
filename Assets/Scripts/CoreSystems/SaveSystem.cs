using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using ET;
using ET.Player;
using ET.Core.Stats;
using ET.Weapons;

namespace ET.Core.SaveSystem
{
    public static class SaveSystem
    {
        public static void SaveGame(PlayerController player, LevelSystem.LevelSystem progress)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            string path = Application.persistentDataPath + "/GameStats.saves";

            FileStream stream = new FileStream(path, FileMode.Create);

            CharacterStats stats = new CharacterStats(player, progress);

            binaryFormatter.Serialize(stream, stats);
            stream.Close();
        }

        public static CharacterStats LoadGame()
        {
            string path = Application.persistentDataPath + "/GameStats.saves";

            if (File.Exists(path))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                CharacterStats stats = binaryFormatter.Deserialize(stream) as CharacterStats;
                stream.Close();

                return stats;
            }
            else
            {
                Debug.LogError($"Error loading from the game file {path}");
                return null;
            }
        }
    }
}
