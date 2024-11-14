


using System;

public interface ITargetingManager {

    event Action<CardView3D> OnStartTargeting;
    event Action<CardView3D> OnEndTargeting;

    void StartTargeting(CardView3D cardView);
    void EndTargeting();
}