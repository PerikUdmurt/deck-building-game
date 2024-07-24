using CardBuildingGame.Datas;
using CardBuildingGame.Gameplay.Characters;
using CardBuildingGame.Services;
using CardBuildingGame.StaticDatas;
using UnityEngine;

namespace CardBuildingGame.Infrastructure.Factories
{
    public class CharacterSpawner : ICharacterSpawner
    {
        private readonly IStaticDataService _staticDataService;
        private readonly LevelData _levelData;

        public CharacterSpawner(IStaticDataService staticDataService, LevelData levelData) 
        {
            _staticDataService = staticDataService;
            _levelData = levelData;
        }

        public Character SpawnCharacterFromStaticData(string staticDataID ,Vector3 atPosition)
        {
            _staticDataService.GetStaticData(staticDataID, out StaticData staticData);
            CharacterStaticData characterData = staticData as CharacterStaticData;

            GameObject obj = GameObject.Instantiate(characterData.Prefab, atPosition, UnityEngine.Quaternion.identity);

            obj.TryGetComponent(out Character character);
            character.Construct(characterData.Health, characterData.MaxHealth, characterData.Defense);
            _levelData.Characters.Add(character);
            character.Died += DespawnCharacter;

            return character;
        }

        public void DespawnCharacter(Character character) 
        {
            _levelData.Characters.Remove(character);
            GameObject.Destroy(character.gameObject);
        }
    }
}