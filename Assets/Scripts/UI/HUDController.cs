using CardBuildingGame.Gameplay.Cards;
using CardBuildingGame.Gameplay.Characters;
using CardBuildingGame.Gameplay.Stacks;
using System;

public class HUDController
{
    private readonly HUD _hud;
    private readonly ICardReset _cardReset;
    private readonly IDeck _deck;
    private readonly Energy _energy;

    public HUDController(HUD hud, ICardPlayer cardPlayer)
    {
        _hud = hud;
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

    public event Action EndTurnButtonPressed;

    public void SetDeckText(int count) => _hud.SetDeckText($"Deck: {count}");

    public void EndTurn() => EndTurnButtonPressed?.Invoke();

    public void SetCardResetText(int count) => _hud.SetCardResetText($"CardReset: {count}");

    public void SetRoomText(int roomNum) => _hud.SetRoomText($"Room {roomNum}/3");

    public void SetButtonInteractable(bool value) => _hud.SetButtonInteractable(value);

    public void SetEnergyText(int value) => _hud.SetEnergyText($"Energy: {value}");
}
