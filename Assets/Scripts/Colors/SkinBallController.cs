using System;
using System.Collections.Generic;
using Integration;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Colors
{
    public class SkinBallController : MonoBehaviour
    {
        [SerializeField]
        private SkinBall _skinBall;

        [SerializeField]
        private List<BallSkinButton> _allSkinToggle;
        
        [SerializeField]
        private GameObject _lockbtn;
        [SerializeField]
        private GameObject _unlockbtn;
        [SerializeField]
        private GameObject _watchAdbtn;
        [SerializeField]
        private GameObject _playbtn;
        [SerializeField]
        private GameObject _pricePanel;
        [SerializeField]
        private Image _priceIcon;
        [SerializeField]
        private TextMeshProUGUI _priceText;
        [SerializeField]
        private Sprite _priceCoin;
        [SerializeField]
        private Sprite _priceDiamond;
        [SerializeField]
        private CoinsHolder _coinsHolder;
        [SerializeField]
        private DiamondsHolder _diamondsHolder;

        private int _activeSkin;
        private int _selectedSkin;
        
        private AdMobController _adMobController;
        private RewardedAdController _rewardedAdController;
        
        [Inject]
        private void Construct(AdMobController adMobController, RewardedAdController rewardedAdController)
        {
            _adMobController = adMobController;
            _rewardedAdController = rewardedAdController;
        }
        
        private void Awake()
        {
            if (!PlayerPrefs.HasKey("skinBall"))
            {
                PlayerPrefs.SetInt("skinBall",0);
            }
            foreach (var ballskin in _allSkinToggle)
            {
                ballskin._toggle.onValueChanged.AddListener(OnSkinToggleValueChanged);
            }
            _watchAdbtn.GetComponent<Button>().onClick.AddListener(ShowRewarded);
            _rewardedAdController.GetRewarded += UnlockSkinRewarded;
            
            _activeSkin = PlayerPrefs.GetInt("skinBall");
            _allSkinToggle[_activeSkin]._toggle.isOn = true;
            CheckUnlockStatus(_activeSkin);
        }

        private void OnEnable()
        {
            for (int i = 0; i < _allSkinToggle.Count; i++)
            {
                if (_allSkinToggle[i]._toggle.isOn)
                {
                    CheckUnlockStatus(i);
                }
            }
        }


        private void OnDestroy()
        {
            foreach (var ballskin in _allSkinToggle)
            {
                ballskin._toggle.onValueChanged.RemoveListener(OnSkinToggleValueChanged);
            }
            _watchAdbtn.GetComponent<Button>().onClick.RemoveListener(ShowRewarded);
            _rewardedAdController.GetRewarded -= UnlockSkinRewarded;
        }
        
        private void OnSkinToggleValueChanged(bool isOn)
        {
            if (!isOn) return;
            
            int index = _allSkinToggle.FindIndex(toggle => toggle._toggle.isOn);
            CheckUnlockStatus(index);
            //_skinBall.SetSkinrv(index);
        }

        private void CheckUnlockStatus(int index)
        {
            _selectedSkin = index;
            var product = _allSkinToggle[index].Product;
            var coins = PlayerPrefs.GetInt("Coins", 10000);
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins", 10000));
            _coinsHolder.UpdateCoinsView();
            var diamonds = PlayerPrefs.GetInt("Diamonds", 0);
            _diamondsHolder.UpdateDiamondsView();
            if (product.UnlockStatus)
            {
                _skinBall.SetSkinrv(index);
                _pricePanel.SetActive(false);
                _lockbtn.SetActive(false);
                _unlockbtn.SetActive(false);
                _watchAdbtn.SetActive(false);
                _playbtn.SetActive(true);
            }
            else
            {
                _playbtn.SetActive(false);
                
                if (product._typeBallUnlock == TypeBallUnlock.ForCoins)
                {
                    _watchAdbtn.SetActive(false);
                    _pricePanel.SetActive(true);
                    _priceIcon.sprite = _priceCoin;
                    _priceText.text = product.price.ToString();
                    if (coins >= product.price)
                    {
                        _lockbtn.SetActive(false);
                        _unlockbtn.SetActive(true);
                    }
                    else
                    {
                        _lockbtn.SetActive(true);
                        _unlockbtn.SetActive(false);
                    }
                
                }
                if (product._typeBallUnlock == TypeBallUnlock.ForDiamonds)
                {
                
                    _watchAdbtn.SetActive(false);
                    _pricePanel.SetActive(true);
                    _priceIcon.sprite = _priceDiamond;
                    _priceText.text = product.price.ToString();
                    if (diamonds >= product.price)
                    {
                        _lockbtn.SetActive(false);
                        _unlockbtn.SetActive(true);
                    }
                    else
                    {
                        _lockbtn.SetActive(true);
                        _unlockbtn.SetActive(false);
                    }
                }
                if (product._typeBallUnlock == TypeBallUnlock.ForAd)
                {
                    _pricePanel.SetActive(false);
                    if (product.adsIsWatchComplete)
                    {
                        _lockbtn.SetActive(false);
                        _watchAdbtn.SetActive(false);
                        _unlockbtn.SetActive(true);
                    }
                    else
                    {
                        _lockbtn.SetActive(false);
                        _unlockbtn.SetActive(false);
                        _watchAdbtn.SetActive(true);
                    }
                }
            }
        }

        public void UnlockSkin()
        {
            if ( _allSkinToggle[_selectedSkin].Product._typeBallUnlock == TypeBallUnlock.ForCoins)
            {
                var coins = PlayerPrefs.GetInt("Coins", 0);
                PlayerPrefs.SetInt("Coins",coins - _allSkinToggle[_selectedSkin].Product.price);
                PlayerPrefs.Save();
                _coinsHolder.UpdateCoinsView();
            }
            if ( _allSkinToggle[_selectedSkin].Product._typeBallUnlock == TypeBallUnlock.ForDiamonds)
            {
                var diamonds = PlayerPrefs.GetInt("Diamonds", 0);
                PlayerPrefs.SetInt("Diamonds",diamonds - _allSkinToggle[_selectedSkin].Product.price);
                PlayerPrefs.Save();
                _diamondsHolder.UpdateDiamondsView();
            }
            _allSkinToggle[_selectedSkin].Product.UnlockStatus = true;
            CheckUnlockStatus(_selectedSkin);
        }

        private void ShowRewarded()
        {
            _adMobController.ShowRewardedAd();
        }
        
        private void UnlockSkinRewarded()
        {
            _allSkinToggle[_selectedSkin].Product.UnlockStatus = true;
            CheckUnlockStatus(_selectedSkin);
        }
        
    }
}
