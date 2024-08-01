using CardBuildingGame.Gameplay.Characters;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CardBuildingGame.Infrastructure.Factories
{
    public interface ICharacterSpawner
    {
        void DespawnCharacter(Character character);
        UniTask<Character> SpawnCharacterFromStaticData(string CharacterID, string deckID, Vector3 atPosition);
    }
}