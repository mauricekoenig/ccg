using DG.Tweening;
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

    private int id;
    public int ID => this.id;
    
    public int ID_Display;

    private void Awake() {

        canvas = GetComponent<Canvas>();
        this.interactionLogic = GetComponent<ICardInteraction>();
    }

    void Update () {
        ID_Display = ID;
    }

    public void Init (int playerID, GameState gameState, RuntimeCardData data, ICardInteraction interactionLogic) {

        if (data == null) {
            Debug.Log("Data is null!");
            return;
        }

        this.id = playerID;
        this.gameState = gameState;
        this.data = data;
        artwork.sprite = data.Artwork;
        this.interactionLogic = interactionLogic;

        if (data is CreatureRuntimeCardData) {

            CreatureRuntimeCardData crd = data as CreatureRuntimeCardData;
            attackTmp.text = crd.Attack.ToString();
            healthTmp.text = crd.Health.ToString();
        }
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

    public void Scale (float endValue, float time) {

        this.transform.DOScale(endValue, time).SetEase(Ease.OutSine);
    }
}
