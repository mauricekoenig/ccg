


using System.Collections.Generic;

[System.Serializable]

public class DatabaseDeckRecord {

    public int VillainID { get; }
    public string DeckCode { get; }
    public string DeckName { get; }

    public DatabaseDeckRecord (int villainID, string deckCode, string deckName) {

        this.VillainID = villainID;
        this.DeckCode = deckCode;
        this.DeckName = deckName;
    }
}