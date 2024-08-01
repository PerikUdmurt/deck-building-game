using System;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

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

            int totalDamage = GetModifiedDamage(target, source);

            target.Health.GetDamage(totalDamage);
        }

        private int GetModifiedDamage(ICardTarget target, ICardTarget source)
        {
            int totalDamage = Damage;
            totalDamage += AddAttackBonus(source);
            totalDamage *= DoubleDamage(target);
            totalDamage -= AddArmor(target);

            return totalDamage;
        }

        private int AddAttackBonus(ICardTarget source)
        {
            if (source.statusHolder.Contains(Statuses.StatusType.AttackBonus))
                return source.statusHolder.GetStatusTotalValue(Statuses.StatusType.AttackBonus);
            else return 0;
        }

        private int DoubleDamage(ICardTarget target)
        {
            if (target.statusHolder.Contains(Statuses.StatusType.DoubleDamage))
            {
                int result = target.statusHolder.GetStatusTotalValue(Statuses.StatusType.DoubleDamage) * 2;
                target.statusHolder.ReduceAllStatus(Statuses.StatusType.DoubleDamage);
                return result;
            }
            else return 1;
        }

        private int AddArmor(ICardTarget target) 
        {
            if (target.statusHolder.Contains(Statuses.StatusType.Armor))
            {
                int result = target.statusHolder.GetStatusTotalValue(Statuses.StatusType.Armor) * 2;
                target.statusHolder.ReduceAllStatus(Statuses.StatusType.Armor);
                return result;
            }
            else return 0;
        }
    }
}