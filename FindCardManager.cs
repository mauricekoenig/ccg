

using UnityEngine;

public class FindCardManager : MonoBehaviour {

    public GameObject cardViewPrefab_findPanel;

    public void AddViewToParent (CardRuntimeData data, Transform findParent, GameState state) {

        var findOption = Instantiate(cardViewPrefab_findPanel, findParent).GetComponent<CardView_Find>();
        findOption.Init(data, state);
    }
}