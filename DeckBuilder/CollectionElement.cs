using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CollectionElement : MonoBehaviour, IPointerDownHandler
{
    public Image Artwork;
    [SerializeField] private TextMeshProUGUI cost;
    [SerializeField] private TextMeshProUGUI attack;
    [SerializeField] private TextMeshProUGUI health;
    [SerializeField] private TextMeshProUGUI cardName;
    [SerializeField] private TextMeshProUGUI cardText;
    [SerializeField] private GameObject grayOut;

    private GameState_DeckBuilder state;
    private bool grayedOut;

    public RuntimeCardData cardData;

    public void Init (RuntimeCardData data, GameState_DeckBuilder state) {

        this.cardData = data;
        this.state = state;

        if (data is CreatureRuntimeCardData) {

            var creature = (CreatureRuntimeCardData)data;
            this.cardName.text = creature.Name;
            Artwork.sprite = creature.Artwork;
            this.cost.text = creature.Cost.ToString();
            this.attack.text = creature.Attack.ToString();
            this.health.text = creature.Health.ToString();
            this.cardText.text = creature.GetKeywordString();
        }

        else {

            this.cardName.text = data.Name;
            Artwork.sprite = data.Artwork;
            this.cost.text = data.Cost.ToString();
            this.attack.text = data.Cost.ToString();
            this.health.text = data.Cost.ToString();
        }
    }

    public void OnPointerDown(PointerEventData eventData) {

        if (grayedOut) return;
        var changeData = Utils.GetDeckBuilderChangeData();
        changeData.collectionElement = this;
        this.state.Invoke_GameStateChanged(GameState_DeckBuilder_ChangeReason.Input_ClickedOnCollectionElement, changeData);
    }

    public void GrayOut () {
        this.grayOut.SetActive(true);
        this.grayedOut = true;
    }
}
