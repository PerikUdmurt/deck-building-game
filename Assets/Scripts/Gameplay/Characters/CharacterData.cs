using static CardBuildingGame.Gameplay.Characters.Character;

namespace CardBuildingGame.Gameplay.Characters
{
    public struct CharacterData
    {
        public int Health;
        public int MaxHealth;
        public int Energy;
        public int MaxEnergy;
        public int Defense;
        public CharacterType CharacterType;
    }
}