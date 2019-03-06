using System.IO;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    public static class SaveLoadManager
    {
        public static void SaveClub(Club club)
        {
            string path = Application.persistentDataPath + "/club.sav";
            using (FileStream stream = File.Open(path, FileMode.OpenOrCreate, FileAccess.Write))
            {
                stream.Position = 0;

                ClubData clubData = new ClubData(club);

                string dataAsJson = JsonUtility.ToJson(clubData);

                using (StreamWriter streamWriter = new StreamWriter(stream))
                {
                    streamWriter.WriteLine(dataAsJson);

                    stream.SetLength(stream.Position);
                    streamWriter.Close();
                    stream.Close();
                }
            }
        }

        public static void LoadClub()
        {
            string path = Application.persistentDataPath + "/club.sav";
            if (File.Exists(path))
            {
                using (FileStream stream = File.Open(path, FileMode.Open, FileAccess.Read))
                {
                    stream.Position = 0;
                    string dataAsJson;

                    using (StreamReader streamReader = new StreamReader(stream))
                    {
                        dataAsJson = streamReader.ReadToEnd();

                        ClubData clubData = JsonUtility.FromJson<ClubData>(dataAsJson);

                        Object.FindObjectOfType<Club>().SetClubData(clubData);

                        streamReader.Close();
                        stream.Close();
                    }
                }
            }
            else
            {
                return;
            }
        }

        public static void SaveSquadPlayer(SquadPlayer player)
        {
            string path = Application.persistentDataPath + "/" + player.Position + "Player.sav";
            using (FileStream stream = File.Open(path, FileMode.OpenOrCreate, FileAccess.Write))
            {
                stream.Position = 0;

                SquadPlayerData playerData = new SquadPlayerData(player);

                string dataAsJson = JsonUtility.ToJson(playerData);

                using (StreamWriter streamWriter = new StreamWriter(stream))
                {
                    streamWriter.WriteLine(dataAsJson);

                    stream.SetLength(stream.Position);
                    streamWriter.Close();
                    stream.Close();
                }
            }
        }

        public static void LoadSquadPlayer(string position)
        {
            string path = Application.persistentDataPath + "/" + position + "Player.sav";
            if (File.Exists(path))
            {
                using (FileStream stream = File.Open(path, FileMode.Open, FileAccess.Read))
                {
                    stream.Position = 0;
                    string dataAsJson;

                    using (StreamReader streamReader = new StreamReader(stream))
                    {
                        dataAsJson = streamReader.ReadToEnd();

                        SquadPlayerData playerData = JsonUtility.FromJson<SquadPlayerData>(dataAsJson);

                        var player = Object.FindObjectsOfType<SquadPlayer>().Where(x => x.Position == playerData.position).FirstOrDefault();
                        player.SetSquadPlayerData(playerData);

                        streamReader.Close();
                        stream.Close();
                    }
                }
            }
            else
            {
                return;
            }
        }

        public static void SaveAutomatedManager(AutomatedManagerObject manager)
        {
            string path = Application.persistentDataPath + "/" + manager.Position + "Manager.sav";
            using (FileStream stream = File.Open(path, FileMode.OpenOrCreate, FileAccess.Write))
            {
                stream.Position = 0;

                AutomatedManagerObjectData managerData = new AutomatedManagerObjectData(manager);

                string dataAsJson = JsonUtility.ToJson(managerData);

                using (StreamWriter streamWriter = new StreamWriter(stream))
                {
                    streamWriter.WriteLine(dataAsJson);

                    stream.SetLength(stream.Position);
                    streamWriter.Close();
                    stream.Close();
                }
            }
        }

        public static void LoadAutomatedManager(string position)
        {
            string path = Application.persistentDataPath + "/" + position + "Manager.sav";
            if (File.Exists(path))
            {
                using (FileStream stream = File.Open(path, FileMode.Open, FileAccess.Read))
                {
                    stream.Position = 0;
                    string dataAsJson;

                    using (StreamReader streamReader = new StreamReader(stream))
                    {
                        dataAsJson = streamReader.ReadToEnd();

                        AutomatedManagerObjectData managerData = JsonUtility.FromJson<AutomatedManagerObjectData>(dataAsJson);

                        var manager = Object.FindObjectsOfType<AutomatedManagerObject>().Where(x => x.Position == managerData.mPosition).FirstOrDefault();
                        manager.SetAutomatedManagerObjectData(managerData);

                        streamReader.Close();
                        stream.Close();
                    }
                }
            }
            else
            {
                return;
            }
        }

        public static void SaveUpgrade(ClubRoomUpgrade upgrade)
        {
            string path = Application.persistentDataPath + "/" + upgrade.Position + "Upgrade.sav";
            using (FileStream stream = File.Open(path, FileMode.OpenOrCreate, FileAccess.Write))
            {
                stream.Position = 0;

                ClubRoomUpgradeData upgradeData = new ClubRoomUpgradeData(upgrade);

                string dataAsJson = JsonUtility.ToJson(upgradeData);

                using (StreamWriter streamWriter = new StreamWriter(stream))
                {
                    streamWriter.WriteLine(dataAsJson);

                    stream.SetLength(stream.Position);
                    streamWriter.Close();
                    stream.Close();
                }
            }
        }

        public static void LoadUpgrade(string position)
        {
            string path = Application.persistentDataPath + "/" + position + "Upgrade.sav";
            if (File.Exists(path))
            {
                using (FileStream stream = File.Open(path, FileMode.Open, FileAccess.Read))
                {
                    stream.Position = 0;
                    string dataAsJson;

                    using (StreamReader streamReader = new StreamReader(stream))
                    {
                        dataAsJson = streamReader.ReadToEnd();


                        ClubRoomUpgradeData upgradeData = JsonUtility.FromJson<ClubRoomUpgradeData>(dataAsJson);

                        var upgrade = Object.FindObjectsOfType<ClubRoomUpgrade>().Where(x => x.Position == upgradeData.mPosition).FirstOrDefault();
                        upgrade.SetClubRoomUpgradeData(upgradeData);

                        streamReader.Close();
                        stream.Close();
                    }
                }
            }
            else
            {
                return;
            }
        }
    }
}
