using CardBuildingGame.Gameplay.Statuses;
using System.Collections.Generic;
using UnityEngine;

namespace CardBuildingGame.Gameplay.Cards
{
    [CreateAssetMenu(fileName = "NewAttackEffect", menuName = "StaticData/CardEffects/AttackEffect")]
    public class AttackEffect : CardEffectStaticData
    {
        public int Damage = 0;

        public override string TextForMarker { get => Damage.ToString(); }

        public override void Play(ICardTarget target, ICardTarget source = null)
        {
            source?.Animator.SetTrigger("attack");
            target.Health.GetDamage(Damage);
        }
    }

    [CreateAssetMenu(fileName = "NewStatusEffect", menuName = "StaticData/CardEffects/StatusEffect")]
    public class AddStatusToSelf : CardEffectStaticData
    {
        public Status Status;
        public int Value;

        public override string TextForMarker => Value.ToString();

        public override void Play(ICardTarget target, ICardTarget source = null)
        {
            
        }
    }
}