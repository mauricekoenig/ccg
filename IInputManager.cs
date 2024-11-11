



using System;
using UnityEngine;

public interface IInputManager {

    event Action <CardView3D> ON_LeftClickedCardView;
    event Action <CardView3D> ON_RightClickedCardView;

    public void OnLeftMouseDown();
    public void OnRightMouseDown();
}