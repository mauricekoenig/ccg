using System;
using UnityEngine;

public interface IMediator {

    event Action <GameState> OnGameStateChanged;

    event Action <GameState> OnStartOfTurn;
    event Action <GameState> OnEndOfTurn;

    event Action <GameState, CardRuntimeData, int> OnPlayerDrawCard;
    event Action <GameState, CardView3D> OnCardPlayedFromHand;
    event Action <GameState, CardView3D> OnCardReturnedToHand;
    event Action <GameState, CardPool> OnFindEffectTriggered;
    event Action <GameState, CardRuntimeData> OnCardInFindWindowSelected;
    event Action <GameState, Villain> OnVillainAbilityClicked;

    GameState GetGameState();
    void StartGame();
    void EndPlayerTurn();
    void DrawForActivePlayer();
    void DiscardRandomCard();
    void PlayRandomCardFromHand();
    bool CardIsPlayable(CardRuntimeData card);
}