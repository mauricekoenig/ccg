


using UnityEngine;

public interface ICardView : IIdentifiable {

    public Transform Transform { get; }
    public RuntimeCardData Data { get; }

    void Scale(float endValue, float time);
    void SetZone (CardZone zone);
    void Interact (GameState state, InputAction action);
    void SetInteractionBehaviour (ICardInteraction cardInteraction);
    void Init (int playerID, GameState gameState, RuntimeCardData data, ICardInteraction interactionLogic);
}