


using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

public interface IDataBaseService {

    void SelectAllCreatures ();
    void SelectAllDecks();

    event Action <HashSet<CreatureRuntimeCardData>> OnSelectAllCreatures;
    event Action<HashSet<DatabaseDeckRecord>> OnSelectAllDecks;

}