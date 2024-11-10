



using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CardDeck : ScriptableObject {

    public string deckName;
    public List<CardRuntimeData> cards = new();
    public Villain villain;
}