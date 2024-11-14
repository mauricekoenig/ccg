using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CollectionElement : MonoBehaviour, IPointerDownHandler
{

    [SerializeField] private TextMeshProUGUI cost;
    [SerializeField] private TextMeshProUGUI cardName;
    [SerializeField] private TextMeshProUGUI cardText;
    [SerializeField] private TextMeshProUGUI typePlate;
    [SerializeField] private GameObject grayOut;

    // Creature specific
    [SerializeField] private GameObject creatureArtwork_GameObject;
    [SerializeField] private Image creatureArtwork;
    [SerializeField] private TextMeshProUGUI attack;
    [SerializeField] private TextMeshProUGUI health;

    [SerializeField] private GameObject spellArtwork_GameObject;
    [SerializeField] private Image spellArtwork;



    private GameState_DeckBuilder state;
    private bool grayedOut;

    public RuntimeCardData cardData;

    public void Init (RuntimeCardData data, GameState_DeckBuilder state) {

        this.cardData = data;
        this.state = state;

        this.cardName.text = data.Name;
        this.cost.text = data.Cost.ToString();

        if (data is CreatureRuntimeCardData) {

            spellArtwork_GameObject.SetActive(false);
            creatureArtwork_GameObject.SetActive(true);
            var creature = (CreatureRuntimeCardData)data;
            creatureArtwork.sprite = creature.Artwork;
            this.attack.text = creature.Attack.ToString();
            this.health.text = creature.Health.ToString();
            this.cardText.text = creature.GetKeywordString();
            typePlate.text = "Creature"; // Need adjustments
        }

        else {

            creatureArtwork_GameObject.SetActive(true);
            spellArtwork_GameObject.SetActive(true);
            var spell = (SpellRuntimeCardData)data;
            spellArtwork.sprite = data.Artwork;
            this.attack.text = data.Cost.ToString();
            this.health.text = data.Cost.ToString();
            this.cardText.text = "My Spell Text";
            this.typePlate.text = "Spell";
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
