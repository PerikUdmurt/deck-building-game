using CardBuildingGame.StaticDatas;

namespace CardBuildingGame.Services
{
    public interface IStaticDataService
    {
        StaticData GetStaticData(string staticDataName, out StaticData itemStaticData);
        void LoadStaticDatas();
    }
}