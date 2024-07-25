using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] private CanvasGroup _mainGameCanvas;
    [SerializeField] private Button _endTurnButton;
    [SerializeField] private TextMeshProUGUI _deckText;
    [SerializeField] private TextMeshProUGUI _cardResetText;
    [SerializeField] private TextMeshProUGUI _floorText;
    [SerializeField] private TextMeshProUGUI _energyText;
    [SerializeField] private CanvasGroup _pauseCanvas;
    [SerializeField] private CanvasGroup _endGameCanvas;
    [SerializeField] private Button _endGameButton;
    [SerializeField] private TextMeshProUGUI _endGameText;

    public event Action EndTurnButtonPressed;
    public event Action EndGameButtonPressed;

    public CanvasGroup MainGameCanvas { get => _mainGameCanvas; }
    public CanvasGroup PauseCanvas { get => _pauseCanvas; }
    public CanvasGroup EndGameCanvas { get => _endGameCanvas; }

    public void SetDeckText(string text) => _deckText.text = text;

    public void SetCardResetText(string text) => _cardResetText.text = text;

    public void SetRoomText(string text) => _floorText.text = text;

    public void SetEndGameText(string text) => _endGameText.text = text;

    public void SetButtonInteractable(bool value) => _endTurnButton.interactable = value;

    public void SetEnergyText(string text) => _energyText.text = text;

    private void OnEnable() 
    { 
        _endTurnButton.onClick.AddListener(EndTurnCallback);
        _endGameButton.onClick.AddListener(EndGameCallback);
    }

    private void OnDisable()
    {
        _endTurnButton.onClick.RemoveListener(EndTurnCallback);
        _endGameButton.onClick.RemoveListener(EndGameCallback);
    }

    private void EndTurnCallback() => EndTurnButtonPressed?.Invoke();

    private void EndGameCallback() => EndGameButtonPressed?.Invoke();
}
