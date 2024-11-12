


using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class CardUtility : MonoBehaviour {

    [SerializeField] private GameData gameData;

    public string GetDeckCode (IEnumerable<RuntimeCardData> deck) {

        StringBuilder sb = new();

        foreach (var card in deck) {
            sb.Append($"{card.Id}=3#");
        }

        return sb.ToString();
    }
}