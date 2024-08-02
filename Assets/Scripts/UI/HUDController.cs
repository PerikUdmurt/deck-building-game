using CardBuildingGame.Gameplay.Cards;
using CardBuildingGame.Gameplay.Characters;
using CardBuildingGame.Gameplay.Stacks;
using CardBuildingGame.Infrastructure.StateMachine;
using System;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using YGameTemplate.Infrastructure.Score;
using YGameTemplate.Services.StatisticsService;

public class HUDController
{
    private readonly Character _character;
    private readonly GameStatisticsService _gameStatisticsService;
    private HUD _hud;
    private ScoreSystem _scoreSystem;
    private ICardReset _cardReset;
    private IDeck _deck;
    private Energy _energy;

    public HUDController(HUD hud, Character player, GameStateMachine gameStateMachine, ScoreSystem scoreSystem, GameStatisticsService gameStatisticsService)
    {
        _hud = hud;
        _character = player;
        _scoreSystem = scoreSystem;
        _scoreSystem.ScoreChanged += SetScoreText;
        player.Died += OnGameOver;
        _gameStatisticsService = gameStatisticsService;
        BindMainGameCanvas(hud, player);
        BindEndGameCanvas(hud, gameStateMachine);
    }


    public event Action EndTurnButtonPressed;

    public void SetScoreText(int score) => _hud.SetScoreText($"Score: {score}");

    public void SetDeckText(int count) => _hud.SetDeckText($"Deck: {count}");

    public void EndTurn() => EndTurnButtonPressed?.Invoke();

    public void SetCardResetText(int count) => _hud.SetCardResetText($"CardReset: {count}");

    public void SetRoomText(int roomNum, int maxRoomNum) => _hud.SetRoomText($"Room {roomNum}/{maxRoomNum}");

    public void SetButtonInteractable(bool value) => _hud.SetButtonInteractable(value);

    public void SetEnergyText(int value) => _hud.SetEnergyText($"Energy: {value}");

    public void SetEndGameText(string value) => _hud.SetEndGameText(value);

    public void OnGameOver(Character character)
    {
        Debug.Log("GameOver");
        SetEndGameText("GameOver");
        ShowEndGameCanvas();
    }

    public void OnVictory() 
    {
        Debug.Log("Victory");
        SetEndGameText("Victory");
        ShowEndGameCanvas();
    }

    public void CleanUp()
    {
        _cardReset.Changed -= SetCardResetText;
        _deck.Changed -= SetDeckText;
        _energy.Changed -= SetEnergyText;
        _hud.EndTurnButtonPressed -= EndTurn;
        _character.Died -= OnGameOver;
    }

    private void BindMainGameCanvas(HUD hud, Character character)
    {
        _cardReset = character.CardPlayer.CardReset;
        _cardReset.Changed += SetCardResetText;
        _deck = character.CardPlayer.Deck;
        _deck.Changed += SetDeckText;
        _energy = character.CardPlayer.Energy;
        _energy.Changed += SetEnergyText;
        _hud.EndTurnButtonPressed += EndTurn;
        SetCardResetText(_cardReset.GetCards().Count);
        SetDeckText(_deck.GetCards().Count);
        SetScoreText(_scoreSystem.GetScore());
    }

    private void BindEndGameCanvas(HUD hud, GameStateMachine gameStateMachine)
    {
        hud.EndGameButtonPressed += () => gameStateMachine.Enter<MainMenuState>();
    }

    private void ShowEndGameCanvas()
    {
        _hud.MainGameCanvas.SetActive(false);
        _hud.PauseCanvas.SetActive(false);
        _hud.EndGameCanvas.SetActive(true);
    }
}