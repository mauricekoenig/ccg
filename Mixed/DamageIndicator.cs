



using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DamageIndicator : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI damageTmp;

    public void Show(int damage, Vector3 position) {
        this.transform.position = position;
        this.gameObject.SetActive(true);
        damageTmp.text = "-" + damage.ToString();
    }

    public void Hide() {
        this.gameObject.SetActive(false);
    }
}