
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu]
public class GameData : ScriptableObject {

    public Villain TestVillain;

    private HashSet<RuntimeCardData> allCards = new();
    private HashSet<CreatureRuntimeCardData> allCreatures = new();
    private HashSet<SpellRuntimeCardData> allSpells = new();
    private HashSet<LocationRuntimeCardData> allLocations = new();
    private HashSet<BaseEffect> allEffects = new();
    private HashSet<Villain> allVillains = new();

    public RuntimeCardData GetCardById (int id) {

        return allCards.Where(x => x.Id == id).FirstOrDefault();
    }
    public RuntimeCardData GetCardByName (string name) {

        return allCards.Where(x => x.Name == name).FirstOrDefault();
    }
    public HashSet<RuntimeCardData> GetAllCards() {
        return allCards;
    }

    public HashSet<Villain> GetAllVillains() {
        return allVillains;
    }

    public void AssignSpells (HashSet<SpellRuntimeCardData> spells) {

        allSpells = spells;
        foreach (var card in spells) allCards.Add(card);
    }
    public void AssignCreatures (HashSet<CreatureRuntimeCardData> creatures) {

        allCreatures = creatures;
        foreach (var card in creatures) allCards.Add(card);
    }
    public void AssignLocations (HashSet<LocationRuntimeCardData> locations) {
        allLocations = locations;
        foreach (var card in allLocations) allCards.Add(card);
    }
    public void AssignCards <T> (HashSet<T> cards) where T : RuntimeCardData {
        foreach (var card in cards) allCards.Add(card);
    }

    public void LoadAllEffects () {

        var effects = Resources.LoadAll<BaseEffect>("Effects");
        foreach (var effect in effects) allEffects.Add(effect);
    }
    public void LoadAllSprites () {

        foreach (var card in allCards) {
            card.Artwork = Utils.CreatureSpriteFromBase64(card.ArtworkBase64);
        }
    }
    public void LoadAllVillains () {

        var villains = Resources.LoadAll<Villain>("Villains");
        foreach (var villain in villains) allVillains.Add(villain);
    }

    public RuntimeCardDeck GetTestDeck () {
        return new RuntimeCardDeck("Test Deck", allCards.ToList(), TestVillain);
    }

}