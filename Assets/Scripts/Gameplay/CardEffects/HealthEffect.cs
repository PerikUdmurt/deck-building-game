using UnityEngine;

namespace CardBuildingGame.Gameplay.Cards
{
    [CreateAssetMenu(fileName = "NewHealthEffect", menuName = "StaticData/CardEffects/HealthEffect")]
    public class HealthEffect : CardEffect
    {
        public int HealthPoints = 0;

        public override void Play(ICardTarget target, ICardTarget source = null)
        {
            target.Health.GetHealth(HealthPoints);
        }
    }
}