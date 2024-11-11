
using System.Collections.Generic;

public interface ICardDeck {

    public string Name { get; }
    public List <RuntimeCardData> Cards { get; }
    public Villain Villain { get; }
}