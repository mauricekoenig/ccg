



public class ControlAnotherCreatureOfTypeCondition : EffectCondition {

    public CreatureType Type;

    public override bool IsMet (GameState gameState) {

        return true; 
    }
}