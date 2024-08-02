using CardBuildingGame.Gameplay.Cards;
using CardBuildingGame.Gameplay.Characters;
using System.Linq;

namespace CardBuildingGame.Gameplay.Statuses
{
    public class StatusPlayer
    {
        private readonly StatusActions _actions;

        public StatusPlayer()
        {
            _actions = new StatusActions();
        }

        public void ExecuteAllStatuses(ICardTarget character)
        {
            var statuses = character.StatusHolder.Statuses.ToList();

            for (int i = 0; i < statuses.Count(); i++)
            {
                _actions.Execute(statuses[i].Key.statusType, statuses[i].Value,character);
                character.StatusHolder.ReduceStatus(statuses[i].Key);
            }
        }

        public void ExecuteStatus(Character character, StatusType statusType)
        {
            var statuses = from c in character.StatusHolder.Statuses
                       where c.Key.statusType == statusType
                       select c;

            var statusList = statuses.ToList();

            for (int i = 0; i < statusList.Count(); i++)
            {
                _actions.Execute(statusType, statusList[i].Value, character);
                character.StatusHolder.ReduceStatus(statusList[i].Key);
            }
        }
    }
}