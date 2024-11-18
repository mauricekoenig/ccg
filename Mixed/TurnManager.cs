



using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TurnManager : MonoBehaviour, ITurnManager {

    [SerializeField] private int turns;
    [SerializeField] private Player activePlayer;
    [SerializeField] private Player waitingPlayer;

    [SerializeField] private List<Player> playerList;

    public event Action OnStartOfTurn;
    public event Action OnEndOfTurn;
    public event Action<RuntimeCardData, int> OnCardDraw;

    public int Turns => turns;
    public Player ActivePlayer => activePlayer;
    public Player WaitingPlayer => waitingPlayer;

    public void Init (List<Player> playerList) {
;
        this.playerList = playerList;
        DetermineStartingPlayer();
        turns = 0;
    }
    public Player GetPlayerById (int id) {

        return playerList.Where(x => x.ID == id).FirstOrDefault();
    }
    private void DetermineStartingPlayer() {
        int startingPlayerIndex = UnityEngine.Random.Range(0, playerList.Count);
        activePlayer = playerList[startingPlayerIndex];
        waitingPlayer = playerList[1 - startingPlayerIndex];

    }


    public void StartTurn() {

        turns++;
        ActivePlayer.AddMana(1);
        DrawCard(ActivePlayer);
        ActivePlayer.ResetAttackStatusForCreaturesInPlay();
        OnStartOfTurn?.Invoke();
    }

    public void DrawStartHand() {

        turns++;
        ActivePlayer.AddMana(1);

        for (int i = 0; i < 3; i++) DrawCard (GetPlayerById(1));
        for (int i = 0; i < 3; i++) DrawCard (GetPlayerById(2));

        OnStartOfTurn?.Invoke();
    }

    public void EndTurn () {

        SwitchPlayers();
        OnEndOfTurn?.Invoke();
        StartTurn();
    }
    private void SwitchPlayers() {

        Player temp = activePlayer;
        activePlayer = waitingPlayer;
        waitingPlayer = temp;
    }

    public void DrawCard (Player player) {

        RuntimeCardData card = player.DrawCard();
        OnCardDraw?.Invoke(card, player.ID);
    }
}