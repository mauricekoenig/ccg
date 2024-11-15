

using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CardViewManager))]
[RequireComponent(typeof(DevInputManager))]
[RequireComponent(typeof(TurnManager))]
[RequireComponent(typeof(EffectManager))]
[RequireComponent(typeof(UIManager))]
[RequireComponent(typeof(VillainAbilityManager))]
[RequireComponent(typeof(TargetingManager))]

public class OfflineGameManager : MonoBehaviour, IMediator {


    private bool gameStarted;
    public List<Player> playerList = new();
    public GameData gameData;

    private ICardDeck testDeck1;
    private ICardDeck testDeck2;

    private Player currentPlayer;
    private Player nonCurrentPlayer;

    private CardViewManager cardViewManager;
    private GameState gameState;
    private IInputManager inputManager;
    private ITurnManager turnManager;
    private IEffectManager effectManager;
    private IUIManager uiManager;
    private IVillainAbilityManager villainAbilityManager;
    private IPreLoader preLoader;
    private ITargetingManager targetingManager;
    private InterruptionZones interruptionZones;

    public event Action<GameState> OnStartOfTurn;
    public event Action<GameState> OnEndOfTurn;
    public event Action<GameState> OnGameStateChanged;

    public event Action<GameState, ICardView> OnCardPlayedFromHand;
    public event Action<GameState, ICardView> OnCardReturnedToHand;
    public event Action<GameState, RuntimeCardData, int> OnPlayerDrawCard;
    public event Action<GameState, CardPool> OnFindEffectTriggered;
    public event Action<GameState, RuntimeCardData> OnCardInFindWindowSelected;
    public event Action<GameState, Villain> OnVillainAbilityClicked;

    void Awake () {

        inputManager = GetComponent<IInputManager>();
        turnManager = GetComponent<ITurnManager>();
        effectManager = GetComponent<IEffectManager>();
        cardViewManager = GetComponent<CardViewManager>();
        uiManager = GetComponent<IUIManager>();
        villainAbilityManager = GetComponent<IVillainAbilityManager>();
        preLoader = GetComponent<IPreLoader>();
        targetingManager = GetComponent<ITargetingManager>();
        interruptionZones = GetComponent<InterruptionZones>();
    }

    private void Start () {

        inputManager.OnLeftClickedCardView += Handler_LeftClickedCardView;
        inputManager.OnRightClickedCardView += Handler_RightClickedCardView;
        inputManager.OnRightClickWhileTargeting += Handler_OnRightClickWhileTargeting;

        turnManager.OnStartOfTurn += Handler_OnStartOfTurn;
        turnManager.OnEndOfTurn += Handler_OnEndOfTurn;
        turnManager.OnCardDraw += Handler_OnCardDraw;

        cardViewManager.OnCardMovedToPlayZone += Handler_OnCardViewMovedToPlayZone;

        villainAbilityManager.OnVillainAbilityHandled += Handler_OnVillainAbilityHandled;

        interruptionZones.OnInterruptionZoneEntered += Handler_OnInterruptionZoneEntered;

    }

    private void Handler_OnInterruptionZoneEntered() {
        this.targetingManager.EndTargeting();
    }

    public GameState GetGameState () {
        return this.gameState;
    }
    public void StartGame () {

        if (gameStarted) return;

        if (!RuntimeGameData.Initialized) {
            gameData.Init();

            preLoader.Run(PreLoaderAction.LoadAllCreatures);
            preLoader.Run(PreLoaderAction.LoadAllEffects);
            preLoader.Run(PreLoaderAction.LoadAllKeywordAssociations);
            preLoader.Run(PreLoaderAction.LoadAllVillains);
            preLoader.Run(PreLoaderAction.LoadAllCardSprites);
            preLoader.Run(PreLoaderAction.LoadAllDecks);
            RuntimeGameData.Initialized = true;
        }

        testDeck1 = gameData.runtimeGameData.GetTestDeck();
        testDeck2 = gameData.runtimeGameData.GetTestDeck();

        playerList[0].Init(1, testDeck1, testDeck1.Villain);
        playerList[1].Init(2, testDeck2, testDeck2.Villain);

        this.turnManager.Init(playerList);
        this.villainAbilityManager.Init(this);

        this.gameState = new GameState(this.turnManager);
        this.gameState.OnChanged += HandleInternalGameStateChange;

        this.uiManager.CreateVillains(this.gameState);
        this.turnManager.DrawStartHand();

        gameStarted = true;

        Invoke_GameStateChanged();
    }

    // BUTTONS
    public void EndPlayerTurn () {

        turnManager.EndTurn();
    }
    public void DrawForActivePlayer () {

        RuntimeCardData data = turnManager.ActivePlayer.DrawCard();
        Invoke_GameStateChanged();
        OnPlayerDrawCard?.Invoke(GetGameState(), data, turnManager.ActivePlayer.ID);
    }
    public void DiscardRandomCard () {

        if (currentPlayer.cards.HandSize == 0) return;
        RuntimeCardData randomCard = currentPlayer.cards.GetRandomCardInHand();
        currentPlayer.cards.MoveCardBetweenZones(randomCard, CardZone.Hand, CardZone.Graveyard);
        Invoke_GameStateChanged();


    }
    public void PlayRandomCardFromHand () {

        if (currentPlayer.cards.HandSize == 0) return;
        RuntimeCardData card = currentPlayer.cards.GetRandomCardInHand();
        currentPlayer.cards.MoveCardBetweenZones(card, CardZone.Hand, CardZone.Play);

        if (card is CreatureRuntimeCardData) {

            CreatureRuntimeCardData creature = card as CreatureRuntimeCardData;

            foreach (var effect in creature.Effects) {
                foreach (var trigger in effect.triggers) {
                    if (trigger == EffectTrigger.Play) {
                        effect.Apply(GetGameState());
                    }
                }
            }

            Debug.Log("Played a creature!");
        }

        if (card is SpellRuntimeCardData) {

            SpellRuntimeCardData spell = card as SpellRuntimeCardData;
            Debug.Log("Played a spell!");
        }

        Invoke_GameStateChanged();
    }
    public void MaxMana() {

        foreach (var player in playerList) {
            player.resources.currentMana = player.resources.maxMana;
        }

        Invoke_GameStateChanged();
    }

    // INVOKE EVENTS
    public void Invoke_GameStateChanged () {

        OnGameStateChanged?.Invoke(this.gameState);
    }

    // HANDLER - InputManager
    public void Handler_LeftClickedCardView (ICardView cardView) {

        cardView.Interact(GetGameState(), InputAction.LeftMouse);
    }
    public void Handler_RightClickedCardView (ICardView cardView) {

        cardView.Interact(GetGameState(), InputAction.RightMouse);
        return;
    }

    public void Handler_OnRightClickWhileTargeting () {
        this.targetingManager.EndTargeting();
    }

    // HANDLER - TurnManager
    public void Handler_OnStartOfTurn () {
        OnStartOfTurn?.Invoke(GetGameState());
    }
    public void Handler_OnEndOfTurn() {
        OnEndOfTurn?.Invoke(GetGameState());
    }
    public void Handler_OnCardDraw (RuntimeCardData cardRuntimeData, int id) {
        OnPlayerDrawCard?.Invoke(GetGameState(), cardRuntimeData, id);
    }
    public void Handler_OnCardViewMovedToPlayZone (GameState state, ICardView cardView) {

        if (!cardView.Data.HasEffect()) {
            return;
        }

        effectManager.Handle(cardView.Data);
    }

    // HANDLER - VillainAbilityManager
    public void Handler_OnVillainAbilityHandled () {
        Invoke_GameStateChanged();
    }

    // HANDLER - GameState
    public void HandleInternalGameStateChange (GameStateChangeReason change, GameStateChangeData data) {


        switch (change) {

            case GameStateChangeReason.Action_PlayedCardFromHand:
                OnCardPlayedFromHand?.Invoke(GetGameState(), data.affectedView);
                break;

            case GameStateChangeReason.Action_ReturnedCardFromBoardToHand:
                OnCardReturnedToHand?.Invoke(GetGameState(), data.affectedView);
                break;

            case GameStateChangeReason.EffectActivated_FindCards:
                OnFindEffectTriggered?.Invoke(GetGameState(), data.cardPool);
                break;

            case GameStateChangeReason.Input_ClickedOnCardInFindWindow:
                OnCardInFindWindowSelected?.Invoke(GetGameState(), data.cardData);
                break;

            case GameStateChangeReason.Input_ClickedOnVillainAbility:
                OnVillainAbilityClicked?.Invoke(GetGameState(), data.villain);
                break;

            case GameStateChangeReason.Input_LeftClickedOnFriendlyCardInPlay:
                this.targetingManager.StartTargeting(data.affectedView);
                break;
        }

        Invoke_GameStateChanged();
    }

    // UTILITY
    public void ResetScene () {
        SceneManager.LoadScene(0);
    }
    public bool CardIsPlayable (RuntimeCardData data) {

        return currentPlayer.resources.currentMana >= data.Cost;
    }
}