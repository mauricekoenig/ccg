


using System.Collections.Generic;
using System;

public interface IDeckBuilder {

    GameState_DeckBuilder GetGameState();
    event Action<HashSet<Villain>, GameState_DeckBuilder> OnVillainDataReceived;
    event Action<GameState_DeckBuilder_ChangeData> OnClickedOnVillain;
}