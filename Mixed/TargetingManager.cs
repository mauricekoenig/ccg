


using System;
using UnityEngine;

public class TargetingManager : MonoBehaviour, ITargetingManager {

    [SerializeField] private CardView3D currentView;

    public event Action<CardView3D> OnStartTargeting;
    public event Action<CardView3D> OnEndTargeting;

    public void StartTargeting(CardView3D cardView) {

        this.currentView = cardView;
        OnStartTargeting?.Invoke(cardView);

    }
    public void EndTargeting() {

        OnEndTargeting?.Invoke(this.currentView);
        this.currentView = null;
    }
}