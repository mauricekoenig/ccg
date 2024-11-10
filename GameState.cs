


using System;
using System.Collections.Generic;
using Unity.VisualScripting;

public class GameState {

    private ITurnManager turnManager;
    public event Action <GameStateChangeReason, GameStateChangeData> OnChanged;

    public GameState (ITurnManager turnManager) {

        this.turnManager = turnManager;
    }

    public int Turns => turnManager.Turns;
    public Player ActivePlayer => turnManager.ActivePlayer;
    public Player WaitingPlayer => turnManager.WaitingPlayer;

    public Player GetPlayerById (int id) {
        return this.turnManager.GetPlayerById(id);
    }
    public void NotifyStateChange (GameStateChangeReason reason, GameStateChangeData data) {

        OnChanged?.Invoke(reason, data);
    }

}