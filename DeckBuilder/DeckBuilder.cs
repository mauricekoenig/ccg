using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DeckBuilderUI))]
public class DeckBuilder : MonoBehaviour, IDeckBuilder
{
    public GameData gameData;
    private GameState_DeckBuilder gameState;

    [SerializeField] private GameObject cardViewCollection_Prefab;
    [SerializeField] private Transform cardViewCollection_Parent;

    private IPreLoader preLoader;
    private DeckBuilderUI deckBuilderUI;

    public event Action<HashSet<Villain>, GameState_DeckBuilder> OnVillainDataReceived;
    public event Action<GameState_DeckBuilder_ChangeData> OnClickedOnVillain;

    private void Awake() {
        preLoader = GetComponent<IPreLoader>();
        deckBuilderUI = GetComponent<DeckBuilderUI>();
    }
    private async void Start() {

        await preLoader.Run(PreLoaderAction.LoadAllVillains);
        await preLoader.Run(PreLoaderAction.LoadAllEffects);
        await preLoader.Run(PreLoaderAction.LoadAllCreatures);

        preLoader.Run(PreLoaderAction.LoadAllCardSprites);

        this.gameState = new GameState_DeckBuilder();
        this.gameState.OnGameStateChanged += HandleGameStateChange;
    }

    public void HandleGameStateChange (GameState_DeckBuilder_ChangeData data, GameState_DeckBuilder_ChangeReason reason) {

        switch (reason) {

            case GameState_DeckBuilder_ChangeReason.Input_ClickedOnVillain:
                OnClickedOnVillain?.Invoke(data);
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
