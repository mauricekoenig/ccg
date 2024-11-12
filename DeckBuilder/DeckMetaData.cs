


using System.Collections.Generic;

[System.Serializable]

public class DeckMetaData {

    public int VillainID { get; }
    public string DeckCode { get; }

    public DeckMetaData (int villainID, string deckCode) {

        this.VillainID = villainID;
        this.DeckCode = deckCode;
    }
}