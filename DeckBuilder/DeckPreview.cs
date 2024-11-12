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

    public Villain VillainData;
    public DeckMetaData MetaData { get; private set; }

    private string deckName;
    public string DeckName => deckName;


    public void Init (GameState_DeckBuilder state, string name, Villain villain, DeckMetaData metaData) {

        deckName = name;
        deckNameDisplay.text = this.deckName;
        VillainData = villain;

        this.gameState = state;
        this.MetaData = metaData;
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
