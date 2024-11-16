using System;
using UnityEngine;

public interface IMediator {

    event Action <GameState> OnGameStateChanged;

    event Action <GameState> OnStartOfTurn;
    event Action <GameState> OnEndOfTurn;

    event Action <GameState, RuntimeCardData, int> OnPlayerDrawCard;
    event Action <GameState, ICardView> OnCardPlayedFromHand;
    event Action <GameState, ICardView> OnCardReturnedToHand;
    event Action <GameState, CardPool> OnFindEffectTriggered;
    event Action <GameState, RuntimeCardData> OnCardInFindWindowSelected;
    event Action <GameState, Villain> OnVillainAbilityClicked;

    GameState GetGameState();
    void StartGame();
    void EndPlayerTurn();
    void DrawForActivePlayer();
}