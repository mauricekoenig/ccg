
using UnityEngine;

public class CardInteraction_Play : ICardInteraction {

    private ICardView cardView;

    public CardInteraction_Play(ICardView view) {

        this.cardView = view;
        this.cardView.SetZone(CardZone.Play);
    }

    public void LeftClick (GameState gameState) {

        var changeData = GameStateChangeData.New(gameState);

        if (TargetingManager.IsTargeting) {

            if (TargetingManager.CurrentViewTargeting.ID == this.cardView.ID) {
                return;
            }

            changeData.affectedView = this.cardView;
            gameState.NotifyStateChange(GameStateChangeReason.Input_LeftClickedOnCardInPlay_WhileTargeting, changeData);
            return;
        }

        changeData.affectedView = this.cardView;
        gameState.NotifyStateChange(GameStateChangeReason.Input_LeftClickedOnCardInPlay, changeData);
    }

    public void RightClick (GameState gameState) {

    }
    public void MiddleClick(GameState gameState) {
        

    }
}