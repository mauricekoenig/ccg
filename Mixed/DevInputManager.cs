



using System;
using UnityEngine;

public class DevInputManager : MonoBehaviour, IInputManager {

    private Camera mainCam;
    private IMediator mediator;

    public LayerMask cardViewLayer;
    public bool raycast;

    public event Action<ICardView> OnLeftClickedCardView;
    public event Action<ICardView> OnRightClickedCardView;

    private ICardView lastSelection;

    private void Start () {
        
        mainCam = Camera.main;
        mediator = GetComponent<IMediator>();
    }

    private void Update () {

        if (!raycast) return;

        if (Input.GetMouseButtonDown(0)) {

            OnLeftMouseDown();
        }

        if (Input.GetMouseButtonDown(1)) {

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