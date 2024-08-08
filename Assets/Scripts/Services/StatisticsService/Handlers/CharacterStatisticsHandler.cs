using CardBuildingGame.Gameplay.Cards;
using CardBuildingGame.Gameplay.Characters;
using UnityEngine;
using YGameTemplate.Services.StatisticsService;
using static YGameTemplate.Services.StatisticsService.Statistics;

public class CharacterStatisticsHandler : IStatisticsModifier
{
    private readonly GameStatisticsService _statisticsService;

    public CharacterStatisticsHandler(GameStatisticsService statisticsService) 
    {
        _statisticsService = statisticsService;
    }

    public void ModifyStatistics(string targetStat, Statistics.ModifyType modifyType, int value)
    {
        _statisticsService.GeneneralStatistics.ModifyStatistics(targetStat, modifyType, value);
        Statistics levelStats = _statisticsService.GetStatistics(StandartStatisticsName.GameModeStatistics.ToString());
        levelStats.ModifyStatistics(targetStat, modifyType, value);
        Debug.Log(_statisticsService.GeneneralStatistics.GetStatisticsData().ToString());
    }

    public void AddCharacter(Character character)
        => character.Died += OnDied;

    public void RemoveCharacter(Character character)
        => character.Died -= OnDied;

    public void OnDied(Character character)
        => ModifyStatistics($"kill_{character.CurrentCharacterType}", ModifyType.Plus, 1);
}

public class CardStatisticsHandler : IStatisticsModifier
{
    private readonly GameStatisticsService _statisticsService;

    public CardStatisticsHandler(GameStatisticsService statisticsService)
    {
        _statisticsService = statisticsService;
    }
    public void ModifyStatistics(string targetStat, Statistics.ModifyType modifyType, int value)
    {
        _statisticsService.GeneneralStatistics.ModifyStatistics(targetStat, modifyType, value);
        Statistics levelStats = _statisticsService.GetStatistics(StandartStatisticsName.GameModeStatistics.ToString());
        levelStats.ModifyStatistics(targetStat, modifyType, value);
    }

    public void AddCard(ICard card)
        => card.Played += OnCardPlay;

    public void RemoveCard(ICard card)
        => card.Played -= OnCardPlay;

    private void OnCardPlay(ICard card) 
        => ModifyStatistics(card.CardData.CardName, ModifyType.Plus, 1);
}