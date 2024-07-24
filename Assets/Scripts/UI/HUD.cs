using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] private Button _endTurnButton;
    [SerializeField] private TextMeshProUGUI _deckText;
    [SerializeField] private TextMeshProUGUI _cardResetText;
    [SerializeField] private TextMeshProUGUI _floorText;
    [SerializeField] private TextMeshProUGUI _energyText;

    public event Action EndTurnButtonPressed;
    public void SetDeckText(string text) => _deckText.text = text;

    public void SetCardResetText(string text) => _cardResetText.text = text;

    public void SetRoomText(string text) => _floorText.text = text;

    public void SetButtonInteractable(bool value) => _endTurnButton.interactable = value;

    public void SetEnergyText(string text) => _energyText.text = text;

    private void OnEnable() => _endTurnButton.onClick.AddListener(InvokeButtonCallback);

    private void OnDisable() => _endTurnButton?.onClick.RemoveListener(InvokeButtonCallback);

    private void InvokeButtonCallback() => EndTurnButtonPressed?.Invoke();
}
