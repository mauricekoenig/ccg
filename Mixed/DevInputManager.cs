



using System;
using UnityEngine;

public class DevInputManager : MonoBehaviour, IInputManager {

    private Camera mainCam;
    private IMediator mediator;

    public LayerMask cardViewLayer;
    public bool raycast;

    public event Action<ICardView> OnLeftClickedCardView;
    public event Action<ICardView> OnRightClickedCardView;
    public event Action OnRightClickWhileTargeting;
    public event Action OnLeftClickWhileTargeting;

    private ICardView lastSelection;

    private void Start () {
        
        mainCam = Camera.main;
        mediator = GetComponent<IMediator>();
    }

    private void Update () {

        if (!raycast) return;

        if (Input.GetMouseButtonDown(0)) {

            if (TargetingManager.IsTargeting) {
                OnLeftClickWhileTargeting?.Invoke();
                return;
            }

            OnLeftMouseDown();
        }

        if (Input.GetMouseButtonDown(1)) {

            if (TargetingManager.IsTargeting) {
                OnRightClickWhileTargeting?.Invoke();
                return;
            }

            OnRightMouseDown();
        }
    }

     public void OnLeftMouseDown() {

        if (Physics.Raycast(GetMouseRay(), out RaycastHit hit, 50, cardViewLayer)) {
             
            ICardView view = hit.collider.gameObject.GetComponent<ICardView>();
            this.lastSelection = view;
            OnLeftClickedCardView?.Invoke(view);
        }
    }

    public void OnRightMouseDown() {

        if (Physics.Raycast(GetMouseRay(), out RaycastHit hit, 50, cardViewLayer)) {

            ICardView view = hit.collider.gameObject.GetComponent<ICardView>();
            this.lastSelection = view;
            OnRightClickedCardView?.Invoke(view);
        }
    }

    private Ray GetMouseRay() {
        return Camera.main.ScreenPointToRay(Input.mousePosition);
    }
}