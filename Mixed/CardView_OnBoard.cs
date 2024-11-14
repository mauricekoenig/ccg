using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardView_OnBoard : MonoBehaviour, ICardView {

    private Canvas canvas;
    private RuntimeCardData data;

    [SerializeField] private CardZone zone;
    [SerializeField] private TextMeshProUGUI attackTmp;
    [SerializeField] private TextMeshProUGUI healthTmp;
    [SerializeField] private Image artwork;

    private ICardInteraction interactionLogic;

    public Transform Transform => this.transform;
    public RuntimeCardData Data => this.data;

    private void Awake() {

        canvas = GetComponent<Canvas>();
        this.interactionLogic = GetComponent<ICardInteraction>();
    }

    public void Init(RuntimeCardData data) {

        this.data = data;
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

    public void SetInteractionBehaviour(ICardInteraction cardInteraction) {

        this.interactionLogic = cardInteraction;
    }

    public void SetZone(CardZone zone) {

        this.zone = zone;
    }
}
