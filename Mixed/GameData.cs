
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu]
public class GameData : ScriptableObject {

    public Villain TestVillain;
    public RuntimeGameData runtimeGameData;

    [SerializeField] private List<Keyword> AllKeywords = new();

    public void Init () {

        if (RuntimeGameData.Initialized) return;
        runtimeGameData = new RuntimeGameData(this);
        RuntimeGameData.Initialized = true;
    }

    public Keyword GetKeyword (int id) {
        return AllKeywords.Where(x => x.ID == id).FirstOrDefault();
    }
    public Keyword GetKeyword (string keywordName) {
        return AllKeywords.Where(x => x.Type.ToString().Equals(keywordName)).FirstOrDefault();
    }
    public Keyword GetKeyword (KeywordType type) {
        return AllKeywords.Where(x => x.Type == type).FirstOrDefault();
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

    public void AssignKeywords(Dictionary<int, HashSet<int>> dictionary) {
        foreach (var record in dictionary) {
            var creature = this.runtimeGameData.GetCreatureById(record.Key);
            foreach (var keyword_id in record.Value) {
                var keyword = GetKeyword(keyword_id);
                keyword.Apply(creature);
            }
        }
    }
}