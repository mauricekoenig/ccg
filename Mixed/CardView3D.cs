using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardView3D : MonoBehaviour
{
    public SpriteRenderer cost;
    public SpriteRenderer artwork;
    public RuntimeCardData data;
    public CardZone viewPosition;

    public TextMeshPro costText;
    public TextMeshPro atkText;
    public TextMeshPro healthText;
    public TextMeshPro nameText;

    private ICardInteraction interactionLogic;

    public void ResetView() {

        this.data = null;
        this.viewPosition = CardZone.None;
        costText.text = string.Empty;
        atkText.text = string.Empty;
        healthText.text = string.Empty;
        nameText.text = string.Empty;
        interactionLogic = null;
        artwork.sprite = null;
    }

    public void Init (RuntimeCardData data, ICardInteraction interactionLogic) {

        if (data == null) {
            Debug.Log("NULL!");
            return;
        }

        this.data = data;
        this.costText.text = data.Cost.ToString();
        artwork.sprite = data.Artwork;
        nameText.text = data.Name;

        if (data is CreatureRuntimeCardData) {

            CreatureRuntimeCardData crd = data as CreatureRuntimeCardData;
            atkText.text = crd.Attack.ToString();
            healthText.text = crd.Health.ToString();
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
