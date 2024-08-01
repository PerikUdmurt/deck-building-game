namespace YGameTempate.Services.SaveLoad
{
    public interface IDataPersistentService
    {
        GameData GameData { get; }

        void ClearDataSavers();
        void LoadGame();
        void NewGame();
        void RegisterDataSaver(IDataSaver dataSaver);
        void SaveGame();
        void UnregisterDataSaver(IDataSaver dataSaver);
    }
}
