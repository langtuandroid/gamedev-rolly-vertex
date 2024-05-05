using TMPro;
using UnityEngine;

namespace Colors
{
    public class DiamondsHolder : MonoBehaviour
    {
        [SerializeField] 
        private TextMeshProUGUI _diamondsAmount;

        private void OnEnable()
        {
            UpdateDiamondsView();
        }

        public void UpdateDiamondsView()
        {
            _diamondsAmount.text = PlayerPrefs.GetInt("Diamonds",0).ToString();
        }
    }
}
