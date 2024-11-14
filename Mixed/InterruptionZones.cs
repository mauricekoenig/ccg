


using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InterruptionZones : MonoBehaviour {

    public event Action OnInterruptionZoneEntered;

    private void OnMouseEnter() {
        
        if (TargetingManager.IsTargeting) {
            OnInterruptionZoneEntered?.Invoke();
            return;
        } 
    }
}