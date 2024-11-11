


using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class DeckBuilderUI : MonoBehaviour {

    IDeckBuilder deckBuilder;

    [SerializeField] private GameData gameData;

    [SerializeField] private TMP_InputField deck_Name;
    [SerializeField] private Image deck_VillainArtwork;

    [SerializeField] private Transform collectionElementParent;
    [SerializeField] private Transform deckElementParent;

    [SerializeField] private GameObject collectionElementPrefab;
    [SerializeField] private GameObject collectionElementVillainPrefab;
    [SerializeField] private GameObject deckElementPrefab;

    [SerializeField] private GameObject villainIcon;
    [SerializeField] private GameObject collectionIcon;

    [SerializeField] private TextMeshProUGUI heading;

    private void Awake() {
        deckBuilder = GetComponent<DeckBuilder>();
    }
    private void Start() {
        deckBuilder.OnVillainDataReceived += Handler_OnVillainDataReceived;
        deckBuilder.OnClickedOnVillain += Handler_OnClickedOnVillain;
    }

    private void Handler_OnClickedOnVillain(GameState_DeckBuilder_ChangeData data) {

        deck_VillainArtwork.sprite = data.villain.artwork;
        deck_Name.text = $"New {data.villain.Name} Deck";

        ClearCollectionParent();

        heading.text = "Collection";
        villainIcon.SetActive(false);
        collectionIcon.SetActive(true);

        var cards = gameData.GetAllCards();

        foreach (var card in cards) {
            GameObject collectionElement = Instantiate(collectionElementPrefab, collectionElementParent);
            collectionElement.GetComponent<CollectionElement>().Init(card);
        }
    }

    private void Handler_OnVillainDataReceived (HashSet<Villain> villains, GameState_DeckBuilder state) {

        Debug.Log(villains.Count);

        foreach (var card in villains) {
            var villainView = Instantiate (collectionElementVillainPrefab, collectionElementParent);
            villainView.GetComponent<CardViewVillain_Collection>().Init(card, state);
        }
    }

    private void ClearCollectionParent () {

        foreach (Transform t in collectionElementParent) {
            Destroy(t.gameObject);
        }
    }

    public void ChangeScene (int buildIndex) {
        SceneManager.LoadScene(buildIndex);
    }
}