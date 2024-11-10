


using UnityEngine;
using System.Collections.Generic;
using System;

public class PlayerCards : MonoBehaviour {

    [SerializeField] private List<CardRuntimeData> deck = new();
    [SerializeField] private List<CardRuntimeData> hand = new();
    [SerializeField] private List<CardRuntimeData> inPlay = new();
    [SerializeField] private List<CardRuntimeData> graveyard = new();
    [SerializeField] private List<CardRuntimeData> exiled = new();

    public int DeckSize => deck.Count;
    public int HandSize => hand.Count;
    public int InPlaySize => inPlay.Count;
    public int GraveyardSize => graveyard.Count;
    public int ExiledSize => exiled.Count;

    public List<CardRuntimeData> Deck => deck;

    public int OnBoard => inPlay.Count;

    public void Init (List<CardRuntimeData> deckList) {

        ClearAllZones();
        this.deck = deckList;
    }

    public void AddCardToZone (CardRuntimeData card, CardZone zone) {

        switch (zone) {

            case CardZone.None:
                break;
            case CardZone.Hand:
                hand.Add(card);
                break;
            case CardZone.Play:
                inPlay.Add(card);
                break;
            case CardZone.Graveyard:
                graveyard.Add(card);
                break;
            case CardZone.Exiled:
                exiled.Add(card);
                break;
        }

    }
    public void RemoveCardFromZone (CardRuntimeData card, CardZone zone) {

        switch (zone) {

            case CardZone.None:
                break;
            case CardZone.Hand:
                hand.Remove(card);
                break;
            case CardZone.Play:
                inPlay.Remove(card);
                break;
            case CardZone.Graveyard:
                graveyard.Remove(card);
                break;
            case CardZone.Exiled:
                exiled.Remove(card);
                break;
        }
    }
    public void MoveCardBetweenZones (CardRuntimeData card, CardZone fromZone, CardZone toZone) {

        RemoveCardFromZone(card, fromZone);
        AddCardToZone(card, toZone);
    }

    public CardRuntimeData DrawCardFromDeck () {

        if (deck.Count <= 0) return null;
        CardRuntimeData card = deck[deck.Count - 1];
        deck.RemoveAt(deck.Count - 1);
        hand.Add(card);
        return card;
    }

    public CardRuntimeData GetRandomCardInHand () {
        if (hand.Count <= 0) return null;
        return hand[UnityEngine.Random.Range(0, hand.Count - 1)];
    }

    private void ClearAllZones () {

        deck.Clear();
        hand.Clear();
        inPlay.Clear();
        graveyard.Clear();
        exiled.Clear();
    }
}