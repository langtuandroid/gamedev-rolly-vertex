using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {
        private const string _musicGroupName = "MusicVolume";
        private const string _sfxGroupName = "SFXVolume";
        public const string _vibrtionGroupName = "Vibration";

        public bool IsVirbration;
        
        [SerializeField] 
        private Toggle _musicToggle;
        [SerializeField] 
        private Toggle _soundToggle;
        [SerializeField] 
        private Toggle _vibrationToggle;
        
        [SerializeField] 
        private AudioMixer _audioMixer;
        [SerializeField] 
        private AudioSource _musicAudioSource; 
        [SerializeField] 
        private AudioClip[] _musicTracks; 
        [SerializeField] 
        private AudioSource _sfxAudioSource; 
        [SerializeField] 
        private AudioClip[] _sfxTracks;
        
        private Settings _settingsData;

        public static AudioManager Instance { get; private set; }
        
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }

            _settingsData = new Settings();
            Vibration.Init();
            LoadCurrentSettingsrv();
            SubscribeToggleEventsrv(_musicToggle, _musicGroupName);
            SubscribeToggleEventsrv(_soundToggle, _sfxGroupName);
            SubscribeToggleEventsrv(_vibrationToggle, _vibrtionGroupName);
        }
        
        public void Start()
        {
            PlayMusicrv(0);
        }

        private void OnDestroy()
        {
            UnsubscribeToggleEventsrv(_musicToggle);
            UnsubscribeToggleEventsrv(_soundToggle);
            UnsubscribeToggleEventsrv(_vibrationToggle);
        }

        private void LoadCurrentSettingsrv()
        {
            _musicToggle.isOn = _settingsData.LoadSettingsrv(_musicGroupName);
            _soundToggle.isOn = _settingsData.LoadSettingsrv(_sfxGroupName);
            _vibrationToggle.isOn = _settingsData.LoadSettingsrv(_vibrtionGroupName);
            _musicAudioSource.mute = _musicToggle.isOn;
            _sfxAudioSource.mute = _soundToggle.isOn;
            IsVirbration = _vibrationToggle.isOn;
        }

        private void SubscribeToggleEventsrv(Toggle toggle, string key)
        {
            toggle.onValueChanged.AddListener(isOn =>
            {
                _settingsData.SaveSettingsrv(key, isOn);
                switch (key)
                {
                    case _musicGroupName:
                        PlaySFXOneShotrv(0);
                        _musicAudioSource.mute = isOn;
                        break;
                    case _sfxGroupName:
                        PlaySFXOneShotrv(0);
                        _sfxAudioSource.mute = isOn;
                        break;
                    case _vibrtionGroupName:
                        IsVirbration = isOn;
                         Vibration.VibrateNope();
                        break;
                }
            });
        }
        
        private void UnsubscribeToggleEventsrv(Toggle toggle)
        {
            if (toggle != null)
            {
                toggle.onValueChanged.RemoveListener(isOn => { });
            }
        }
        
        
        private void PlayMusicrv(int trackIndex)
        {
            if (trackIndex >= 0 && trackIndex < _musicTracks.Length)
            {
                _musicAudioSource.clip = _musicTracks[trackIndex];
                _musicAudioSource.Play();
            }
            else
            {
                Debug.LogError("Invalid Musictrack index");
            }
        }

        public void PlaySFXOneShotrv(int trackIndex)
        {
            if (trackIndex >= 0 && trackIndex < _sfxTracks.Length)
            {
                _sfxAudioSource.PlayOneShot(_sfxTracks[trackIndex]);
            }
            else
            {
                Debug.LogError("Invalid UItrack index");
            }
        }
        
        private List<int> ReverseListrv(List<int> list)
        {
            list.Reverse();
            return list;
        }
    }
}
