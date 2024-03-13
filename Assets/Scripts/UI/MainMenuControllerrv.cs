using Audio;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class MainMenuControllerrv : MonoBehaviour
    {
        private const string GameScene = "Game";
        
        [SerializeField]
        private Button _playGamerv;
        [SerializeField]
        private Button _settingsrv;
        [SerializeField]
        private Button _skinBallrv;
        [SerializeField]
        private GameObject _settingsPopuprv;
        [SerializeField]
        private GameObject _skinBallPopuprv;
        
        [SerializeField]
        private TextMeshProUGUI _currentLevelrv;
        [SerializeField]
        private TextMeshProUGUI _bestScorerv;

        private void Awake()
        {
            _playGamerv.onClick.AddListener(LoadGamerv);
            _settingsrv.onClick.AddListener(ShowHideSettingsPopuprv);
            _skinBallrv.onClick.AddListener(ShowHideSkinBallPopuprv);
        }

        private void Start()
        {
            int level = PlayerPrefs.GetInt("level");
            int best = PlayerPrefs.GetInt("best");
            _currentLevelrv.text = $"Level: {level}";
            _bestScorerv.text = $"Best Score: {best}";
        }

        private void OnDestroy()
        {
            _playGamerv.onClick.RemoveListener(LoadGamerv);
            _settingsrv.onClick.RemoveListener(ShowHideSettingsPopuprv);
            _skinBallrv.onClick.RemoveListener(ShowHideSkinBallPopuprv);
        }
        
        private void ShowHideSettingsPopuprv()
        {
            AudioManager.Instance.PlaySFXOneShot(0);
            _settingsPopuprv.SetActive(!_settingsPopuprv.activeSelf);
            _skinBallPopuprv.SetActive(false);
            AudioManager.Instance.PlaySFXOneShot(1);
        }
        
        private void ShowHideSkinBallPopuprv()
        {
            AudioManager.Instance.PlaySFXOneShot(0);
            _skinBallPopuprv.SetActive(!_skinBallPopuprv.activeSelf);
            _settingsPopuprv.SetActive(false);
            AudioManager.Instance.PlaySFXOneShot(1);
        }

        private void LoadGamerv()
        {
            AudioManager.Instance.PlaySFXOneShot(0);
            SceneManager.LoadScene(GameScene);
        }
        
        private int CalculateFactorialrv(int number)
        {
            int result = 1;
            for (int i = 2; i <= number; i++)
            {
                result *= i;
            }
            return result;
        }
    }
}
