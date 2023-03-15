using System.Collections.Generic;
using Components.BusinessParams;
using Gameframe.SaveLoad;
using UnityEngine;

namespace Utilities
{
    public static class SaveUtility
    {
        private static GameModel _model;
        private static GameModel Model
        {
            get
            {
                if (_model != null) return _model;
                Debug.Log($"Save location {Application.persistentDataPath}");
                var saveFilesCount = SaveManagerInstance.GetFiles(null).Length;
                if (saveFilesCount > 0)
                    _model = SaveManagerInstance.Load<GameModel>("gameProgress.txt");
                else
                    _model = new GameModel();

                return _model;
            }
        }
        private static SaveLoadManager _saveManagerInstance;
        private static SaveLoadManager SaveManagerInstance
        {
            get
            {
                if (_saveManagerInstance == null)
                    _saveManagerInstance = SaveLoadManager.Create(
                        null, null,
                        SerializationMethodType.UnityJson);

                return _saveManagerInstance;
            }
        }

        public static void SaveBusinessesProgress(List<BusinessData> businesses)
        {
            Model.Businesses = businesses;
            SaveManagerInstance.Save(Model, "gameProgress.txt");
        }

        public static void SaveBalance(int balance)
        {
            Model.Balance = balance;
            SaveManagerInstance.Save(Model, "gameProgress.txt");
        }

        public static void SaveLocalDataState()
        {
            Model.HaveLocalData = true;
            SaveManagerInstance.Save(Model, "gameProgress.txt");
        }

        public static List<BusinessData> LoadBusinessData() => Model.Businesses;
        public static int LoadBalance() => Model.Balance;
        public static bool HaveLocalData() => Model.HaveLocalData;
    }

    public class GameModel
    {
        public int Balance;
        public List<BusinessData> Businesses;
        public bool HaveLocalData;
    }
}