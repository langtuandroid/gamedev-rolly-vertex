
using UnityEngine;
using UnityEngine.UI;

namespace Colors
{
    public class BallSkinButton : MonoBehaviour
    {
        public Toggle _toggle;
        [SerializeField]
        private PurchasePrduct _purchasePrduct;

        public PurchasePrduct Product
        {
            get => _purchasePrduct;
            set => _purchasePrduct = value;
        }
    }
}
