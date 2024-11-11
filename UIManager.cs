


using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(FindCardManager))]
public class UIManager : MonoBehaviour, IUIManager {

    private IMediator mediator;
    private FindCardManager findCardManager;

    public TextMeshProUGUI player1Display;
    public TextMeshProUGUI player2Display;
    public TextMeshProUGUI turnPlayerDisplay;
    public TextMeshProUGUI turnCounterDisplay;

    public GameObject graveyardWindow;
    public GameObject findWindow;

    public Transform findWindowParent;

    public VillainUI villain1UI;
    public VillainUI villain2UI;


    private void Awake() {

        mediator = GetComponent<IMediator>();
        findCardManager = GetComponent<FindCardManager>();
    }

    private void Start() {

        mediator.OnGameStateChanged += Handle_GameStateChanged;
        mediator.OnStartOfTurn += Handle_OnStartOfTurn;
        mediator.OnEndOfTurn += Handle_OnEndOfTurn;
        mediator.OnFindEffectTriggered += Handle_FindEffectTriggered;
        mediator.OnCardInFindWindowSelected += Handle_OnCardInFindWindowSelected;
    }

    private void Handle_OnCardInFindWindowSelected(GameState state, RuntimeCardData data) {

        ToggleWindow(findWindow);
    }

    public void Handle_GameStateChanged (GameState state) {

        UpdateGameDataDisplay(state);
    }

    private void UpdateGameDataDisplay (GameState state) {

        if (state == null) {
            Debug.LogError("GameState is null.");
            return;
        }

        Player p1 = state.GetPlayerById(1);
        Player p2 = state.GetPlayerById(2);

        if (p1 == null || p2 == null) {
            Debug.LogError($"Player 1 or Player 2 is null. p1: {p1}, p2: {p2}");
            return;
        }

        player1Display.text = $"Player: {p1.ID} - Health: {p1.Health}" +
            $"\nMana: {p1.resources.currentMana}" +
            $"\nDeckSize: {p1.cards.DeckSize}" +
            $"\nHandSize: {p1.cards.HandSize}" +
            $"\nInPlaySize: {p1.cards.InPlaySize}" +
            $"\nGraveyardSize: {p1.cards.GraveyardSize}" +
            $"\nExiledSize: {p1.cards.ExiledSize}";

        player2Display.text = $"Player: {p2.ID} - Health: {p2.Health}" +
            $"\nMana: {p2.resources.currentMana}" +
             $"\nDeckSize: {p2.cards.DeckSize}" +
                $"\nHandSize: {p2.cards.HandSize}" +
                 $"\nInPlaySize: {p2.cards.InPlaySize}" +
                 $"\nGraveyardSize: {p2.cards.GraveyardSize}" +
                $"\nExiledSize: {p2.cards.ExiledSize}";
    }

    public void Handle_OnStartOfTurn (GameState state) {

        turnPlayerDisplay.text = $"Player {state.ActivePlayer.ID}";
        turnCounterDisplay.text = state.Turns.ToString();
        UpdateGameDataDisplay(state);
    }

    public void Handle_OnEndOfTurn (GameState state) {

        turnPlayerDisplay.text = $"Player {state.ActivePlayer.ID}";
        turnCounterDisplay.text = state.Turns.ToString();
        UpdateGameDataDisplay(state);
    }

    public void Handle_FindEffectTriggered (GameState state, CardPool pool) {

        Utils.Shuffle<RuntimeCardData>(pool.cards);

        foreach (Transform t in findWindowParent) {
            Destroy(t.gameObject);
        }

        for (int i = 0; i < 3; i++) 
            findCardManager.AddViewToParent(pool.cards[i], findWindowParent, state);

        ToggleWindow(findWindow);
    }

    public void ToggleWindow (GameObject window) {

        if (window.activeInHierarchy) window.SetActive(false);
        else window.SetActive(true);
    }

    public void OpenFindWindow () {

        findWindow.SetActive(true);
    }

    public void CreateVillains (GameState state) {

        Player p1 = state.GetPlayerById(1);
        Player p2 = state.GetPlayerById(2);

        villain1UI.Init(p1.Villain, state);
        villain2UI.Init(p2.Villain, state);
    }

    public void ChangeScene (int buildIndex) {
        SceneManager.LoadScene(buildIndex);
    }
}