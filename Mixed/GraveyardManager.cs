

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GraveyardManager : MonoBehaviour, IGraveyardManager {

    [SerializeField] private GameObject cardView_Graveyard_Prefab;
    [SerializeField] private Transform cardView_Graveyard_PoolParent;

    [SerializeField] private GameObject graveyardWindow;
    [SerializeField] private Transform graveyardWindow_Content_1;
    [SerializeField] private Transform graveyardWindow_Content_2;

    private PlayerManager playerManager;

    private Queue<GameObject> cardViewGraveyard_Pool = new();

    private Dictionary<int, List<RuntimeCardData>> Lists = new();

    void Awake () {

        playerManager = GetComponent<PlayerManager>();
        CreatePool(30);
    }

    void Start () {

        this.playerManager.OnCardMovedToGraveyard += Handler_OnCardMovedToGraveyard;
    }

    private void Handler_OnCardMovedToGraveyard (int id, RuntimeCardData data) {

        Debug.Log($"{data.Name} with ID {id.ToString()} moved to graveyard!");
        Transform panel = id == 1 ? graveyardWindow_Content_1 : graveyardWindow_Content_2;
        var view = CreateView(data);
        view.transform.SetParent(panel);
    }

    public void ShowGraveyard (int playerId) {

        Transform toShow = playerId == 1 ? graveyardWindow_Content_1 : graveyardWindow_Content_2;
        Transform toHide = playerId == 1 ? graveyardWindow_Content_2 : graveyardWindow_Content_1;
        toHide.gameObject.SetActive(false);
        toShow.gameObject.SetActive(true);
        if (graveyardWindow.activeInHierarchy) return;
        graveyardWindow.SetActive(true);
    }

    public void HideGraveyard () {
        this.graveyardWindow.SetActive(false);
    }


    private GameObject CreateView (RuntimeCardData data) {

        if (cardViewGraveyard_Pool.Count == 0) {
            Debug.Log("Pool is empty!");
            return null;
        }

        var viewGameObject = cardViewGraveyard_Pool.Dequeue();
        var graveyardView = viewGameObject.GetComponent<CardView_Graveyard>();
        graveyardView.Init(data);
        return viewGameObject;
    }
    private void ReQueueView (GameObject view) {
        cardViewGraveyard_Pool.Enqueue(view);
    }
    private void CreatePool (int amount) {

        for (int i = 0; i < amount; i++) {
            cardViewGraveyard_Pool.Enqueue(Instantiate(cardView_Graveyard_Prefab, new Vector3(-100, -100), Quaternion.identity, cardView_Graveyard_PoolParent));
        }

        cardView_Graveyard_PoolParent.name = $"Graveyard Pool: {cardView_Graveyard_PoolParent.childCount}";
    }
}