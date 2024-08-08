using System.Collections.Generic;
using UnityEngine;
using YG;
using YGameTemplate;

namespace YGameTempate.Services.SaveLoad
{
    public class DataPersistenceService : IDataPersistentService
    {
        private GameData _gameData = null;
        
        private List<IDataSaver> _dataSavers = new List<IDataSaver>();

        public GameData GameData { get => _gameData; }

        public void NewGame()
        {
            _dataSavers.Clear();
            _gameData = new GameData();
        }

        public void LoadGame()
        {
            _gameData = LoadFromYandex();

            if (_gameData == null)
            {
                Debug.Log("GameData is not found. New GameData was created.");
                NewGame();
            }
        }


        public void SaveGame()
        {
            foreach (IDataSaver saver in _dataSavers)
            {
                saver.SaveData(ref _gameData);
            }

            SaveToYandex();
        }

        public void RegisterDataSaver(IDataSaver dataSaver) 
            => _dataSavers.Add(dataSaver);

        public void UnregisterDataSaver(IDataSaver dataSaver) 
            => _dataSavers?.Remove(dataSaver);

        public void ClearDataSavers()
            => _dataSavers = null;

        private void SaveToYandex()
        {
            string json = _gameData.ToJson();
            YandexGame.savesData.JsonFile = json;
            YandexGame.SaveProgress();
        }
        private GameData LoadFromYandex()
        {
            YandexGame.LoadProgress();
            return YandexGame.savesData.JsonFile.ToDeserialized<GameData>();
        }
    }
}