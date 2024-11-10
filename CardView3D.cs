using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardView3D : MonoBehaviour
{
    public SpriteRenderer cost;
    public SpriteRenderer artwork;
    public CardRuntimeData data;
    public CardZone viewPosition;

    public TextMeshPro costText;
    public TextMeshPro atkText;
    public TextMeshPro healthText;
    public TextMeshPro nameText;

    private ICardInteraction interactionLogic;

    public void Init (CardRuntimeData data, ICardInteraction interactionLogic) {

        this.data = data;
        this.costText.text = data.cardCost.ToString();
        artwork.sprite = data.artwork;
        nameText.text = data.cardName;

        if (data is CreatureRuntimeData) {

            CreatureRuntimeData crd = data as CreatureRuntimeData;
            atkText.text = crd.Attack.ToString();
            healthText.text = crd.Health.ToString();
        }


        if (data is SpellRuntimeData) {

        }

        this.interactionLogic = interactionLogic;
    }

    public void SetZone (CardZone zone) {

        viewPosition = zone;
    }
    public void SetInteractionLogic (ICardInteraction interactionLogic) {
        this.interactionLogic = interactionLogic;
    }

    public void Interact (GameState state, InputAction action) {
        if (this.interactionLogic == null) return;

        switch (action) {

            case InputAction.LeftMouse:
                this.interactionLogic.LeftClick(state);
                break;

            case InputAction.RightMouse:
                this.interactionLogic.RightClick(state);
                break;

            case InputAction.MiddleMouse:
                this.interactionLogic.MiddleClick(state);
                break;
        }
    }

}

public enum InputAction {

    LeftMouse,
    RightMouse,
    MiddleMouse,
}
