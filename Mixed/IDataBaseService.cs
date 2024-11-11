


using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

public interface IDataBaseService {

    void SelectAllCreatures ();
    event Action <HashSet<CreatureRuntimeCardData>> OnSelectAllCreatures;

}