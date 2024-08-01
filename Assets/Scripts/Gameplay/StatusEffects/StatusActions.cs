using CardBuildingGame.Gameplay.Cards;
using CardBuildingGame.Gameplay.Characters;

namespace CardBuildingGame.Gameplay.Statuses
{
    public class StatusActions
    {
        public void Execute(StatusType statusType, int value, ICardTarget target)
        {
            switch (statusType)
            {
                case StatusType.Bleeding:
                    OnBleeding(target, value);
                    break;
                case StatusType.Poisoning: 
                    break;
                case StatusType.Armor: 
                    break;
                case StatusType.AttackBonus: 
                    break;
                case StatusType.Health: 
                    OnHealth(target);
                    break;
                case StatusType.DoubleDamage:
                    break;
            }
        }

        private void OnBleeding(ICardTarget target, int value)
        {
            target.Health.GetDamage(value, Health.DamageType.Directly);
        }

        private void OnHealth(ICardTarget target) 
        {
            target.Health.GetHealth(1);
        }
    }
}