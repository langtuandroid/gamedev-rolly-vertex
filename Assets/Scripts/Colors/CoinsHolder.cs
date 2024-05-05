using TMPro;
using UnityEngine;

namespace Colors
{
    public class CoinsHolder : MonoBehaviour
    {
        [SerializeField] 
        private TextMeshProUGUI _coinsAmount;

        private void OnEnable()
        {
            UpdateCoinsView();
        }

        public void UpdateCoinsView()
        {
            _coinsAmount.text = PlayerPrefs.GetInt("Coins",0).ToString();
        }
    }
}
