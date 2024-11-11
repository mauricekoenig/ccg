


using UnityEngine;

public interface ICardData {

    public int Id { get; set; }
    public string Name { get; set; }
    public int Cost { get; set; }
    public Sprite Artwork { get; set; }
    public CardRarity Rarity { get; set; }
    public string ArtworkBase64 { get; set; }

    public bool HasEffect();
}