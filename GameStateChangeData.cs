


public class GameStateChangeData {

    public GameState state;
    public CardView3D affectedView;
    public CardPool cardPool;
    public CardRuntimeData cardData;
    public Villain villain;

    public GameStateChangeData (GameState state) {

        this.state = state;
    }

    public static GameStateChangeData New (GameState state) {
        return new GameStateChangeData (state);
    }
}
