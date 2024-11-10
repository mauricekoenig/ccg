



using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameData : ScriptableObject {

    private HashSet<CreatureData> allCreatures;

    public void AssignCreatures (HashSet<CreatureData> creatures) {

        allCreatures = creatures;
    }
}