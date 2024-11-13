


using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CollectionElementVillain : MonoBehaviour, IPointerDownHandler {

    private GameState_DeckBuilder state;

    [SerializeField] private Villain villain;
    [SerializeField] private TextMeshProUGUI health;
    [SerializeField] private TextMeshProUGUI villainName;
    [SerializeField] private Image artwork;
    [SerializeField] private TextMeshProUGUI ability;

    public void Init (Villain villain, GameState_DeckBuilder state) {

        this.state = state;
        this.villain = villain;
        this.artwork.sprite = villain.artwork;
        this.health.text = villain.health.ToString();
        this.villainName.text = villain.Name;
        this.ability.text = villain.cardText;
    }

    public void OnPointerDown(PointerEventData eventData) {
        if (eventData.button != PointerEventData.InputButton.Left) return;

        var changeData = Utils.GetDeckBuilderChangeData();
        changeData.villain = this.villain;
        this.state.Invoke_GameStateChanged(GameState_DeckBuilder_ChangeReason.Input_ClickedOnVillain, changeData);
    }
}