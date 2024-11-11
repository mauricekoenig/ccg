
using UnityEngine;
using System.Data.SQLite;
using System.Data;
using TMPro;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

public class TestDatabaseService : MonoBehaviour, IDataBaseService {

    [SerializeField] private string connectionString;

    public event Action<HashSet<CreatureRuntimeCardData>> OnSelectAllCreatures;

    void Start () {

        string dbPath = Application.streamingAssetsPath + "/tcg.db";
        connectionString = "Data Source=" + dbPath;
    }

    public async Task SelectAllCreatures() {

        try {

            HashSet<CreatureRuntimeCardData> creatureList = await Task.Run(() => {

                using (var dbConnection = new SQLiteConnection(connectionString)) {

                    dbConnection.Open();
                    var query = $"SELECT * FROM Cards WHERE CardType = 'Creature'";

                    using (IDbCommand dbCommand = dbConnection.CreateCommand()) {

                        dbCommand.CommandText = query;

                        using (IDataReader reader = dbCommand.ExecuteReader()) {

                            var creatures = new HashSet<CreatureRuntimeCardData>();

                            while (reader.Read()) {

                                int id = reader.GetInt32(reader.GetOrdinal("CardID"));
                                string name = reader.GetString(reader.GetOrdinal("Name"));
                                int cost = reader.GetInt32(reader.GetOrdinal("Cost"));
                                string base64 = reader.GetString(reader.GetOrdinal("Artwork"));
                                int attack = reader.GetInt32(reader.GetOrdinal("Attack"));
                                int health = reader.GetInt32(reader.GetOrdinal("HP"));

                                var creatureData = new CreatureRuntimeCardData(id, name, cost, base64, attack, health);
                                creatures.Add(creatureData);
                            }

                            return creatures; 
                        }
                    }
                }
            });

            OnSelectAllCreatures?.Invoke(creatureList);
        } 
        
        catch (Exception ex) {

            Debug.LogError($"Error while fetching creatures: {ex.Message}");
        }
    }

}