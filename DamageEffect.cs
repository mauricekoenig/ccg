


using System;
using UnityEngine;

[CreateAssetMenu]
public class DamageEffect : BaseEffect, IDamage {

    public int amount;

    public override void Apply (GameState state) {

        if (!ConditionsMet(state)) return;
    }

    public int DamageAmount() {

        return amount;
    }
}
