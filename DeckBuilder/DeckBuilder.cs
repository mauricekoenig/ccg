using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DeckBuilderUI))]
public class DeckBuilder : MonoBehaviour, IDeckBuilder
{
    public GameData gameData;
    private GameState_DeckBuilder gameState;

    private IPreLoader preLoader;
    private DeckBuilderUI deckBuilderUI;

    public event Action<HashSet<Villain>, GameState_DeckBuilder> OnVillainDataReceived;
    public event Action<GameState_DeckBuilder_ChangeData> OnClickedOnVillain;
    public event Action<DeckPreview> OnClickedOnDeckPreview;
    public event Action<GameState_DeckBuilder> OnDeckBuilderEntered;
    public event Action OnClickedOnBackToDeckPreviewArrow;

    private void Awake() {

        preLoader = GetComponent<IPreLoader>();
        deckBuilderUI = GetComponent<DeckBuilderUI>();
    }

    private void Start() {

        this.gameState = new GameState_DeckBuilder();
        this.gameState.OnGameStateChanged += HandleGameStateChange;

        if (RuntimeGameData.Initialized)
            OnDeckBuilderEntered?.Invoke(this.GetGameState());
    }

    public void HandleGameStateChange (GameState_DeckBuilder_ChangeData data, GameState_DeckBuilder_ChangeReason reason) {

        switch (reason) {

            case GameState_DeckBuilder_ChangeReason.Input_ClickedOnVillain:
                OnClickedOnVillain?.Invoke(data);
                break;

            case GameState_DeckBuilder_ChangeReason.Input_ClickedOnDeckPreview:
                OnClickedOnDeckPreview?.Invoke(data.deckPreview);
                break;

            case GameState_DeckBuilder_ChangeReason.Input_ClickedOnBackToDeckPreviewArrow:
                OnDeckBuilderEntered?.Invoke(this.GetGameState());
                break;
        }
    }

    public void GetVillainData () {

        var villains = gameData.runtimeGameData.GetAllVillains();
        OnVillainDataReceived?.Invoke(villains, GetGameState());
    }

    public GameState_DeckBuilder GetGameState() {
        return this.gameState;
    }

}
