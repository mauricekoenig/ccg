


using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public abstract class BaseEffect : ScriptableObject, IEffect, IIdentifiable {

    public string text;
    [SerializeField] private int id;
    public List<EffectTarget> targets = new();
    public List<EffectTrigger> triggers = new();
    public List<EffectCondition> conditions = new();

    public int ID => id;

    public abstract void Apply (GameState gameState);

    protected bool ConditionsMet (GameState gameState) {

        foreach (var condition in conditions) {
            if (!condition.IsMet(gameState))
                return false;
        }

        return true;
    }
}
