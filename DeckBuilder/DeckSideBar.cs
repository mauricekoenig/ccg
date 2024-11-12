


using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeckSideBar : MonoBehaviour {

    public Image villainArtwork;
    public TMP_InputField deckName;
    [SerializeField] private Transform deckElementParent;
    [SerializeField] private GameObject deckElementPrefab;
    [SerializeField] private GameObject deckSideBar;
    private DeckPreview deckPreview;
    public GameObject navigateBackArrow;

    private Animator anim;
    private GameState_DeckBuilder state;

    private void Awake() {

        anim = deckSideBar.GetComponent<Animator>();
    }

    public void Show (DeckPreview deckPreview, GameState_DeckBuilder state) {

        ClearSideBar();
        this.state = state;
        this.deckPreview = deckPreview;
        this.anim.Play("DeckBuilder_SideBar_FadeIn");
        villainArtwork.sprite = deckPreview.Deck.Villain.artwork;
        deckName.text = deckPreview.DeckName;
        this.navigateBackArrow.SetActive(true);

        ClearDeckElements();
        CreateDeckElements(this.deckPreview);
    }
    private void CreateDeckElements (DeckPreview preview) {

        foreach (var card in preview.Deck.Cards) {

            Instantiate(deckElementPrefab, deckElementParent).GetComponent<DeckElement>().Init(card);
        }
    }
    private void ClearDeckElements () {

        foreach (Transform t in deckElementParent) Destroy(t.gameObject);
    }
    public void GoBackToDeckView () {

        anim.Play("DeckBuilder_SideBar_FadeOut");
        this.navigateBackArrow.SetActive(false);
        var changeData = Utils.GetDeckBuilderChangeData();
        this.state.Invoke_GameStateChanged(GameState_DeckBuilder_ChangeReason.Input_ClickedOnBackToDeckPreviewArrow, changeData);
    }

    private void ClearSideBar() {

        villainArtwork.sprite = null;
        deckName.text = string.Empty;
        ClearDeckElements();
    }
}