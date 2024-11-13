using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
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

    Canvas canvas;

    public void Init (RuntimeCardData cardData) {

        Add(cardData);
        this.cardName.text = cardData.Name;
        this.cost.text = cardData.Cost.ToString();
        quantityUI.text = cardsInThisDeckElement.Count.ToString();
        canvas = FindFirstObjectByType<Canvas>();
    }

    public void Add (RuntimeCardData cardData) {

        if (cardsInThisDeckElement.Count == 2) {
            Debug.Log("FULL!");
            return;
        }

        if (cardsInThisDeckElement.Count == 0) {

            Debug.Log("EMPTY!");
            cardsInThisDeckElement.Add(cardData);
            quantityUI.text = cardsInThisDeckElement.Count.ToString();
            return;
        }

        if (cardData.ID != cardsInThisDeckElement[0].ID) return;

        Debug.Log("HAS SPACE!");
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
