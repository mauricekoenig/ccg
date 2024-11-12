


using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using UnityEngine;
using UnityEngine.Rendering;

public static class Utils {

    public static Sprite CreatureSpriteFromBase64 (string base64String) {

        byte[] imageBytes = Convert.FromBase64String(base64String);
        Texture2D texture = new Texture2D (1, 1);
        texture.LoadImage(imageBytes);
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(.5f, .5f));
    }
    public static void Shuffle<T>(List<T> list) {

        // Fisher-Yates shuffle algorithm
        System.Random rng = new System.Random();
        int n = list.Count;
        while (n > 1) {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
    public static GameState_DeckBuilder_ChangeData GetDeckBuilderChangeData() {
        return new GameState_DeckBuilder_ChangeData();
    }
    
    public static RuntimeCardDeck GetRuntimeDeck (DatabaseDeckRecord deckRecord, GameData gameData) {

        List<RuntimeCardData> cards = new();
        Villain villain = gameData.GetVillainById(deckRecord.VillainID);

        var deckCode = deckRecord.DeckCode.Trim();
        var segments = deckCode.Split('#', StringSplitOptions.RemoveEmptyEntries);

        foreach (var segment in segments) {
            segment.Trim();
            if (segment.Length < 3) continue;

            bool canParseId = int.TryParse(segment[0].ToString(), out int id);
            bool canParseQuantity = int.TryParse(segment[2].ToString(), out int quantity);

            if (!canParseId || !canParseQuantity) {
                Debug.LogWarning("Invalid deck code format.");
                continue;
            }

            for (int i = 0; i < quantity; i++)
                cards.Add(gameData.GetCardById(id));
        }

        return new RuntimeCardDeck(deckRecord.DeckName, cards, villain);
    } 

}