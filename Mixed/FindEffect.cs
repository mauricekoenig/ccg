


using UnityEngine;

[CreateAssetMenu]
public class FindEffect : BaseEffect {

    public CardPool cardPool;

    public override void Apply (GameState gameState) { 

        GameStateChangeData changeData = GameStateChangeData.New(gameState);
        changeData.cardPool = this.cardPool;
        gameState.NotifyStateChange(GameStateChangeReason.EffectActivated_FindCards, changeData);
    }
}