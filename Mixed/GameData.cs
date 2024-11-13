
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu]
public class GameData : ScriptableObject {

    public Villain TestVillain;
    public RuntimeGameData runtimeGameData;

    public void Init () {

        if (RuntimeGameData.Initialized) return;
        runtimeGameData = new RuntimeGameData(this);
        RuntimeGameData.Initialized = true;
    }

    public RuntimeCardData GetCardByName (string name) {
        return this.runtimeGameData.GetCardByName(name);
    }

    public RuntimeCardData GetCardById (int id) {
        return this.runtimeGameData.GetCardById(id);
    }

    public HashSet<RuntimeCardData> GetAllCards() {
        return this.runtimeGameData.GetAllCards();
    }

    public HashSet<DatabaseDeckRecord> GetAllDatabaseDeckRecords () {
        return this.runtimeGameData.GetAllDeckMetaData();
    }

    public Villain GetVillainById(int id) {
        return this.runtimeGameData.GetVillainById(id);
    }
}