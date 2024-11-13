using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DeckElement : MonoBehaviour, IPointerDownHandler
{
    public TextMeshProUGUI cost;
    public List<RuntimeCardData> cardsInThisDeckElement = new();
    public TextMeshProUGUI quantityUI;
    public TextMeshProUGUI cardName;

    public GameObject prefab;
    public GameObject clone;

    CollectionElement collectionElement;

    public void Init (RuntimeCardData cardData, CollectionElement collectionElement = null) {

        Add(cardData);
        this.cardName.text = cardData.Name;
        this.cost.text = cardData.Cost.ToString();
        quantityUI.text = cardsInThisDeckElement.Count.ToString();
        if (collectionElement != null) this.collectionElement = collectionElement;
        this.gameObject.name = "DeckElement: " + cardData.Name;
    }

    public void Add (RuntimeCardData cardData) {

        if (cardsInThisDeckElement.Count == 2) {
            return;
        }

        if (cardsInThisDeckElement.Count == 0) {

            cardsInThisDeckElement.Add(cardData);
            quantityUI.text = cardsInThisDeckElement.Count.ToString();
            return;
        }

        if (cardData.ID != cardsInThisDeckElement[0].ID) return;

        cardsInThisDeckElement.Add (cardData);
        quantityUI.text = cardsInThisDeckElement.Count.ToString();
    }

    public void OnPointerDown(PointerEventData eventData) {
        
        if (eventData.button == PointerEventData.InputButton.Left) {

            if (cardsInThisDeckElement.Count == 2) return; // grey out UI
            var clone = cardsInThisDeckElement[0].Clone();
            cardsInThisDeckElement.Add(clone);
            quantityUI.text = cardsInThisDeckElement.Count.ToString();
        }

        if (eventData.button == PointerEventData.InputButton.Right) {

            if (cardsInThisDeckElement.Count == 1) {
                Destroy(gameObject);
                return;
            }

            var lastCard = cardsInThisDeckElement[cardsInThisDeckElement.Count - 1];
            this.cardsInThisDeckElement.Remove(lastCard);
            quantityUI.text = cardsInThisDeckElement.Count.ToString();
        }
    }

}
