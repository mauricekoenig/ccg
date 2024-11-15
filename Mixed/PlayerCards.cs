


using UnityEngine;
using System.Collections.Generic;
using System;

public class PlayerCards : MonoBehaviour, IIdentifiable {

    private int id;
    private ICardDeck cardDeck;

    [SerializeField] private List<RuntimeCardData> deck = new();
    [SerializeField] private List<RuntimeCardData> hand = new();
    [SerializeField] private List<RuntimeCardData> inPlay = new();
    [SerializeField] private List<RuntimeCardData> graveyard = new();
    [SerializeField] private List<RuntimeCardData> exiled = new();

    public int DeckSize => deck.Count;
    public int HandSize => hand.Count;
    public int InPlaySize => inPlay.Count;
    public int GraveyardSize => graveyard.Count;
    public int ExiledSize => exiled.Count;

    public List<RuntimeCardData> Deck => deck;

    public int OnBoard => inPlay.Count;

    public int ID => this.id;

    public void Init (ICardDeck deck) {

        ClearAllZones();
        this.cardDeck = deck;
        this.deck.AddRange(deck.Cards);
    }

    public void AddCardToZone (RuntimeCardData card, CardZone zone) {

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
    public void RemoveCardFromZone (RuntimeCardData card, CardZone zone) {

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
    public void MoveCardBetweenZones (RuntimeCardData card, CardZone fromZone, CardZone toZone) {

        RemoveCardFromZone(card, fromZone);
        AddCardToZone(card, toZone);
    }

    public RuntimeCardData DrawCardFromDeck () {

        if (deck.Count <= 0) return null;
        RuntimeCardData card = deck[deck.Count - 1];
        deck.RemoveAt(deck.Count - 1);
        hand.Add(card);
        return card;
    }

    public RuntimeCardData GetRandomCardInHand () {
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