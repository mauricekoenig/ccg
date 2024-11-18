


using UnityEngine;

public class BoardManager : MonoBehaviour, IBoardManager {

    private GameState gameState;

    public bool CanAttack (ICardView view) {

        var creature = view.Data as CreatureRuntimeCardData;
        return creature.AttacksPerTurn > 0;
    }

    public void Init (GameState gameState) {
        this.gameState = gameState;
    }
}