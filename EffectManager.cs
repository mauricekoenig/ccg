


using UnityEngine;

public class EffectManager : MonoBehaviour, IEffectManager {

    private IMediator mediator;

    private void Awake() {
        mediator = GetComponent<IMediator>();
    }

    public void Handle (CardRuntimeData card) {

        GameState state = mediator.GetGameState();

        foreach (var effect in card.effects) {

            if (!AllConditionsMet(effect, state)) return;
            effect.Apply(state);
        }
    }

    private bool AllConditionsMet (BaseEffect effect, GameState state) {

        foreach (var condition in effect.conditions) {
            if (!condition.IsMet(state)) return false;
        }

        return true;
    }
}