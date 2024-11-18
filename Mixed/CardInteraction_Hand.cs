




using UnityEngine;

public class CardInteraction_Hand : ICardInteraction {

    private ICardView cardView;

    public CardInteraction_Hand(ICardView view) {

        this.cardView = view;
        this.cardView.SetZone(CardZone.Hand);
    }

    public void LeftClick (GameState gameState, ICardView view) {

        if (gameState.ActivePlayer.ID != view.ID) return;
        if (gameState.ActivePlayer.CardsOnBoard > 4) return;

        if (gameState.ActivePlayer.resources.currentMana >= this.cardView.Data.Cost) {

            gameState.ActivePlayer.cards.MoveCardBetweenZones(cardView.Data, CardZone.Hand, CardZone.Play);
            gameState.ActivePlayer.resources.currentMana -= cardView.Data.Cost;

            var changeData = GameStateChangeData.New(gameState);
            changeData.affectedView = this.cardView;

            gameState.NotifyStateChange(GameStateChangeReason.Action_PlayedCardFromHand, changeData) ;
        }
    }
    public void RightClick(GameState gameState, ICardView view) {
        

    }
    public void MiddleClick(GameState gameState, ICardView view) {
        

    }
}