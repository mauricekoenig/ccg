using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;

public class Foo : MonoBehaviour
{

    public string databaseName;
    public string fullPath;
    private string connectionString;

    [Header("Read")]
    public int cardID;
    public SpriteRenderer display;
    public TextMeshPro effectText;
    public TextMeshPro cost;
    public TextMeshPro atk;
    public TextMeshPro hp;
    public TextMeshPro cardName;
    public TextMeshPro tribe;
    
    private void Start() {

        fullPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), databaseName);
        connectionString = $"Data Source={fullPath}";
        ReadData();
    }

    byte[] GetBinaryPNG (Texture2D texture) {
        return texture.EncodeToPNG();
    }

    private void ReadData () {

        string selectQuery = $"SELECT * FROM cards WHERE CardID = {cardID}";

        try {

            using (var connection = new SQLiteConnection(connectionString)) {

                connection.Open();

                using (var command = new SQLiteCommand(selectQuery, connection)) {

                    using (var reader = command.ExecuteReader()) {

                        while (reader.Read()) {

                            string nameX = (string)reader["Name"];
                            string base64String = (string)reader["Artwork"];
                            string costX = reader["Cost"].ToString();
                            string atkX =  reader["Attack"].ToString();
                            string hpX =   reader["HP"].ToString();

                            string tribeX = atkX == "-1" ? "Spell" : "Creature";

                            byte[] imageBytes = Convert.FromBase64String (base64String);
                            Texture2D texture = new Texture2D (512, 512);

                            if (texture.LoadImage(imageBytes)) {

                                display.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(.5f, .5f));

                                cardName.text = nameX;
                                cost.text = costX;
                                tribe.text = tribeX;

                                if (atkX != "-1") atk.text = atkX;
                                else atk.text = string.Empty;

                                if (hpX != "-1")  hp.text = hpX;
                                else hp.text = string.Empty;

                            } 
                            
                            else Debug.Log("Failed to load Image!");
                        }
                    }
                }
            }
        } 
        
        catch (Exception e) {

            Debug.Log(e.ToString());
        }
    }


    public void Back () {

        if (cardID > 1) cardID--;
        ReadData();
    }

    public void Next () {
        cardID++;
        ReadData();
    }

}
