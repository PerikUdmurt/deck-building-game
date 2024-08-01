using System.Collections.Generic;

namespace CardBuildingGame.Gameplay.Statuses
{
    public interface IStatusHolder
    {
        Dictionary<Status, int> Statuses { get; }

        void AddStatus(Status status);
        bool Contains(StatusType status);
        List<KeyValuePair<Status, int>> GetStatusesByType(StatusType type);
        int GetStatusTotalValue(StatusType statusType);
        void ReduceAllStatus(StatusType statusType);
        void ReduceStatus(Status status);
        void RemoveStatus(Status status);
    }
}