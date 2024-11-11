using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeckElement : MonoBehaviour
{
    private int quantity;
    private RuntimeCardData[] data = new RuntimeCardData[2];
    public TextMeshProUGUI quantityUI;
    public TextMeshProUGUI cardName;

    public void Init (RuntimeCardData cardData) {

        this.data[0] = cardData;
        this.cardName.text = cardData.Name;
        quantity++;
    }

    public void Add (RuntimeCardData cardData) {

        this.data[1] = cardData;
        quantity++;
        quantityUI.text = quantity.ToString();
    }

}
