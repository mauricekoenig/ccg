


using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class DeckBuilderUI : MonoBehaviour {

    IDeckBuilder deckBuilder;

    [SerializeField] private TMP_InputField deck_Name;
    [SerializeField] private Image deck_VillainArtwork;

    [SerializeField] private Transform cardView_Collection_Parent;
    [SerializeField] private GameObject cardView_Collection_Prefab;
    [SerializeField] private GameObject cardViewVillain_Collection_Prefab;

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
    }

    private void Handler_OnVillainDataReceived (HashSet<Villain> villains, GameState_DeckBuilder state) {

        Debug.Log(villains.Count);

        foreach (var card in villains) {
            var villainView = Instantiate (cardViewVillain_Collection_Prefab, cardView_Collection_Parent);
            villainView.GetComponent<CardViewVillain_Collection>().Init(card, state);
        }
    }
}