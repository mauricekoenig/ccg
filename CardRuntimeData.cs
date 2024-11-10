


using System.Collections.Generic;
using UnityEngine;

public abstract class CardRuntimeData : ScriptableObject, IIdentifiable {

    [Space]
    [Header("Shared Data")]
    [Space]

    public int cardID;
    public string cardName;
    public int cardCost;
    public Sprite artwork;
    public CardRarity cardRarity;

    public List<BaseEffect> effects = new();

    public int ID => cardID;

    public bool HasEffect () {
        return effects.Count > 0;
    }
}
