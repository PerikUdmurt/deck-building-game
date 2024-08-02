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
using YGameTemplate.Services.StatisticsService;
using static CardBuildingGame.Gameplay.Characters.Character;

namespace CardBuildingGame.Infrastructure.Factories
{
    public class CharacterSpawner : ICharacterSpawner
    {
        private readonly IStaticDataService _staticDataService;
        private readonly LevelData _levelData;
        private readonly AbstractFactory<Character> _factory;
        private readonly CharacterStatisticsHandler _statsHandler;

        public CharacterSpawner(IStaticDataService staticDataService, LevelData levelData, IAssetProvider assetProvider, GameStatisticsService statServise) 
        {
            _staticDataService = staticDataService;
            _levelData = levelData;
            _factory = new(assetProvider);
            _statsHandler = new(statServise);
        }

        public async UniTask<Character> SpawnCharacterFromStaticData(CharacterType type, int deckID ,Vector3 atPosition)
        {
            List<CardData> cardDatas = GetCardDatasFromStaticData(type, deckID);
            
            GetCharacterData(type.ToString(), out CharacterData characterData);

            Character character = await _factory.Create(type.ToString());
            character.transform.position = atPosition;
            character.Construct(characterData, cardDatas);
            _levelData.Characters.Add(character);
            character.Died += DespawnCharacter;
            _statsHandler.AddCharacter(character);

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
            _statsHandler.RemoveCharacter(character);
            _levelData.Characters.Remove(character);
            character.Died -= DespawnCharacter;
            GameObject.Destroy(character.gameObject);
        }

        private List<CardData> GetCardDatasFromStaticData(CharacterType type, int deckID)
        {
            string totalID = type.ToString() + deckID.ToString();
            _staticDataService.GetStaticData(totalID, out StaticData staticData);
            DeckStaticData staticDeck = staticData as DeckStaticData;

            List<CardData> cardDatas = staticDeck.ToCardDataList();

            return cardDatas;
        }
    }
}