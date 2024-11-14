




using UnityEngine;

public class CardInteraction_Play : ICardInteraction {

    private ICardView cardView;

    public CardInteraction_Play(ICardView view) {

        this.cardView = view;
        this.cardView.SetZone(CardZone.Play);
    }

    public void LeftClick (GameState gameState) {

        Debug.Log("Hello, I am here!");
        if (TargetingManager.IsTargeting) {
            Debug.Log("Already targeting!");
            return;
        }


        var changeData = GameStateChangeData.New(gameState);
        changeData.affectedView = this.cardView;
        gameState.NotifyStateChange(GameStateChangeReason.Input_LeftClickedOnFriendlyCardInPlay, changeData);
    }

    public void RightClick(GameState gameState) {
        

    }
    public void MiddleClick(GameState gameState) {
        

    }
}