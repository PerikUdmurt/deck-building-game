using System;

namespace CardBuildingGame.Gameplay.Characters
{
    public class Energy
    {
        private int _energy;
        private int _maxEnergy;
        public Energy(int energy, int maxEnergy) 
        { 
            _energy = energy;
            _maxEnergy = maxEnergy;
        }

        public event Action<int> Changed;

        public int CurrentEnergy { get => _energy; }
        public int MaxEnergy { get => _maxEnergy; }

        public void SetEnergy(int newEnergy)
        {
            if (newEnergy < 0)
                _energy = 0;

            if (newEnergy > _maxEnergy)
                _energy = _maxEnergy;

            _energy = newEnergy;
            Changed?.Invoke(_energy);
        }

        public bool TrySpendEnergy(int energy)
        {
            if (energy > CurrentEnergy)
                return false;

            SetEnergy(_energy - energy);
            return true;
        }

        public void RestoreEnergy() => SetEnergy(MaxEnergy);
    }
}