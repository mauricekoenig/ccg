


using System;
using System.Collections.Generic;
using System.Data;

public interface IDataBaseService {

    void SelectAllCreatures ();
    event Action <HashSet<CreatureData>> OnSelectAllCreatures;

}