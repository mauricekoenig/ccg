


using System.Collections.Generic;
using System;

public interface IDeckBuilder {

    GameState_DeckBuilder GetGameState();
    event Action OnDeckBuilderEntered;
    event Action<GameState_DeckBuilder_ChangeData> OnClickedOnVillain;
    event Action<DeckPreview> OnClickedOnDeckPreview;
}