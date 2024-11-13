


using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeckSideBar : MonoBehaviour {

    public Image villainArtwork;
    public TMP_InputField deckName;
    [SerializeField] private Transform deckElementParent;
    [SerializeField] private GameObject deckElementPrefab;
    [SerializeField] private GameObject deckSideBar;
    private RuntimeCardDeck deck;
    public GameObject navigateBackArrow;
    [SerializeField] private TextMeshProUGUI heading;
    [SerializeField] private TextMeshProUGUI deckCounterDisplay;
    [SerializeField] private CanvasGroup canvasGroup;

    private Animator anim;
    private GameState_DeckBuilder state;
    private DeckBuilder deckBuilder;

    private void Awake() {

        anim = deckSideBar.GetComponent<Animator>();
        deckBuilder = GetComponent<DeckBuilder>();
    }

    private void Update() {
        // REPLACE WITH STATE CHANGE, INSTEAD OF EVERY FRAME
        if (this.deck == null) return;
        deckCounterDisplay.text = $"{this.deck.Cards.Count} / 30";
    }
    private void Start() {

        deckBuilder.OnClickedOnCollectionElement += Handler_OnClickedOnCollectionElement;
    }

    private void Handler_OnClickedOnCollectionElement(CollectionElement collectionElement) {

        if (collectionElement == null) return;

        foreach (Transform t in deckElementParent) {

            DeckElement t_Element = t.GetComponent<DeckElement>();
            if (t_Element.cardsInThisDeckElement[0].ID == collectionElement.cardData.ID) {
                t_Element.Add(collectionElement.cardData);
                OrderDeckElementsByCost();
                return;
            }
        }

        Instantiate(deckElementPrefab, deckElementParent).GetComponent<DeckElement>().Init(collectionElement.cardData);
        OrderDeckElementsByCost();
    }

    public void Show (RuntimeCardDeck deck, GameState_DeckBuilder state) {

        ClearSideBar();
        this.state = state;
        this.deck = deck;
        SetHeader("Cards");
        this.anim.Play("DeckBuilder_SideBar_FadeIn");
        villainArtwork.sprite = this.deck.Villain.artwork;
        deckName.text = this.deck.Name;
        this.navigateBackArrow.SetActive(true);

        ClearDeckElements();
        CreateDeckElements(deck);
    }


    private void CreateDeckElements (RuntimeCardDeck deck) {

        foreach (var card in deck.Cards) {

            bool foundExistingCopy = false;
            foreach (Transform t in deckElementParent.transform) {
                var deckElement = t.GetComponent<DeckElement>();
                if (deckElement.cardsInThisDeckElement[0].ID == card.ID) {
                    deckElement.Add(card);
                    foundExistingCopy = true;
                    break;
                }
            }

            if (foundExistingCopy) continue;
            Instantiate(deckElementPrefab, deckElementParent).GetComponent<DeckElement>().Init(card);
        }

        Debug.Log("Ha!");
        OrderDeckElementsByCost();
    }

    private void ClearDeckElements () {

        foreach (Transform t in deckElementParent) Destroy(t.gameObject);
    }
    public void GoBackToDeckView () {

        anim.Play("DeckBuilder_SideBar_FadeOut");
        this.navigateBackArrow.SetActive(false);
        var changeData = Utils.GetDeckBuilderChangeData();
        this.state.Invoke_GameStateChanged(GameState_DeckBuilder_ChangeReason.Input_ClickedOnBackToDeckPreviewArrow, changeData);
    }

    private void ClearSideBar() {

        villainArtwork.sprite = null;
        deckName.text = string.Empty;
        ClearDeckElements();
    }
    private void SetHeader (string text) {
        this.heading.text = text;
    }
    
    private void OrderDeckElementsByCost () {
        if (deckElementParent.childCount == 0 || deckElementParent.childCount == 1 ) return;

        List<DeckElement> elements = new();

        foreach (Transform t in deckElementParent) {

            DeckElement de = t.GetComponent<DeckElement>();

            if (de == null) {
                Debug.Log("DeckElement is null.");
                continue;
            }

            elements.Add(de);
        }

        elements = elements.OrderBy(x => x.cardsInThisDeckElement[0].Cost).ToList();
        for (int i = 0; i < elements.Count; i++) {
            elements[i].transform.SetSiblingIndex(i);
        }
    }

    public void Foobar() {
        OrderDeckElementsByCost();
        Debug.Log("Eyo!");
    }
}