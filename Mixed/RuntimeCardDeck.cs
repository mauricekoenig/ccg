

using System.Collections.Generic;


public class RuntimeCardDeck : ICardDeck {

    private string deckName;
    private List<RuntimeCardData> cards;
    private Villain villain;

    public string Name => deckName;
    public List<RuntimeCardData> Cards => cards;
    public Villain Villain => villain;

    public RuntimeCardDeck (string deckName, List<RuntimeCardData> cards, Villain villain) {

        this.deckName = deckName;
        this.cards = cards;
        this.villain = villain;
    }

    public void AddCard (RuntimeCardData card) {
        this.cards.Add (card);
    }

    public void RemoveCard (RuntimeCardData card) {
        this.cards.Remove(card);
    }
}