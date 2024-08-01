using Cysharp.Threading.Tasks;
using YGameTemplate.Infrastructure.AssetProviders;
using YGameTemplate.Infrastructure.Factory;

namespace CardBuildingGame.Infrastructure.Factories
{
    public class HUDSpawner : IHUDSpawner
    {
        private Factory<HUD> _factory;

        public HUDSpawner(IAssetProvider assetProvider)
        {
            _factory = new Factory<HUD>(assetProvider ,BundlePath.HUD);
        }

        public async UniTask<HUD> SpawnHUD()
        {
            HUD hud = await _factory.Create();
            return hud;
        }
    }
}