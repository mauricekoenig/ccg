


using System;

public interface ITargetingManager {

    event Action<ICardView> OnStartTargeting;
    event Action<ICardView> OnEndTargeting;

    void StartTargeting(ICardView cardView);
    void EndTargeting();
}