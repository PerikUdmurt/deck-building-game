using CardBuildingGame.Gameplay.Characters;
using UnityEngine;

namespace CardBuildingGame.Infrastructure.Factories
{
    public interface ICharacterSpawner
    {
        void DespawnCharacter(Character character);
        Character SpawnCharacterFromStaticData(string CharacterID, string deckID, Vector3 atPosition);
    }
}