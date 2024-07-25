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
        public static List<CardData> ToCardDataList(this DeckStaticData staticData )
        {
            List<CardData> cardDatas = new List<CardData>();

            foreach (CardStaticData cardStaticData in staticData.Cards)
            {
                CardData data = cardStaticData.ToCardData();
                if (data != null)
                    cardDatas.Add(data);
            }

            return cardDatas;
        }
    }
}