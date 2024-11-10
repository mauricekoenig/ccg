



using UnityEngine;
using System.Collections.Generic;

public abstract class BaseCardData {

    public int Id { get; set; }
    public string Name { get; set; }
    public int Cost { get; set; }
    public Sprite Artwork { get; set; }
    public CardRarity Rarity { get; set; }
    public string ArtworkBase64 { get; set; }

    public HashSet<BaseEffect> Effects { get; set; }

    public bool HasEffect() {
        return Effects.Count > 0;
    }

    public BaseCardData (int id, string name, int cost, string artworkBase64) {

        Id = id;
        Name = name;
        Cost = cost;
        ArtworkBase64 = artworkBase64;
    }

    public BaseCardData (int id, string name, int cost, Sprite artwork, CardRarity rarity, HashSet<BaseEffect> effects) {

        Id = id;
        Name = name;
        Cost = cost;
        Artwork = artwork;
        Rarity = rarity;
        Effects = effects;
    }

    public void LoadSprite () {
        
        Artwork = Utils.CreatureSpriteFromBase64 (ArtworkBase64);
    }
}