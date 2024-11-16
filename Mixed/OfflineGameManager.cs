

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
[RequireComponent(typeof(AnimationManager))]
[RequireComponent(typeof(PlayerManager))]

public class OfflineGameManager : MonoBehaviour, IMediator {


    private bool gameStarted;

    public GameData gameData;
    private ICardDeck testDeck1;
    private ICardDeck testDeck2;

    public Player player1;
    public Player player2;

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
    private IAnimationManager animationManager;
    private IPlayerManager playerManager;

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
        animationManager = GetComponent<IAnimationManager>();
        playerManager = GetComponent<IPlayerManager>();
    }

    private void Start () {

        inputManager.OnLeftClickedCardView += Handler_LeftClickedCardView;
        inputManager.OnRightClickedCardView += Handler_RightClickedCardView;

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

        this.playerManager.Init(player1, player2); 
        this.turnManager.Init(this.playerManager.PlayerList);
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

    public void MaxMana() {

        foreach (var player in this.playerManager.PlayerList) {
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

        Debug.Log("Right Clicked while targeting!");
        this.targetingManager.EndTargeting();
    }
    private void Handler_OnLeftClickWhileTargeting() {

        Debug.Log("Left Clicked while targeting!");
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

            case GameStateChangeReason.Input_LeftClickedOnCardInPlay:
                this.targetingManager.StartTargeting(data.affectedView);
                break;

            case GameStateChangeReason.Input_LeftClickedOnCardInPlay_WhileTargeting:
                // Attack.
                this.targetingManager.EndTargeting();
                this.playerManager.Init(null, null);
                this.animationManager.Attack(TargetingManager.CurrentViewTargeting, data.affectedView);
                
                break;
        }

        Invoke_GameStateChanged();
    }

    // UTILITY
    public void ResetScene () {
        SceneManager.LoadScene(0);
    }
}