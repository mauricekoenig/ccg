using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CollectionElement : MonoBehaviour, IPointerDownHandler
{
    public Image Artwork;
    [SerializeField] private TextMeshProUGUI cost;
    [SerializeField] private TextMeshProUGUI attack;
    [SerializeField] private TextMeshProUGUI health;
    [SerializeField] private TextMeshProUGUI cardName;

    private RuntimeCardData cardData;

    public void Init (RuntimeCardData data) {

        this.cardData = data;

        if (data is CreatureRuntimeCardData) {

            var creature = (CreatureRuntimeCardData)data;
            this.cardName.text = creature.Name;
            Artwork.sprite = creature.Artwork;
            this.cost.text = creature.Cost.ToString();
            this.attack.text = creature.Attack.ToString();
            this.health.text = creature.Health.ToString();
        }

        else {

            this.cardName.text = data.Name;
            Artwork.sprite = data.Artwork;
            this.cost.text = data.Cost.ToString();
            this.attack.text = data.Cost.ToString();
            this.health.text = data.Cost.ToString();
        }
    }

    public void OnPointerDown(PointerEventData eventData) {

        Debug.Log("Clicked on: " + this.cardData.Name);
    }
}
