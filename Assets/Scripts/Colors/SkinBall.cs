using System.Collections.Generic;
using UnityEngine;

namespace Colors
{
    [CreateAssetMenu(menuName = "SkinBall")]
    public class SkinBall : ScriptableObject
    {
        [SerializeField]
        private Material SelectedSkinMaterial;
        
        [SerializeField] 
        private List<PurchasePrduct> _allBallSkins;

        public void SetSkinrv(int index)
        {
            SelectedSkinMaterial = _allBallSkins[index]._material;
            PlayerPrefs.SetInt("skinBall",index);
        }
        
        public Material LoadSelectedSkin()
        {
            int index =  PlayerPrefs.GetInt("skinBall");
            SelectedSkinMaterial = _allBallSkins[index]._material;
            return SelectedSkinMaterial;
        }
    }
    
}
