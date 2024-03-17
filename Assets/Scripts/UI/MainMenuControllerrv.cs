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
        private Button _playGameSkinrv;
        [SerializeField]
        private Button _settingsOpenrv;
        [SerializeField]
        private Button _settingsCloserv;
        [SerializeField]
        private Button _skinBallOpenrv;
        [SerializeField]
        private Button _skinBallHiderv;
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
            _playGameSkinrv.onClick.AddListener(LoadGamerv);
            _settingsOpenrv.onClick.AddListener(ShowSettingsPopuprv);
            _settingsCloserv.onClick.AddListener(HideSettingsPopuprv);
            _skinBallOpenrv.onClick.AddListener(ShowSkinBallPopuprv);
            _skinBallHiderv.onClick.AddListener(HideSkinBallPopuprv);
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
            _playGameSkinrv.onClick.RemoveListener(LoadGamerv);
            _settingsOpenrv.onClick.RemoveListener(ShowSettingsPopuprv);
            _settingsCloserv.onClick.AddListener(HideSettingsPopuprv);
            _skinBallOpenrv.onClick.RemoveListener(ShowSkinBallPopuprv);
            _skinBallHiderv.onClick.RemoveListener(HideSkinBallPopuprv);
        }
        
        private void ShowSettingsPopuprv()
        {
            AudioManager.Instance.PlaySFXOneShotrv(0);
            _settingsPopuprv.SetActive(true);
            AudioManager.Instance.PlaySFXOneShotrv(1);
        }
        private void HideSettingsPopuprv()
        {
            AudioManager.Instance.PlaySFXOneShotrv(0);
            _settingsPopuprv.SetActive(false);
            AudioManager.Instance.PlaySFXOneShotrv(1);
        }
        
        private void ShowSkinBallPopuprv()
        {
            AudioManager.Instance.PlaySFXOneShotrv(0);
            _skinBallPopuprv.SetActive(true);
            AudioManager.Instance.PlaySFXOneShotrv(1);
        }
        private void HideSkinBallPopuprv()
        {
            AudioManager.Instance.PlaySFXOneShotrv(0);
            _skinBallPopuprv.SetActive(false);
            AudioManager.Instance.PlaySFXOneShotrv(1);
        }

        private void LoadGamerv()
        {
            AudioManager.Instance.PlaySFXOneShotrv(0);
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
