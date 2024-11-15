


using System;
using UnityEngine;

public class TargetingManager : MonoBehaviour, ITargetingManager {

    private static bool isTargeting;
    public static bool IsTargeting => isTargeting;

    [SerializeField] private TargetingSettings targetingSettings;
    [SerializeField] private ICardView currentView;
    [SerializeField] private GameObject targetArrowPrefab;
    [SerializeField] private GameObject targetArrowInstance;

    private TargetArrow targetArrow;

    public event Action<ICardView> OnStartTargeting;
    public event Action<ICardView> OnEndTargeting;

    void Start () {

       CreateArrowInstance();
    }

    private void CreateArrowInstance () {

        if (targetArrowPrefab == null) return;
        this.targetArrowInstance = Instantiate(targetArrowPrefab);
        this.targetArrow = this.targetArrowInstance.GetComponent<TargetArrow>();
    }

    public void StartTargeting(ICardView cardView) {

        if (isTargeting) return;

        this.currentView = cardView;
        this.currentView.Scale(targetingSettings.endValue, targetingSettings.scaleDuration);
        this.targetArrowInstance.transform.position = cardView.Transform.position;
        this.targetArrow.SetOrigin(cardView.Transform.position);
        this.targetArrowInstance.SetActive(true);
        isTargeting = true;

        OnStartTargeting?.Invoke(cardView);
    }

    public void EndTargeting() {

        if (!isTargeting) return;

        this.currentView.Scale(1, targetingSettings.scaleDuration);
        this.targetArrowInstance.SetActive(false);
        this.targetArrowInstance.transform.position = this.currentView.Transform.position;
        this.targetArrow.Reset();
        this.currentView = null;
        isTargeting = false;

        OnEndTargeting?.Invoke(this.currentView);
    }
}