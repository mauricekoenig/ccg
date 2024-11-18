



using System;
using UnityEngine;

public interface IInputManager {

    event Action <ICardView> OnLeftClickedCardView;
    event Action <ICardView> OnRightClickedCardView;
    event Action OnRightClickWhileTargeting;

    public void OnLeftMouseDown();
    public void OnRightMouseDown();
}