



using System;
using UnityEngine;

public interface IInputManager {

    event Action <ICardView> OnLeftClickedCardView;
    event Action <ICardView> OnRightClickedCardView;

    public void OnLeftMouseDown();
    public void OnRightMouseDown();
}