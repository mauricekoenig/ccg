



using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu]
public class GameData : ScriptableObject {

    public Villain TestVillain;

    private HashSet<RuntimeCardData> allCards = new();
    private HashSet<CreatureRuntimeCardData> allCreatures = new();
    private HashSet<SpellRuntimeCardData> allSpells = new();
    private HashSet<LocationRuntimeCardData> allLocations = new();

    public RuntimeCardData GetCardById (int id) {

        return allCards.Where(x => x.Id == id).FirstOrDefault();
    }
    public RuntimeCardData GetCardByName (string name) {

        return allCards.Where(x => x.Name == name).FirstOrDefault();
    }

    public void AssignSpells (HashSet<SpellRuntimeCardData> spells) {

        allSpells = spells;
        allCards.AddRange(spells);
    }
    public void AssignCreatures (HashSet<CreatureRuntimeCardData> creatures) {

        allCreatures = creatures;
        allCards.AddRange(creatures);
    }
    public void AssignLocations (HashSet<LocationRuntimeCardData> locations) {
        allLocations = locations;
        allCards.AddRange(locations);
    }

    public RuntimeCardDeck GetTestDeck () {
        return new RuntimeCardDeck("Test Deck", allCards.ToList(), TestVillain);
    }

    public void LoadSpritesForAllCards() {

        foreach (var card in allCards) {
            card.Artwork = Utils.CreatureSpriteFromBase64(card.ArtworkBase64);
        }
    }
}