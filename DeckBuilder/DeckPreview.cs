using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DeckPreview : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameState_DeckBuilder gameState;
    [SerializeField] private Image deckBoxArtwork;
    [SerializeField] private TextMeshProUGUI deckNameDisplay;
    [SerializeField] private Image villainPreview;

    public RuntimeCardDeck Deck { get; set; }

    private string deckName;
    public string DeckName => deckName;

    public void Init (RuntimeCardDeck deck, GameState_DeckBuilder state) {

        this.gameState = state;

        deckName = deck.Name;
        deckNameDisplay.text = deck.Name;

        this.Deck = deck;
        this.villainPreview.sprite = deck.Villain.artwork;
    }

    public void OnPointerDown (PointerEventData eventData) {

        var changeData = Utils.GetDeckBuilderChangeData();
        changeData.deckPreview = this;
        this.gameState.Invoke_GameStateChanged(GameState_DeckBuilder_ChangeReason.Input_ClickedOnDeckPreview, changeData);
    }

    public void OnPointerEnter(PointerEventData eventData) {
        
    }

    public void OnPointerExit(PointerEventData eventData) {
        
    }
}
