


using UnityEngine;

public interface ICardView {

    public Transform Transform { get; }
    public RuntimeCardData Data { get; }

    void Scale(float endValue, float time);
    void SetZone (CardZone zone);
    void Interact (GameState state, InputAction action);
    void SetInteractionBehaviour (ICardInteraction cardInteraction);
    void Init(GameState gameState, RuntimeCardData data, ICardInteraction interactionLogic);
}