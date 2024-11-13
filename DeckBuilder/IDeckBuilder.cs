


using System.Collections.Generic;
using System;

public interface IDeckBuilder {

    GameState_DeckBuilder GetGameState();
    event Action<GameState_DeckBuilder> OnDeckBuilderEntered;
    event Action<GameState_DeckBuilder_ChangeData> OnClickedOnVillain;
    event Action<DeckPreview> OnClickedOnDeckPreview;
    event Action OnClickedOnBackToDeckPreviewArrow;
    event Action<CollectionElement> OnClickedOnCollectionElement;
}