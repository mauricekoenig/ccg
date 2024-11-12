


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

    [SerializeField] private GameObject villainIcon;
    [SerializeField] private GameObject collectionIcon;
    [SerializeField] private TextMeshProUGUI heading;

    public void ShowDecks (GameState_DeckBuilder state) {

        this.gameState = state;
        ClearCollectionViewPorts();

        var deckRecords = gameData.GetAllDatabaseDeckRecords();;

        foreach (var record in deckRecords) {

            var runtimeDeck = Utils.GetRuntimeDeck(record, gameData);
            var deckPreview = Instantiate(deckPreviewPrefab, viewport_Decks).GetComponent<DeckPreview>();
            deckPreview.Init(runtimeDeck, this.gameState);
        }
    }

    public void ClearCollectionViewPorts () {

        foreach (Transform t in viewport_Decks) Destroy(t.gameObject);
        foreach (Transform t in viewport_Cards) Destroy(t.gameObject);
    }
}