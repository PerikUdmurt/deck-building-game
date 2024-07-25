using CardBuildingGame.Gameplay.Cards;
using CardBuildingGame.Gameplay.Characters;
using CardBuildingGame.Gameplay.Stacks;
using CardBuildingGame.Infrastructure.StateMachine;
using System;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class HUDController
{
    private HUD _hud;
    private ICardReset _cardReset;
    private IDeck _deck;
    private Energy _energy;

    public HUDController(HUD hud, ICardPlayer cardPlayer, GameStateMachine gameStateMachine)
    {
        _hud = hud;
        BindMainGameCanvas(hud, cardPlayer);
        BindEndGameCanvas(hud, gameStateMachine);
    }


    public event Action EndTurnButtonPressed;

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

    private void BindMainGameCanvas(HUD hud, ICardPlayer cardPlayer)
    {
        _cardReset = cardPlayer.CardReset;
        _cardReset.Changed += SetCardResetText;
        _deck = cardPlayer.Deck;
        _deck.Changed += SetDeckText;
        _energy = cardPlayer.Energy;
        _energy.Changed += SetEnergyText;
        _hud.EndTurnButtonPressed += EndTurn;
        SetCardResetText(_cardReset.GetCards().Count);
        SetDeckText(_deck.GetCards().Count);
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