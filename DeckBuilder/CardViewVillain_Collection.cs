using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardViewVillain_Collection : MonoBehaviour, IPointerDownHandler
{
    public Image Artwork;
    private Villain villainData;
    private GameState_DeckBuilder state;

    public void Init (Villain villain, GameState_DeckBuilder state) {

        this.villainData = villain;
        Artwork.sprite = this.villainData.artwork;
        this.state = state;
    }

    public void OnPointerDown(PointerEventData eventData) {

        var data = Utils.GetDeckBuilderChangeData();
        data.villain = this.villainData;
        this.state.Invoke_GameStateChanged(GameState_DeckBuilder_ChangeReason.Input_ClickedOnVillain, data);
    }
}
