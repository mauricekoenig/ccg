


public class GameStateChangeData {

    public GameState state;
    public ICardView affectedView;
    public CardPool cardPool;
    public RuntimeCardData cardData;
    public Villain villain;

    public CreatureRuntimeCardData attackingCreature;
    public CreatureRuntimeCardData defendingCreature;

    public GameStateChangeData (GameState state) {

        this.state = state;
    }

    public static GameStateChangeData New (GameState state) {
        return new GameStateChangeData (state);
    }
}
