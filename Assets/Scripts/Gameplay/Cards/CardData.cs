using CardBuildingGame.Gameplay.Characters;
using System.Collections.Generic;
using UnityEngine;

namespace CardBuildingGame.Gameplay.Cards
{
    public class CardData
    {
        public string CardName { get; private set; }
        public int EnergyCost { get; private set; }
        public TargetLayer TargetLayer { get; private set;}
        public Sprite Sprite { get; private set; }
        public List<CardEffect> Effects { get; private set; }


        public CardData(int energyCost, List<CardEffect> effects, string cardName, Sprite sprite, TargetLayer targetLayer)
        {
            EnergyCost = energyCost;
            Effects = effects;
            CardName = cardName;
            Sprite = sprite;
            TargetLayer = targetLayer;
        }

        public void SetEnergyCost(int newCost)
        {
            if (newCost < 0) newCost = 0;
            EnergyCost = newCost;
        }
    }
}