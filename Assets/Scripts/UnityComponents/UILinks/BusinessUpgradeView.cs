using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UnityComponents.UILinks
{
    public class BusinessUpgradeView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _label;
        [SerializeField] private TMP_Text _bonus;
        [SerializeField] private TMP_Text _cost;
        [SerializeField] private TMP_Text _purchasedBanner;
        [SerializeField] private GameObject _costInfo;

        public void Init(string label, int bonus, int cost)
        {
            _label.text = label;
            _bonus.text = bonus.ToString();
            _cost.text = cost.ToString();
        }

        public void SetPurchasedBanner()
        {
            _costInfo.gameObject.SetActive(false);
            _purchasedBanner.gameObject.SetActive(true);
            GetComponent<Button>().interactable = false;
        }
    }
}