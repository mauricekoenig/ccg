using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardView_Collection : MonoBehaviour, IPointerDownHandler
{
    public Image Artwork;
    private RuntimeCardData cardData;

    public void Init (RuntimeCardData data) {

        this.cardData = data;
        Artwork.sprite = this.cardData.Artwork;
    }

    public void OnPointerDown(PointerEventData eventData) {

        if (cardData.Artwork == null) Debug.Log("Sprite NULL!");
        Debug.Log("Clicked on: " + this.cardData.Name);
    }
}
