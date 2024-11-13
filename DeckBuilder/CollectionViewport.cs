


using System.Linq;
using TMPro;
using UnityEngine;

public class CollectionViewport : MonoBehaviour {


    private GameState_DeckBuilder gameState;
    [SerializeField] private GameData gameData;

    [SerializeField] private Transform viewport_Cards;
    [SerializeField] private Transform viewport_Decks;

    [SerializeField] private GameObject collectionElementPrefab;
    [SerializeField] private GameObject collectionElementVillainPrefab;
    [SerializeField] private GameObject deckPreviewPrefab;
    [SerializeField] private GameObject addNewDeckPrefab;

    [SerializeField] private GameObject villainIcon;
    [SerializeField] private GameObject collectionIcon;
    [SerializeField] private TextMeshProUGUI heading;

    [SerializeField] private Animator anim;

    public void AddNewDeck () {

        ClearCollectionViewPorts();
        // show villains.

    }

    public void ShowVillains () {

    }
    public void ShowDecks (GameState_DeckBuilder state) {

        this.gameState = state;
        SetHeader("Decks");
        ClearCollectionViewPorts();

        var deckRecords = gameData.GetAllDatabaseDeckRecords();

        foreach (var record in deckRecords) {

            var runtimeDeck = Utils.GetRuntimeDeck(record, gameData);
            var deckPreview = Instantiate(deckPreviewPrefab, viewport_Decks).GetComponent<DeckPreview>();
            deckPreview.Init(runtimeDeck, this.gameState);
        }

        if (deckRecords.Count < 10) {
            var prefab = Instantiate(addNewDeckPrefab, viewport_Decks);
            prefab.GetComponent<AddNewDeck>().Init(this);
        }
    }
    public void ShowCards () {

        this.anim.Play("DeckBuilder_CollectionViewPort_FadeIn");

        var ordered = gameData.GetAllCards().OrderBy(card => card.Cost.ToString());

        foreach (var card in ordered) {
            Instantiate(collectionElementPrefab, viewport_Cards).GetComponent<CollectionElement>().Init(card, gameState);
        }
    }
    public void ClearCollectionViewPorts () {

        foreach (Transform t in viewport_Decks) Destroy(t.gameObject);
        foreach (Transform t in viewport_Cards) Destroy(t.gameObject);
    }
    private void SetHeader (string text) {
        heading.text = text;
    }
}