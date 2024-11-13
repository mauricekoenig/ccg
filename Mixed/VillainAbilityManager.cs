


using System;
using UnityEngine;

[RequireComponent(typeof(CardViewManager))]
public class VillainAbilityManager : MonoBehaviour, IVillainAbilityManager {

    [SerializeField] GameData gameData;

    private IMediator mediator;
    private CardViewManager cardViewManager;

    public event Action OnVillainAbilityHandled;

    void Awake () {

        this.cardViewManager = GetComponent<CardViewManager>();
    }

    public void Init (IMediator mediator) {

        this.mediator = mediator;
        this.mediator.OnVillainAbilityClicked += Handler_OnVillainAbilityClicked;
    }

    private void Handler_OnVillainAbilityClicked(GameState state, Villain villain) {

        if (villain.ability == null) {
            Debug.Log("No ability set! VillainAbility is null!");
            return;
        }

        if (state.ActivePlayer.CurrentMana < villain.ability.cost) return;

        switch (villain.ability.type) {

            case VillainAbilityType.Summon:
                if (state.ActivePlayer.CardsOnBoard > 4) return;
                HandleSummonAbility(state, villain);
            break;

            case VillainAbilityType.Draw:
                HandleDrawAbility();
            break;

            case VillainAbilityType.Shuffle:
                HandleShuffleAbility();
            break;

            case VillainAbilityType.Buff:
                HandleBuffAbility();
            break;
        }

        state.ActivePlayer.resources.currentMana -= villain.ability.cost;
        OnVillainAbilityHandled?.Invoke();
    }

    private void HandleSummonAbility (GameState state, Villain villain) {

        var summonAbility = villain.ability as VillainAbility_SummonCreature;
        var owner = state.ActivePlayer.ID == 1 ? CardViewOwner.Local : CardViewOwner.Remote;
        var parent = state.ActivePlayer.ID == 1 ? cardViewManager.BoardView1 : cardViewManager.BoardView2;

        for (int i = 0; i < summonAbility.howMany; i++) {

            RuntimeCardData creatureData = gameData.GetCardByName(summonAbility.creatureName);

            state.ActivePlayer.AddCardToBoard(creatureData);
            var cardView = cardViewManager.CreateCardView(creatureData, owner, parent);
            cardView.Init(creatureData, new CardInteraction_Play(cardView));
        }

        this.cardViewManager.UpdateBoard(state.ActivePlayer.ID);
    }
    private void HandleDrawAbility () {

    }
    private void HandleShuffleAbility() {

    }
    private void HandleBuffAbility () {

    }
}