



using System;
using UnityEngine;

public class DevInputManager : MonoBehaviour, IInputManager {

    private Camera mainCam;
    private IMediator mediator;

    public LayerMask cardViewLayer;
    public bool raycast;

    public event Action<CardView3D> ON_LeftClickedCardView;
    public event Action<CardView3D> ON_RightClickedCardView;

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
             
            CardView3D view = hit.collider.gameObject.GetComponent<CardView3D>();
            ON_LeftClickedCardView?.Invoke(view);
        }
    }

    public void OnRightMouseDown() {

        if (Physics.Raycast(GetMouseRay(), out RaycastHit hit, 50, cardViewLayer)) {

            CardView3D view = hit.collider.gameObject.GetComponent<CardView3D>();
            ON_RightClickedCardView?.Invoke(view);
        }
    }

    private Ray GetMouseRay() {
        return Camera.main.ScreenPointToRay(Input.mousePosition);
    }
}