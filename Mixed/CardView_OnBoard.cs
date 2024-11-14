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

    private GameState gameState;
    public Transform Transform => this.transform;
    public RuntimeCardData Data => this.data;

    private void Awake() {

        canvas = GetComponent<Canvas>();
        this.interactionLogic = GetComponent<ICardInteraction>();
    }

    public void Init(GameState gameState, RuntimeCardData data, ICardInteraction interactionLogic) {

        this.gameState = gameState;
        this.data = data;
        this.interactionLogic = interactionLogic;

        if (data == null) {
            Debug.Log("Data is null!");
            return;
        }

        this.gameState = gameState;
        this.data = data;
        artwork.sprite = data.Artwork;

        if (data is CreatureRuntimeCardData) {

            CreatureRuntimeCardData crd = data as CreatureRuntimeCardData;
            attackTmp.text = crd.Attack.ToString();
            healthTmp.text = crd.Health.ToString();
        }


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

    public void SetInteractionBehaviour(ICardInteraction cardInteraction) {

        this.interactionLogic = cardInteraction;
    }

    public void SetZone(CardZone zone) {

        this.zone = zone;
    }

}
