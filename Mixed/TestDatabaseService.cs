
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
    public event Action<HashSet<DeckMetaData>> OnSelectAllDecks;

    void Awake () {

        string dbPath = Application.streamingAssetsPath + "/tcg.db";
        connectionString = $"Data Source={dbPath}";
    }

    public void SelectAllDecks () {

        try {

            HashSet<DeckMetaData> set = new();

            using (var dbConnection = new SQLiteConnection(connectionString)) {

                dbConnection.Open();
                var query = $"SELECT * FROM Decks";

                using (IDbCommand dbCommand = dbConnection.CreateCommand()) {

                    dbCommand.CommandText = query;

                    using (IDataReader reader = dbCommand.ExecuteReader()) {

                        while (reader.Read()) {

                            int villainId = reader.GetInt32(reader.GetOrdinal("villain_id"));
                            string deckCode = reader.GetString(reader.GetOrdinal("code"));
                            DeckMetaData deckMetaData = new DeckMetaData(villainId, deckCode);

                            set.Add(deckMetaData);
                        }

                        OnSelectAllDecks?.Invoke(set);
                    }
                }
            }
        } catch (Exception ex) {

            Debug.LogError($"Error while fetching creatures: {ex.Message}");
        }

    }

    public void SelectAllCreatures() {

        try {

            HashSet<CreatureRuntimeCardData> set = new();

            using (var dbConnection = new SQLiteConnection(connectionString)) {

                dbConnection.Open();
                var query = $"SELECT * FROM Cards WHERE CardType = 'Creature'";

                using (IDbCommand dbCommand = dbConnection.CreateCommand()) {

                    dbCommand.CommandText = query;

                    using (IDataReader reader = dbCommand.ExecuteReader()) {

                        while (reader.Read()) {

                            int id = reader.GetInt32(reader.GetOrdinal("CardID"));
                            string name = reader.GetString(reader.GetOrdinal("Name"));
                            int cost = reader.GetInt32(reader.GetOrdinal("Cost"));
                            string base64 = reader.GetString(reader.GetOrdinal("Artwork"));
                            int attack = reader.GetInt32(reader.GetOrdinal("Attack"));
                            int health = reader.GetInt32(reader.GetOrdinal("HP"));

                            var creatureData = new CreatureRuntimeCardData(id, name, cost, base64, attack, health);
                            set.Add(creatureData);
                        }

                        OnSelectAllCreatures?.Invoke(set);
                    }
                }
            }
        } 
        
        catch (Exception ex) {

            Debug.LogError($"Error while fetching creatures: {ex.Message}");
        }
    }

}