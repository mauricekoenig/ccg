



using System;
using System.Collections.Generic;

public interface ITurnManager {

    int Turns { get; }
    Player ActivePlayer { get; }
    Player WaitingPlayer { get; }

    event Action OnStartOfTurn;
    event Action OnEndOfTurn;

    event Action<CardRuntimeData, int> OnCardDraw;

    void StartTurn();
    void EndTurn();

    void Init (List<Player> playerList);
    Player GetPlayerById(int id);

    void DrawCard (Player player);
    void DrawStartHand();
}