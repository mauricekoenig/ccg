using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardView_Find : MonoBehaviour, IPointerDownHandler
{
    private GameState gameState;
    private CardRuntimeData cardRuntimeData;
    [SerializeField] private Image artwork;

    public void Init (CardRuntimeData data, GameState state) {

        this.gameState = state;
        cardRuntimeData = data;
        artwork.sprite = data.artwork;
    }

    public void OnPointerDown(PointerEventData eventData) {

        var changeData = GameStateChangeData.New(gameState);
        changeData.cardData = cardRuntimeData;
        this.gameState.NotifyStateChange(GameStateChangeReason.Input_ClickedOnCardInFindWindow, changeData);
    }
}
