



using UnityEngine;

public abstract class BaseVillainAbility : ScriptableObject {

    public string Name;
    public int cost;
    public VillainAbilityType type;

    public abstract void Apply(GameState state);
}

public enum VillainAbilityType {

    Summon,
    Draw,
    Shuffle,
    Buff
}