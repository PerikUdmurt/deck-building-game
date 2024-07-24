using UnityEngine;

namespace CardBuildingGame.Gameplay.Cards
{
    [CreateAssetMenu(fileName = "NewAttackEffect", menuName = "StaticData/CardEffects/AttackEffect")]
    public class AttackEffect : CardEffect
    {
        public int Damage = 0;

        public override void Play(ICardTarget target)
        {
            target.Health.GetDamage(Damage);
        }
    }
}