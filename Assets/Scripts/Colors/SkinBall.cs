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
        private List<Material> _allBallSkins;

        public void SetSkinrv(int index)
        {
            SelectedSkinMaterial = _allBallSkins[index];
            PlayerPrefs.SetInt("skinBall",index);
        }

        public Material LoadSelectedSkin() => SelectedSkinMaterial;
    }
}
