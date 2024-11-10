


using System;
using UnityEngine;

public class CardViewManager : MonoBehaviour {

    [SerializeField] private Transform handView1;
    [SerializeField] private Transform handView2;
    [SerializeField] private Transform boardView1;
    [SerializeField] private Transform boardView2;
    [SerializeField] private Transform graveyardView1;
    [SerializeField] private Transform graveyardView2;

    public Transform HandView1 => handView1;
    public Transform HandView2 => handView2;
    public Transform BoardView1 => boardView1;
    public Transform BoardView2 => boardView2;
    public Transform GraveyardView1 => graveyardView1;
    public Transform GraveyardView2 => graveyardView2;

    public IMediator mediator;
    public CardViewManagerLayoutSettings layoutSettings;
    public GameObject cardViewPrefab;

    public event Action<GameState, CardView3D> OnCardMovedToPlayZone;

    private void Awake() {
        mediator = GetComponent<IMediator>();
    }
    private void Start() {

        mediator.OnPlayerDrawCard += EventHandler_OnPlayerDrawCard;
        mediator.OnCardPlayedFromHand += Handler_OnCardPlayedFromHand;
        mediator.OnCardReturnedToHand += EventHandler_OnCardReturnedToHand;
        mediator.OnCardInFindWindowSelected += Handler_OnCardInFindWindowSelected;
    }

    private void Handler_OnCardPlayedFromHand (GameState state, CardView3D cardView) {

        Transform boardView = state.ActivePlayer.ID == 1 ? boardView1 : boardView2;
        cardView.gameObject.transform.SetParent(boardView);
        // ANIMATION LOGIC HERE
        cardView.SetInteractionLogic(new CardInteraction_Play(cardView));

        UpdateHand (state.ActivePlayer.ID);
        UpdateBoard (state.ActivePlayer.ID);

        OnCardMovedToPlayZone?.Invoke(state, cardView);
    }
    private void Handler_OnCardInFindWindowSelected (GameState state, CardRuntimeData data) {

        CardViewOwner owner = state.ActivePlayer.ID == 1 ? CardViewOwner.Local : CardViewOwner.Remote;
        Transform cardHolder = state.ActivePlayer.ID == 1 ? handView1 : handView2;
        CardView3D cardView = CreateCardView(data, owner, cardHolder);
        cardView.Init(data, new CardInteraction_Hand(cardView));

        UpdateHand(state.ActivePlayer.ID);
    }

    public void EventHandler_OnCardReturnedToHand (GameState state, CardView3D cardView) {

        Transform handView = state.ActivePlayer.ID == 1 ? handView1 : handView2;
        cardView.gameObject.transform.SetParent(handView);

        UpdateHand(state.ActivePlayer.ID);
        UpdateBoard(state.ActivePlayer.ID);
    }
    public void EventHandler_OnPlayerDrawCard(GameState state, CardRuntimeData data, int playerID) {

        int id = playerID;
        Transform hand = id == 1 ? handView1 : handView2;
        CardViewOwner owner = id == 1 ? CardViewOwner.Local : CardViewOwner.Remote;

        CardView3D cardView = CreateCardView (data, owner, hand);
        cardView.Init(data, new CardInteraction_Hand(cardView));
        UpdateHand(id);
    }
    public void EventHandler_OnCardMovedToGraveyard (GameState state) {

    }

    public void UpdateHand(int id) {

        if (id == 1) {

            for (int i = 0; i < handView1.childCount; i++) {

                float n = handView1.childCount;
                float t = n > 1 ? (float)i / (n - 1) : .5f;
                float x = Mathf.Lerp(-layoutSettings.handWidth / 2, layoutSettings.handWidth / 2, t) * (layoutSettings.cardOverlap * n);
                float y = -layoutSettings.handOffset;
                float z = Mathf.Lerp(0, -layoutSettings.depth, t);

                handView1.GetChild(i).transform.position = new Vector3(x, y, z);
            }
        }

        if (id == 2) {

            for (int i = 0; i < handView2.childCount; i++) {

                float n = handView2.childCount;
                float t = n > 1 ? (float)i / (n - 1) : .5f;
                float x = Mathf.Lerp(-layoutSettings.handWidth / 2, layoutSettings.handWidth / 2, t) * (layoutSettings.cardOverlap * n);
                float y = layoutSettings.handOffset;
                float z = Mathf.Lerp(0, -layoutSettings.depth, t);

                handView2.GetChild(i).transform.position = new Vector3(x, y, z);
            }
        }
    }
    public void UpdateBoard(int id) {

        if (id == 1) {

            for (int i = 0; i < boardView1.childCount; i++) {

                float n = boardView1.childCount;
                float t = n > 1 ? (float)i / (n - 1) : .5f;
                float x = Mathf.Lerp(-layoutSettings.boardWidth / 2, layoutSettings.boardWidth / 2, t) * (layoutSettings.boardOverlap * n);
                float y = boardView1.transform.position.y;

                boardView1.GetChild(i).transform.position = new Vector3(x, y, 0);
            }
        }

        if (id == 2) {

            for (int i = 0; i < boardView2.childCount; i++) {

                float n = boardView2.childCount;
                float t = n > 1 ? (float)i / (n - 1) : .5f;
                float x = Mathf.Lerp(-layoutSettings.boardWidth / 2, layoutSettings.boardWidth / 2, t) * (layoutSettings.boardOverlap * n);
                float y = boardView2.transform.position.y;

                boardView2.GetChild(i).transform.position = new Vector3(x, y, 0);
            }
        }
    }

    public CardView3D CreateCardView (CardRuntimeData cardRuntimeData, CardViewOwner owner, Transform parent) {
        return Instantiate(cardViewPrefab, Vector3.zero, GetCardRotation(owner), parent).GetComponent<CardView3D>();
    }


    private Quaternion GetCardRotation(CardViewOwner owner) {
        switch (owner) {
            case CardViewOwner.Local:
                return Quaternion.identity;
            case CardViewOwner.Remote:
                return Quaternion.Euler(0, 0, 180);
            default:
                return Quaternion.identity;
        }
    }

}

