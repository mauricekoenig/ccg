


using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class StaticCardDeck : ScriptableObject {


    [SerializeField] private string deckName;
    [SerializeField] private Villain villain;
    [SerializeField] private List<RuntimeCardData> cards = new();

    public List<CreatureRuntimeCardData> creatures = new();


    public string Name => deckName;
    public List<RuntimeCardData> Cards => null;
    public Villain Villain => villain;
}