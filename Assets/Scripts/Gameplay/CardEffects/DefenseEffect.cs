using UnityEngine;

namespace CardBuildingGame.Gameplay.Cards
{
    [CreateAssetMenu(fileName = "NewDefenseEffect", menuName = "StaticData/CardEffects/DefenseEffect")]
    public class DefenseEffect : CardEffect
    {
        public int DefensePoints = 0;

        public override void Play(ICardTarget target, ICardTarget source = null)
        {
            target.Health.GetDefense(DefensePoints);
        }
    }
}