


using UnityEngine;


public abstract class EffectCondition : ScriptableObject {

    public abstract bool IsMet(GameState gameState);
}