using Audio;
using DG.Tweening;
using GamePlay;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class UIPanelControllerrv : MonoBehaviour
    {
        private const string MAINMENU = "MainMenu";
        private const string GAME = "Game";

        [SerializeField]
        private Button _pauserv;
        [SerializeField]
        private GameObject _pausePopuprv;
        [SerializeField]
        private Button _mainMenurv;
        [SerializeField]
        private Button _restartrv;
        [SerializeField]
        private Button _resumerv;
        [SerializeField]
        private Camera _camerarv;
        [SerializeField]
        private CameraFollowrv _camFollowrv;
        [SerializeField]
        private GameObject _canvasToOpenrv;
        public GameObject[] objectsToClose;

        private Color _defaultColor;
    
        private void Awake() 
        {
            _defaultColor = _camerarv.backgroundColor;
            _pauserv.onClick.AddListener(ShowHidePausePopuprv);
            _mainMenurv.onClick.AddListener(LoadMainMenurv);
            _restartrv.onClick.AddListener(RestartGameMenurv);
            _resumerv.onClick.AddListener(ShowHidePausePopuprv);
        }

        private void OnDestroy()
        {
            _pauserv.onClick.RemoveListener(ShowHidePausePopuprv);
            _mainMenurv.onClick.RemoveListener(LoadMainMenurv);
            _restartrv.onClick.RemoveListener(RestartGameMenurv);
            _resumerv.onClick.RemoveListener(ShowHidePausePopuprv);
        }

        private void ShowHidePausePopuprv()
        { 
            AudioManager.Instance.PlaySFXOneShot(0);
            if (_pausePopuprv.activeSelf)
            {
                _pausePopuprv.SetActive(false);
                DOTween.PlayAll(); // Приостанавливаем все твины
            }
            else
            {
                _pausePopuprv.SetActive(true);
                DOTween.PauseAll(); // Приостанавливаем все твины
            }
            AudioManager.Instance.PlaySFXOneShot(1);
        }
        
        private void LoadMainMenurv()
        {
            AudioManager.Instance.PlaySFXOneShot(0);
            SceneManager.LoadScene(MAINMENU);
        }
        
        private void RestartGameMenurv()
        {
            AudioManager.Instance.PlaySFXOneShot(0);
            SceneManager.LoadScene(GAME);
        }

        public void OpenVideoModerv()
        {
            _canvasToOpenrv.SetActive(true);
            for (int i = 0; i < objectsToClose.Length; i++) 
            {
                objectsToClose[i].SetActive(false);
            }
        }

        public void StartLikeSecondChancerv()
        {
            _canvasToOpenrv.SetActive(false);
            for (int i = 0; i < objectsToClose.Length; i++)
            {
                objectsToClose[i].SetActive(true);
            }

            GameManager.Instance.SecondChanceWithoutAdrv();
        }
        
        public void ToggleColorrv() 
        {
            if (_camerarv.backgroundColor == _defaultColor) {
                _camerarv.backgroundColor = Color.black;
            } else {
                _camerarv.backgroundColor = _defaultColor;
            }
        }

        public void Forwardrv() {
            _camFollowrv.OffsetZ += 1f;
        }

        public void Backrv() {
            _camFollowrv.OffsetZ -= 1f;
        }

        public void Leftrv() {
            _camFollowrv.FixedX -= 1f;
        }

        public void RightScenerv() {
            _camFollowrv.FixedX += 1f;
        }

        public void Uprv() {
            _camFollowrv.FixedY += 1f;
        }

        public void Downrv() {
            _camFollowrv.FixedY -= 1f;
        }

        public void RotateRightrv() {
            _camerarv.transform.rotation *= Quaternion.Euler(0f, 5f, 0f);
        }

        public void RotateLeftrv() {
            _camerarv.transform.rotation *= Quaternion.Euler(0f, -5f, 0f);
        }

        public void RotateUprv() {
            _camerarv.transform.rotation *= Quaternion.Euler(5f, 0f, 0f);
        }

        public void RotateDownrv() {
            _camerarv.transform.rotation *= Quaternion.Euler(-5f, 0f, 0f);
        }
        
        private bool IsPrimerv(int number)
        {
            if (number < 2) return false;
            for (int i = 2; i * i <= number; i++)
            {
                if (number % i == 0) return false;
            }
            return true;
        }
    }
}