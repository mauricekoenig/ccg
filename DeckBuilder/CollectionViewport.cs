


using TMPro;
using UnityEngine;

public class CollectionViewport : MonoBehaviour {

    [SerializeField] private GameData gameData;

    [SerializeField] private Transform viewport_Cards;
    [SerializeField] private Transform viewport_Decks;

    [SerializeField] private GameObject collectionElementPrefab;
    [SerializeField] private GameObject collectionElementVillainPrefab;
    [SerializeField] private GameObject deckPreviewPrefab;

    [SerializeField] private GameObject villainIcon;
    [SerializeField] private GameObject collectionIcon;
    [SerializeField] private TextMeshProUGUI heading;


    public void ShowDecks () {

        ClearViewports();
        var metaData = gameData.GetAllDeckMetaData();
        foreach (var record in metaData) Instantiate(deckPreviewPrefab, viewport_Decks);
        Debug.Log(metaData.Count);
    }

    private void ClearViewports () {

        foreach (Transform t in viewport_Decks) Destroy(t.gameObject);
        foreach (Transform t in viewport_Cards) Destroy(t.gameObject);
    }
}