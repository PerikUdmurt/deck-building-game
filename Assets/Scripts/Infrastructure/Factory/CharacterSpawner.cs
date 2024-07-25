﻿using CardBuildingGame.Datas;
using CardBuildingGame.Gameplay.Cards;
using CardBuildingGame.Gameplay.Characters;
using CardBuildingGame.Services;
using CardBuildingGame.StaticDatas;
using System.Collections.Generic;
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

        public Character SpawnCharacterFromStaticData(string CharacterID ,string deckID ,Vector3 atPosition)
        {
            List<CardData> cardDatas = GetCardDatasFromStaticData(deckID);

            _staticDataService.GetStaticData(CharacterID, out StaticData staticData);
            CharacterStaticData characterData = staticData as CharacterStaticData;

            GameObject obj = GameObject.Instantiate(characterData.Prefab, atPosition, UnityEngine.Quaternion.identity);

            obj.TryGetComponent(out Character character);
            character.Construct(characterData.Health, characterData.MaxHealth, characterData.Energy, characterData.Energy, cardDatas, characterData.Defense);
            _levelData.Characters.Add(character);
            character.Died += DespawnCharacter;

            return character;
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