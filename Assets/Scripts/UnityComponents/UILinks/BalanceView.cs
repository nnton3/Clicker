using TMPro;
using UnityEngine;

namespace UnityComponents.UILinks
{
    public class BalanceView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _balance; 
       
        public void SetBalance(int balance) => _balance.text = balance.ToString();
    }
}