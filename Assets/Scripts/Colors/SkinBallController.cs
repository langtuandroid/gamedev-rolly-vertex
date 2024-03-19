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

        private int _activeSkin;

        private void Awake()
        {
            if (!PlayerPrefs.HasKey("skinBall"))
            {
                PlayerPrefs.SetInt("skinBall",0);
            }
            _activeSkin = PlayerPrefs.GetInt("skinBall");

            _allSkinToggle[_activeSkin].isOn = true;

            foreach (Toggle toggle in _allSkinToggle)
            {
                toggle.onValueChanged.AddListener(OnSkinToggleValueChanged);
            }
        }

        private void Start()
        {
            _activeSkin = PlayerPrefs.GetInt("skinBall");
            _allSkinToggle[_activeSkin].isOn = true;
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
            
            int index = _allSkinToggle.FindIndex(toggle => toggle.isOn);
            _skinBall.SetSkinrv(index);
        }
    }
}
