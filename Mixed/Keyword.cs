



using UnityEngine;

public abstract class Keyword : ScriptableObject, IIdentifiable {

    [SerializeField] private int id;
    [SerializeField] private KeywordType type;

    public int ID => id;
    public KeywordType Type => type;

    public abstract void Apply (CreatureRuntimeCardData creatureData);
}