using CardBuildingGame.Gameplay.Characters;
using Cysharp.Threading.Tasks;
using UnityEngine;
using static CardBuildingGame.Gameplay.Characters.Character;

namespace CardBuildingGame.Infrastructure.Factories
{
    public interface ICharacterSpawner
    {
        void DespawnCharacter(Character character);
        UniTask<Character> SpawnCharacterFromStaticData(CharacterType type, int deckID, Vector3 atPosition);
    }
}