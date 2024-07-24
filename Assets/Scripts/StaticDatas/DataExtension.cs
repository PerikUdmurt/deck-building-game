using CardBuildingGame.Gameplay.Cards;
using CardBuildingGame.StaticDatas;
using System.Collections.Generic;

namespace CardBuildingGame
{
    public static class DataExtension
    {
        public static CardData ToCardData(this CardStaticData staticData)
            => new
                (
                energyCost: staticData.EnergyCost,
                effects: staticData.Effects,
                cardName: staticData.Name,
                sprite: staticData.Sprite,
                targetLayer: staticData.TargetLayer
                );
    }
}