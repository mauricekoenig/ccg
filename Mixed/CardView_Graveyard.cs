using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardView_Graveyard : MonoBehaviour, ICardViewUI {

    private RuntimeCardData data;
    [SerializeField] private Image artwork;
    [SerializeField] private Image color;
    [SerializeField] private TextMeshProUGUI cardText;
    [SerializeField] private GameObject attackGO;
    [SerializeField] private GameObject healthGO;

    [SerializeField] private TextMeshProUGUI attackTmp;
    [SerializeField] private TextMeshProUGUI healthTmp;
    [SerializeField] private TextMeshProUGUI costTmp;

    public void Init (RuntimeCardData cardData) {

        this.data = cardData;
        this.cardText.text = cardData.Name;
        this.artwork.sprite = cardData.Artwork;
        this.color.color = ColorUtility.GetCardColor(cardData.Color);
        this.costTmp.text = cardData.Cost.ToString();

        if (cardData is CreatureRuntimeCardData) {

            var crd = cardData as CreatureRuntimeCardData;

            attackTmp.text = crd.BaseAttack.ToString();
            healthTmp.text = crd.BaseHealth.ToString();
            crd.ResetAllStats();

            attackGO.SetActive(true);
            healthGO.SetActive(true);
        }

        else {

            attackGO.SetActive(false);
            healthGO.SetActive(false);
        }
    }
}
