

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

public class OfflineGameManager : MonoBehaviour, IMediator {

    private bool gameStarted;
    public List<Player> playerList = new();
    public GameData gameData;


    ICardDeck testDeck1;
    ICardDeck testDeck2;

    Player currentPlayer;
    Player nonCurrentPlayer;

    CardViewManager cardViewManager;

    internal GameState gameState;
    internal IInputManager inputManager;
    internal ITurnManager turnManager;
    internal IEffectManager effectManager;
    internal IUIManager uiManager;
    internal IVillainAbilityManager villainAbilityManager;
    internal IPreLoader preLoader;

    public event Action<GameState> OnStartOfTurn;
    public event Action<GameState> OnEndOfTurn;
    public event Action<GameState> OnGameStateChanged;

    public event Action<GameState, CardView3D> OnCardPlayedFromHand;
    public event Action<GameState, CardView3D> OnCardReturnedToHand;
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
    }

    private void Start () {

        inputManager.ON_LeftClickedCardView += Handler_LeftClickedCardView;
        inputManager.ON_RightClickedCardView += Handler_RightClickedCardView;

        turnManager.OnStartOfTurn += Handler_OnStartOfTurn;
        turnManager.OnEndOfTurn += Handler_OnEndOfTurn;
        turnManager.OnCardDraw += Handler_OnCardDraw;

        cardViewManager.OnCardMovedToPlayZone += Handler_OnCardViewMovedToPlayZone;

        villainAbilityManager.OnVillainAbilityHandled += Handler_OnVillainAbilityHandled;

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

        for (int i = 0; i < playerList.Count; i++) {  
            
            if ((i + 1) == 1) playerList[i].Init(i + 1, testDeck1, testDeck1.Villain);
            else              playerList[i].Init(i+1, testDeck2, testDeck2.Villain);
        }

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
        Invoke_OnPlayerDrawCard(data, turnManager.ActivePlayer.ID);
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
    public void Invoke_OnPlayerDrawCard (RuntimeCardData data, int playerID) {
        OnPlayerDrawCard?.Invoke(GetGameState(), data, playerID);
    }
    public void Invoke_OnCardPlayedFromHand (CardView3D cardView) {

        OnCardPlayedFromHand?.Invoke(GetGameState(), cardView);
    }
    public void Invoke_ReturnCardFromBoardToHand(CardView3D cardView) {

        OnCardReturnedToHand?.Invoke(GetGameState(), cardView);
    }
    public void Invoke_OnFindEffectTriggered (CardPool pool) {

        OnFindEffectTriggered?.Invoke(GetGameState(), pool);
    }
    public void Invoke_OnCardInFindWindowSelected (RuntimeCardData data) {

        OnCardInFindWindowSelected?.Invoke(GetGameState(), data);
    }
    public void Invoke_OnVillainAbilityClicked (Villain villain) {
        OnVillainAbilityClicked?.Invoke(GetGameState(), villain);
    }
    public void Invoke_OnStartOfTurn () {

        OnStartOfTurn?.Invoke(GetGameState());
    }
    public void Invoke_OnEndOfTurn() {
        OnEndOfTurn?.Invoke(GetGameState());
    }

    // EVENT HANDLER
    public void Handler_LeftClickedCardView (CardView3D cardView) {

        cardView.Interact(GetGameState(), InputAction.LeftMouse);
    }
    public void Handler_RightClickedCardView (CardView3D cardView) {

        cardView.Interact(GetGameState(), InputAction.RightMouse);
        return;

        /*
        if (cardView.viewPosition == CardZone.Hand) return;
        CardZone from = CardZone.Play;
        CardZone to = CardZone.Hand;
        currentPlayer.cards.MoveCardBetweenZones(cardView.data, from, to);
        cardView.SetZone(CardZone.Hand);
        Invoke_GameStateChanged();
        Invoke_OnCardReturnedToHand(cardView);
        */
    }

    // HANDLER - TurnManager
    public void Handler_OnStartOfTurn () {
        Invoke_OnStartOfTurn();
    }
    public void Handler_OnEndOfTurn() {
        Invoke_OnEndOfTurn();
    }
    public void Handler_OnCardDraw (RuntimeCardData cardRuntimeData, int id) {
        Invoke_OnPlayerDrawCard(cardRuntimeData, id);
    }
    public void Handler_OnCardViewMovedToPlayZone (GameState state, CardView3D cardView) {

        if (!cardView.data.HasEffect()) {
            Debug.Log("Played a card without an effect.");
            return;
        }

        effectManager.Handle(cardView.data);
    }
    // HANDLER - VillainAbilityManager
    public void Handler_OnVillainAbilityHandled () {
        Invoke_GameStateChanged();
    }

    // GameState
    public void HandleInternalGameStateChange (GameStateChangeReason change, GameStateChangeData data) {

        switch (change) {

            case GameStateChangeReason.Action_PlayedCardFromHand:
                Invoke_OnCardPlayedFromHand(data.affectedView);
                break;

            case GameStateChangeReason.Action_ReturnedCardFromBoardToHand:
                Invoke_ReturnCardFromBoardToHand(data.affectedView);
                break;

            case GameStateChangeReason.EffectActivated_FindCards:
                Invoke_OnFindEffectTriggered(data.cardPool);
                break;

            case GameStateChangeReason.Input_ClickedOnCardInFindWindow:
                Invoke_OnCardInFindWindowSelected(data.cardData);
                break;

            case GameStateChangeReason.Input_ClickedOnVillainAbility:
                Invoke_OnVillainAbilityClicked(data.villain);
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