using CardBuildingGame.Datas;
using CardBuildingGame.Gameplay.Cards;
using CardBuildingGame.Gameplay.Characters;
using CardBuildingGame.Services;
using CardBuildingGame.StaticDatas;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using YGameTemplate.Infrastructure.AssetProviders;
using YGameTemplate.Infrastructure.Factory;

namespace CardBuildingGame.Infrastructure.Factories
{
    public class CharacterSpawner : ICharacterSpawner
    {
        private readonly IStaticDataService _staticDataService;
        private readonly LevelData _levelData;
        private readonly AbstractFactory<Character> _factory;

        public CharacterSpawner(IStaticDataService staticDataService, LevelData levelData, IAssetProvider assetProvider) 
        {
            _staticDataService = staticDataService;
            _levelData = levelData;
            _factory = new(assetProvider);
        }

        public async UniTask<Character> SpawnCharacterFromStaticData(string CharacterID ,string deckID ,Vector3 atPosition)
        {
            List<CardData> cardDatas = GetCardDatasFromStaticData(deckID);
            
            GetCharacterData(CharacterID, out CharacterData characterData);

            Character character = await _factory.Create(characterData.BundlePath);
            character.transform.position = atPosition;
            character.Construct(characterData, cardDatas);
            _levelData.Characters.Add(character);
            character.Died += DespawnCharacter;

            return character;
        }

        private void GetCharacterData(string CharacterID, out CharacterData characterData)
        {
            _staticDataService.GetStaticData(CharacterID, out StaticData staticData);
            CharacterStaticData characterStaticData = staticData as CharacterStaticData;
            characterData = characterStaticData.ToCharacterData();
        }

        public void DespawnCharacter(Character character) 
        {
            _levelData.Characters.Remove(character);
            GameObject.Destroy(character.gameObject);
        }

        private List<CardData> GetCardDatasFromStaticData(string deckID)
        {
            _staticDataService.GetStaticData(deckID, out StaticData staticData);
            DeckStaticData staticDeck = staticData as DeckStaticData;

            List<CardData> cardDatas = staticDeck.ToCardDataList();

            return cardDatas;
        }
    }
}