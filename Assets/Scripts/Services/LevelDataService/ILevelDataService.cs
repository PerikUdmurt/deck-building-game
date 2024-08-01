using YGameTemplate.Services.LevelData;

namespace YGameTemplate
{
    public interface ILevelDataService
    {
        LevelData CurrentLevelData { get; }
    }
}