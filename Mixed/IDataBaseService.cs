


using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

public interface IDataBaseService {

    void SelectAllCreatures ();
    void SelectAllDecks();
    void SelectAllKeywordsAssociations();

    event Action <Dictionary<int, HashSet<int>>> OnSelectAllKeywordAssociations;
    event Action <HashSet<CreatureRuntimeCardData>> OnSelectAllCreatures;
    event Action <HashSet<DatabaseDeckRecord>> OnSelectAllDecks;

}