



using UnityEngine;

[CreateAssetMenu]
public class HealEffect : BaseEffect, IHeal {

    public int amount;

    public override void Apply(GameState gameState) {
        if (!ConditionsMet(gameState)) return;
    }

    public int HealAmount() {
        return amount;
    }
}