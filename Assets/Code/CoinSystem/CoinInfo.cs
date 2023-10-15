using TMPro;
using UnityEngine;

namespace Code.CoinSystem
{
    public class CoinInfo : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        public int CurrentCoinsAmount;
        
        public void ChangeCoin(int amount)
        {
            CurrentCoinsAmount += amount;
            _text.text = CurrentCoinsAmount.ToString();
        }
        
        
    }
}