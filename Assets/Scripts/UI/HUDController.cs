using CardBuildingGame.Gameplay.Cards;
using CardBuildingGame.Gameplay.Stacks;
using System;
using UnityEngine;

public class HUDController
{
    private readonly HUD _hud;
    private readonly ICardReset _cardReset;
    private readonly IDeck _deck;

    public HUDController(HUD hud, ICardReset cardReset, IDeck deck)
    {
        _hud = hud;
        _cardReset = cardReset;
        _cardReset.Changed += SetCardResetText;
        _deck = deck;
        _deck.Changed += SetDeckText;
        _hud.EndTurnButtonPressed += EndTurn;
        SetCardResetText(cardReset.GetCards().Count);
        SetDeckText(deck.GetCards().Count);
    }

    public event Action EndTurnButtonPressed;

    public void SetDeckText(int count) => _hud.SetDeckText($"Deck: {count}");

    public void EndTurn() => EndTurnButtonPressed?.Invoke();

    public void SetCardResetText(int count) => _hud.SetCardResetText($"CardReset: {count}");

    public void SetRoomText(int roomNum) => _hud.SetRoomText($"Room {roomNum}/3");

    public void SetButtonInteractable(bool value) => _hud.SetButtonInteractable(value);

    public void SetEnergyText(int value) => _hud.SetEnergyText($"Energy: {value}");
}
