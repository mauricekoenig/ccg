using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardView_OnBoard : MonoBehaviour, ICardView {

    private RuntimeCardData data;

    [SerializeField] private CardZone zone;
    [SerializeField] private TextMeshProUGUI attackTmp;
    [SerializeField] private TextMeshProUGUI healthTmp;
    [SerializeField] private Image artwork;
    [SerializeField] private GameObject icon_summoningSickness;

    private ICardInteraction interactionLogic;

    private GameState gameState;
    public Transform Transform => this.transform;
    public RuntimeCardData Data => this.data;

    private int id;
    public int ID => this.id;

    private Canvas canvas;
    public Canvas Canvas => this.canvas;

    private void Awake() {

        canvas = GetComponent<Canvas>();
        this.interactionLogic = GetComponent<ICardInteraction>();
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

            ToggleSummoningSickness();

            crd.OnAttackChanged += Handler_OnAttackChanged;
            crd.OnHealthChanged += Handler_OnHealthChanged;
            crd.OnAttacksPerTurnChanged += ToggleSummoningSickness;
        }
    }

    public void Interact (GameState state, ICardView view, InputAction action) {

        if (this.interactionLogic == null) return;

        switch (action) {

            case InputAction.LeftMouse:
                this.interactionLogic.LeftClick(state, view);
                break;

            case InputAction.RightMouse:
                this.interactionLogic.RightClick(state, view);
                break;

            case InputAction.MiddleMouse:
                this.interactionLogic.MiddleClick(state, view);
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

    public void Handler_OnAttackChanged (CreatureRuntimeCardData creatureData) {

        this.attackTmp.text = creatureData.Attack.ToString();
    }

    public void Handler_OnHealthChanged (CreatureRuntimeCardData creatureData, bool creatureDied) {

        int oldHealth = int.Parse(this.healthTmp.text);

        if (creatureData.Health < oldHealth) {
            this.healthTmp.color = Color.red;
        }

        if (creatureData.Health > oldHealth) {
            this.healthTmp.color = Color.green;
        }

        this.healthTmp.text = creatureData.Health.ToString();
        if (!creatureDied) return;

        var changeData = GameStateChangeData.New(this.gameState);
        changeData.affectedView = this;
        this.gameState.NotifyStateChange(GameStateChangeReason.Battle_CreatureDied, changeData);
    }

    public void Handler_OnAttacksPerTurnChanged (CreatureRuntimeCardData creatureData) {
        ToggleSummoningSickness();
    }

    public void ToggleSummoningSickness () {

        if (Data is not CreatureRuntimeCardData) return;
        var creature = Data as CreatureRuntimeCardData;
        if (creature.AttacksPerTurn > 0) icon_summoningSickness.SetActive(false);
        else icon_summoningSickness.SetActive(true);
    }
}
