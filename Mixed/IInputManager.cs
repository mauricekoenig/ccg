



using System;
using UnityEngine;

public interface IInputManager {

    event Action <ICardView> OnLeftClickedCardView;
    event Action <ICardView> OnRightClickedCardView;
    event Action OnRightClickWhileTargeting;
    event Action OnLeftClickWhileTargeting;

    public void OnLeftMouseDown();
    public void OnRightMouseDown();
}