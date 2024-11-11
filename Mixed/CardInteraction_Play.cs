




public class CardInteraction_Play : ICardInteraction {

    private CardView3D cardView;

    public CardInteraction_Play(CardView3D view) {

        this.cardView = view;
        this.cardView.SetZone(CardZone.Play);
    }

    public void LeftClick (GameState gameState) {
        
    }

    public void RightClick(GameState gameState) {
        

    }
    public void MiddleClick(GameState gameState) {
        

    }
}