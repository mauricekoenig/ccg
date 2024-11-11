


using System.Collections.Generic;
using UnityEngine;

public class EffectFactory : MonoBehaviour {

    public DamageEffect GetDamageEffect (int amount, List<EffectTrigger> triggers, List<EffectCondition> conditions, List<EffectTarget> targets) {

        DamageEffect effect = ScriptableObject.CreateInstance<DamageEffect>();
        effect.amount = amount;
        effect.conditions = conditions;
        effect.triggers = triggers;
        effect.targets = targets;
        return effect;
    }
}