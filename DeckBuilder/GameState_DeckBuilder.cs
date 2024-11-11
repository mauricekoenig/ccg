


using System;

public class GameState_DeckBuilder {

    public Villain villain;

    public event Action<GameState_DeckBuilder_ChangeData, GameState_DeckBuilder_ChangeReason> OnGameStateChanged;

    public void Invoke_GameStateChanged (GameState_DeckBuilder_ChangeReason reason, GameState_DeckBuilder_ChangeData data) {
        OnGameStateChanged?.Invoke(data, reason);
    }
}