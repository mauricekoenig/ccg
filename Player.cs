


using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerCards))]
[RequireComponent (typeof(PlayerResources))]

public class Player : MonoBehaviour {

    private int id;
    private int health;
    private Villain villain;

    public Villain Villain => villain;

    public int Health => health;
    public int ID => id;

    public int CardsOnBoard => cards.OnBoard;
    public int CurrentMana => resources.currentMana;

    public PlayerCards cards;
    public  PlayerResources resources;

    private void Start() {

        cards = GetComponent<PlayerCards>();
        resources = GetComponent<PlayerResources>();
    }

    public void Init (int id, List<CardRuntimeData> deckList, Villain villain) {

        this.id = id;
        this.health = 20;
        this.villain = villain; 
        cards.Init(deckList);
        resources.Init(this);
    }

    public void AddMana (int amount) {
        this.resources.Add(amount);
    }
    public void RemoveMana (int amount) {
        this.resources.Remove(amount);
    }

    public void AddCardToBoard (CardRuntimeData data) {

        cards.AddCardToZone(data, CardZone.Play);
    }

    public CardRuntimeData DrawCard () {

        if (cards.Deck.Count <= 0) return null;
        return cards.DrawCardFromDeck();
    }
}