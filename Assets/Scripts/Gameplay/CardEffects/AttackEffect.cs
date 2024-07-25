using UnityEngine;

namespace CardBuildingGame.Gameplay.Cards
{
    [CreateAssetMenu(fileName = "NewAttackEffect", menuName = "StaticData/CardEffects/AttackEffect")]
    public class AttackEffect : CardEffect
    {
        public int Damage = 0;

        public override void Play(ICardTarget target, ICardTarget source = null)
        {
            source?.Animator.SetTrigger("attack");
            target.Health.GetDamage(Damage);
        }
    }
}