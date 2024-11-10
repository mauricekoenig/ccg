




using UnityEngine;

public class CardInteraction_Hand : ICardInteraction {

    private CardView3D cardView;

    public CardInteraction_Hand(CardView3D view) {

        this.cardView = view;
        this.cardView.SetZone(CardZone.Hand);
    }

    public void LeftClick (GameState gameState) {

        if (gameState.ActivePlayer.CardsOnBoard > 4) return;

        if (gameState.ActivePlayer.resources.currentMana >= this.cardView.data.cardCost) {

            gameState.ActivePlayer.cards.MoveCardBetweenZones(cardView.data, CardZone.Hand, CardZone.Play);
            gameState.ActivePlayer.resources.currentMana -= cardView.data.cardCost;

            var changeData = GameStateChangeData.New(gameState);
            changeData.affectedView = this.cardView;

            gameState.NotifyStateChange(GameStateChangeReason.Action_PlayedCardFromHand, changeData) ;
        }
    }
    public void RightClick(GameState gameState) {
        

    }
    public void MiddleClick(GameState gameState) {
        

    }
}