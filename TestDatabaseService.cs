
using UnityEngine;
using System.Data.SQLite;
using System.Data;
using TMPro;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

public class TestDatabaseService : MonoBehaviour, IDataBaseService {

    public TextMeshProUGUI display;
    [SerializeField] private string connectionString;

    public event Action<HashSet<CreatureData>> OnSelectAllCreatures;

    void Start () {

        string dbPath = Application.streamingAssetsPath + "/tcg.db";
        connectionString = "Data Source=" + dbPath;
    }

    public async void SelectAllCreatures() {

        try {

            HashSet<CreatureData> creatureList = await Task.Run(() => {

                using (var dbConnection = new SQLiteConnection(connectionString)) {

                    dbConnection.Open();
                    var query = $"SELECT * FROM Cards WHERE CardType = 'Creature'";

                    using (IDbCommand dbCommand = dbConnection.CreateCommand()) {

                        dbCommand.CommandText = query;

                        using (IDataReader reader = dbCommand.ExecuteReader()) {

                            var creatures = new HashSet<CreatureData>();

                            while (reader.Read()) {

                                int id = reader.GetInt32(reader.GetOrdinal("CardID"));
                                string name = reader.GetString(reader.GetOrdinal("Name"));
                                int cost = reader.GetInt32(reader.GetOrdinal("Cost"));
                                string base64 = reader.GetString(reader.GetOrdinal("Artwork"));
                                int attack = reader.GetInt32(reader.GetOrdinal("Attack"));
                                int health = reader.GetInt32(reader.GetOrdinal("HP"));

                                var creatureData = new CreatureData(id, name, cost, base64, attack, health);
                                creatures.Add(creatureData);
                            }

                            return creatures; 
                        }
                    }
                }
            });

            foreach (var creature in creatureList) creature.LoadSprite();
            OnSelectAllCreatures?.Invoke(creatureList);
        } 
        
        catch (Exception ex) {

            Debug.LogError($"Error while fetching creatures: {ex.Message}");
        }
    }

}