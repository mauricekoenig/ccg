


using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(DeckSideBar))]
[RequireComponent(typeof(CollectionViewport))]

public class DeckBuilderUI : MonoBehaviour {

    private IDeckBuilder deckBuilder;
    private DeckSideBar deckSideBar;
    private CollectionViewport collectionViewport;


    [SerializeField] private GameData gameData;
    private GameState_DeckBuilder gameState;

    private void Awake() {

        deckBuilder = GetComponent<DeckBuilder>();
        deckSideBar = GetComponent<DeckSideBar>();
        collectionViewport = GetComponent<CollectionViewport>();
    }
    private void Start() {

        deckBuilder.OnClickedOnVillain += Handler_OnClickedOnVillain;
        deckBuilder.OnClickedOnDeckPreview += Handler_OnClickedOnDeckPreview;
        deckBuilder.OnDeckBuilderEntered += Handler_OnDeckBuilderEntered;
    }

    private void Handler_OnDeckBuilderEntered (GameState_DeckBuilder state) {

        this.gameState = state;
        this.collectionViewport.ShowDecks(state);
    }
    private void Handler_OnClickedOnVillain(GameState_DeckBuilder_ChangeData data) {


    }
    private void Handler_OnClickedOnDeckPreview (DeckPreview deckPreview) {

        collectionViewport.ClearCollectionViewPorts();
        deckSideBar.Show(deckPreview, this.gameState);
        collectionViewport.ShowCards();
    }

    public void ChangeScene (int buildIndex) {
        SceneManager.LoadScene(buildIndex);
    }

}
