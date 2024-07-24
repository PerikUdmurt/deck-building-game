using UnityEngine;

namespace CardBuildingGame.Infrastructure.Factories
{
    public class HUDSpawner : IHUDSpawner
    {
        private Factory<HUD> _factory;

        public HUDSpawner()
        {
            _factory = new Factory<HUD>(AssetPath.HUD);
        }

        public HUD SpawnHUD()
        {
            HUD hud = _factory.Create();
            return hud;
        }
    }
}