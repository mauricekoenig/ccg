


using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using System;

public class VillainUI : MonoBehaviour, IPointerDownHandler {

    private Villain data;
    public Image artworkHolder;
    public TextMeshProUGUI abilityCost;
    private GameState gameState;

    public void Init (Villain data, GameState state) {

        this.data = data;
        artworkHolder.sprite = data.artwork;

        if (data.ability == null) {
            abilityCost.text = "-1";
        }

        else abilityCost.text = data.ability.cost.ToString();
        this.gameState = state;
    }

    public void OnPointerDown(PointerEventData eventData) {

        var changeData = GameStateChangeData.New(this.gameState);
        changeData.villain = this.data;
        this.gameState.NotifyStateChange(GameStateChangeReason.Input_ClickedOnVillainAbility, changeData);
    }
}