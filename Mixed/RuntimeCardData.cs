



using UnityEngine;
using System.Collections.Generic;
using System;

public abstract class RuntimeCardData : IIdentifiable {

    public int ID { get;}
    public string Name { get; set; }
    public int Cost { get; set; }
    public Sprite Artwork { get; set; }
    public CardRarity Rarity { get; set; }
    public string ArtworkBase64 { get; set; }
    public string Color { get; set; }

    public HashSet<BaseEffect> Effects { get; set; }

    public bool HasEffect() {
        return Effects.Count > 0;
    }

    public RuntimeCardData (int id, string name, int cost, string artworkBase64, string color) {

        ID = id;
        Name = name;
        Cost = cost;
        ArtworkBase64 = artworkBase64;
        Effects = new HashSet<BaseEffect>();
        Color = color;
    }

    public RuntimeCardData(int id, string name, int cost, Sprite sprite, string color) {

        ID = id;
        Name = name;
        Cost = cost;
        Artwork = sprite;
        Effects = new HashSet<BaseEffect>();
        Color = color;
    }

    public RuntimeCardData (int id, string name, int cost, Sprite artwork, CardRarity rarity, HashSet<BaseEffect> effects, string color) {

        ID = id;
        Name = name;
        Cost = cost;
        Artwork = artwork;
        Rarity = rarity;
        Effects = effects;
        Color = color;
    }

    public void LoadSprite () {
        
        Artwork = Utils.CreatureSpriteFromBase64 (ArtworkBase64);
    }

    public override string ToString() {
        return this.Name;
    }

    public abstract RuntimeCardData Clone();
}