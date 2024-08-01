using CardBuildingGame.Gameplay.Statuses;
using UnityEngine;

namespace CardBuildingGame.Gameplay.Cards
{
    [CreateAssetMenu(fileName = "NewStatusEffect", menuName = "StaticData/CardEffects/StatusEffect")]
    public class StatusEffect : CardEffectStaticData
    {
        public StatusType Status;
        public int Value;
        public bool isPerpetual;
        public bool isStackable;
        public bool isFullDecimate;


        public override string TextForMarker => Value.ToString();

        public override void Play(ICardTarget target, ICardTarget source = null)
        {
            Status newStatus = new()
            {
                isPerpetual = isPerpetual,
                isStackable = isStackable,
                statusType = Status,
                isFullDecimate = isFullDecimate,
            };

            for(int i = 0; i < Value; i++)
                target.statusHolder.AddStatus(newStatus);
        }
    }
}