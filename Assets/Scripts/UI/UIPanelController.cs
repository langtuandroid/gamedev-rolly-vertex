using Audio;
using DG.Tweening;
using GamePlay;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class UIPanelController : MonoBehaviour
    {
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

        private Color defaultColor;
    
        private void Awake() 
        {
            defaultColor = _camerarv.backgroundColor;
          
            _pauserv.onClick.AddListener(ShowHidePausePopup);
            _mainMenurv.onClick.AddListener(LoadMainMenu);
            _restartrv.onClick.AddListener(RestartGameMenu);
            _resumerv.onClick.AddListener(ShowHidePausePopup);
        }

        private void OnDestroy()
        {
            _pauserv.onClick.RemoveListener(ShowHidePausePopup);
            _mainMenurv.onClick.RemoveListener(LoadMainMenu);
            _restartrv.onClick.RemoveListener(RestartGameMenu);
            _resumerv.onClick.RemoveListener(ShowHidePausePopup);
        }

        private void ShowHidePausePopup()
        { 
            AudioManager.Instance.PlaySFXOneShot(0);
            if (_pausePopuprv.activeSelf)
            {
                _pausePopuprv.SetActive(false);
                //Time.timeScale = 1;
                DOTween.PlayAll(); // Приостанавливаем все твины
            }
            else
            {
                _pausePopuprv.SetActive(true);
               // Time.timeScale = 0;
                DOTween.PauseAll(); // Приостанавливаем все твины
            }
            AudioManager.Instance.PlaySFXOneShot(1);
        }
        
        private void LoadMainMenu()
        {
            AudioManager.Instance.PlaySFXOneShot(0);
            SceneManager.LoadScene("MainMenu");
        }
        private void RestartGameMenu()
        {
            AudioManager.Instance.PlaySFXOneShot(0);
            SceneManager.LoadScene("Game");
        }

        public void OpenVideoMode()
        {
            _canvasToOpenrv.SetActive(true);
            for (int i = 0; i < objectsToClose.Length; i++) 
            {
                objectsToClose[i].SetActive(false);
            }
        }

        public void StartLikeSecondChance()
        {
            _canvasToOpenrv.SetActive(false);
            for (int i = 0; i < objectsToClose.Length; i++)
            {
                objectsToClose[i].SetActive(true);
            }

            GameManager.Instance.SecondChanceWithoutAd();
        }
        
        public void ToggleColor() 
        {
            if (_camerarv.backgroundColor == defaultColor) {
                _camerarv.backgroundColor = Color.black;
            } else {
                _camerarv.backgroundColor = defaultColor;
            }
        }

        public void Forward() {
            _camFollowrv.OffsetZ += 1f;
        }

        public void Back() {
            _camFollowrv.OffsetZ -= 1f;
        }

        public void Left() {
            _camFollowrv.FixedX -= 1f;
        }

        public void Right() {
            _camFollowrv.FixedX += 1f;
        }

        public void Up() {
            _camFollowrv.FixedY += 1f;
        }

        public void Down() {
            _camFollowrv.FixedY -= 1f;
        }

        public void RotateRight() {
            _camerarv.transform.rotation *= Quaternion.Euler(0f, 5f, 0f);
        }

        public void RotateLeft() {
            _camerarv.transform.rotation *= Quaternion.Euler(0f, -5f, 0f);
        }

        public void RotateUp() {
            _camerarv.transform.rotation *= Quaternion.Euler(5f, 0f, 0f);
        }

        public void RotateDown() {
            _camerarv.transform.rotation *= Quaternion.Euler(-5f, 0f, 0f);
        }
    }
}