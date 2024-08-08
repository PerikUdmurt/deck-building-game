using System;
using System.Collections.Generic;
using System.Linq;

namespace CardBuildingGame.Gameplay.Statuses
{
    public class StatusHolder: IStatusHolder
    {
        private Dictionary<Status, int> _statuses = new Dictionary<Status, int>();

        public StatusHolder(List<(Status, int)> initialStatuses = null) 
        { 
            if (initialStatuses != null)
                foreach (var pair in initialStatuses)
                    for (int i = 0; i < pair.Item2; i++)
                        AddStatus(pair.Item1);
        }

        public Dictionary<Status, int> Statuses { get => _statuses; }

        public event Action Changed;

        public void AddStatus(Status status)
        {
            if (_statuses.ContainsKey(status))
            {
                _statuses[status]++; 
                return;
            }
            _statuses.Add(status, 1);
            Changed?.Invoke();
        }

        public List<KeyValuePair<Status, int>> GetStatusesByType(StatusType type) 
        {
            var statuses = from c in _statuses
                           where c.Key.statusType == type
                           select c;
            return statuses.ToList();
        }

        public bool Contains(StatusType status)
        {
            if (_statuses.Any(c => c.Key.statusType == status))
                return true;

            return false;
        }

        public void RemoveStatus(Status status)
        {
            if (_statuses.ContainsKey(status))
                _statuses.Remove(status);
            Changed?.Invoke();
        }

        public void ReduceStatus(Status status)
        {
            if (!_statuses.ContainsKey(status) || status.isPerpetual)
                return;

            if (status.isFullDecimate)
            {
                RemoveStatus(status);
                return;
            }

            _statuses[status]--;
            CheckStatus();
            Changed?.Invoke();
        }

        public void ReduceAllStatus(StatusType statusType)
        {
            if (!Contains(statusType))
                return;

            var list = GetStatusesByType(statusType);

            for (int i = 0; i < list.Count; i++)
                ReduceStatus(list[i].Key);

            Changed?.Invoke();
        }

        public int GetStatusTotalValue(StatusType statusType)
        {
            if (!Contains(statusType))
                return 0;

            var list = GetStatusesByType(statusType);
            int result = list.Sum(c => c.Value);
            return result;
        }

        private void CheckStatus()
        {
            var statusList = _statuses.ToList();

            for (int i = 0; i < _statuses.Count; i++)
                if (statusList[0].Value <= 0 && !statusList[0].Key.isPerpetual)
                    RemoveStatus(statusList[0].Key);
        }
    }
}