


using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeckSideBar : MonoBehaviour {

    public Image villainArtwork;
    public TMP_InputField deckName;
    [SerializeField] private Transform deckElementParent;
    [SerializeField] private GameObject deckElementPrefab;

    public void Show (DeckPreview deckPreview) {

        villainArtwork.sprite = deckPreview.VillainData.artwork;
        deckName.text = deckPreview.DeckName;
        ClearDeckElements();
    }

    private void ClearDeckElements () {

        foreach (Transform t in deckElementParent) Destroy(t.gameObject);
    }
}