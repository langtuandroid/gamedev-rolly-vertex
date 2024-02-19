using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Colors
{
    public class SkinBallController : MonoBehaviour
    {
        [SerializeField]
        private SkinBall _skinBall;

        [SerializeField]
        private List<Toggle> _allSkinToggle;

        private void Awake()
        {
            if (!PlayerPrefs.HasKey("skinBall"))
            {
                PlayerPrefs.SetInt("skinBall",0);
            }
            int activeSkin = PlayerPrefs.GetInt("skinBall");

            _allSkinToggle[activeSkin].isOn = true;

            foreach (Toggle toggle in _allSkinToggle)
            {
                toggle.onValueChanged.AddListener(OnSkinToggleValueChanged);
            }
        }

        private void OnDestroy()
        {
            foreach (Toggle toggle in _allSkinToggle)
            {
                toggle.onValueChanged.RemoveListener(OnSkinToggleValueChanged);
            }
        }
        
        private void OnSkinToggleValueChanged(bool isOn)
        {
            if (!isOn) return;

            // Находим индекс выбранного тогла
            int index = _allSkinToggle.FindIndex(toggle => toggle.isOn);

            // Устанавливаем скин в скриптабле
            _skinBall.SetSkin(index);
        }
    }
}
