


using System;

public interface IVillainAbilityManager {

    public void Init (IMediator mediator);

    event Action OnVillainAbilityHandled;
}