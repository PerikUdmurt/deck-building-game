using CardBuildingGame.Gameplay.Characters;
using UnityEngine;

namespace CardBuildingGame.Infrastructure.Factories
{
    public interface ICharacterSpawner
    {
        void DespawnCharacter(Character character);
        Character SpawnCharacterFromStaticData(string staticDataID, Vector3 atPosition);
    }
}